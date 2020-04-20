// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teams.Plugins.Chatbot.Infra.Database;
using Teams.Plugins.Chatbot.Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EchoBot2.Bots
{
    public class EchoBot : TeamsActivityHandler
    {
        private string _appId;
        private string _appPassword;
        private readonly ChatbotDbContext _context;

        public EchoBot(IConfiguration config, ChatbotDbContext context)
        {
            _appId = config["MicrosoftAppId"];
            _appPassword = config["MicrosoftAppPassword"];
            _context = context;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            turnContext.Activity.RemoveRecipientMention();

            if (turnContext.Activity.Value != null)
            {
                JToken commandToken = JToken.Parse(turnContext.Activity.Value.ToString());
                var command = commandToken["id"].Value<string>();

                switch (command)
                {
                    case "messageAll":
                        await MessageAllMembersAsync(turnContext, cancellationToken);
                        break;

                    case "workingStatus":
                        using (var context = _context)
                        {
                            var senderId = turnContext.Activity.From.Id;
                            var memberInfo = TeamsInfo.GetMemberAsync(turnContext, senderId).Result;

                            var currentUser = await context.Users
                                .Where(x => x.Email == memberInfo.Email)
                                .FirstOrDefaultAsync();

                            if (currentUser != null)
                            {
                                await turnContext.SendActivityAsync(MessageFactory.Text($"User already exists."), cancellationToken);
                            }
                            else
                            {
                                var newUser = new User(memberInfo.UserPrincipalName, memberInfo.Email);

                                context.Add(newUser);
                                context.SaveChanges();
                            }

                            await turnContext.SendActivityAsync(MessageFactory.Text($"Thanks, your status has been recorded."), cancellationToken);
                        };
                        break;

                    default:
                        await turnContext.SendActivityAsync(MessageFactory.Text($"Sorry, an error has occured. Please try again."), cancellationToken);
                        break;
                }
            }
            else
            {
                var card = CreateAdaptiveCardAttachment("mainCard.json", "");
                var response = CreateResponse(turnContext.Activity, card);

                await turnContext.SendActivityAsync(response, cancellationToken);
            }
        }

        protected override async Task OnTeamsMembersAddedAsync(IList<TeamsChannelAccount> membersAdded, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var teamMember in membersAdded)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to the team {teamMember.GivenName} {teamMember.Surname}."), cancellationToken);
            }
        }

        // Create an attachment message response.
        private Activity CreateResponse(IActivity activity, Attachment attachment)
        {
            var response = ((Activity)activity).CreateReply();
            response.Attachments = new List<Attachment>() { attachment };
            return response;
        }

        // Create Adaptive Card from file.
        private Attachment CreateAdaptiveCardAttachment(string filename, string answer)
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", filename };
            string fullPath = Path.Combine(paths);
            var adaptiveCard = File.ReadAllText(fullPath);

            // set dynamic variables
            string today = DateTime.Today.ToString("dd MMM yyyy");
            adaptiveCard = adaptiveCard.Replace("{today}", today);

            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCard),
            };
        }

        // If you encounter permission-related errors when sending this message, see
        // https://aka.ms/BotTrustServiceUrl
        private async Task MessageAllMembersAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var teamsChannelId = turnContext.Activity.TeamsGetChannelId();
            var serviceUrl = turnContext.Activity.ServiceUrl;
            var credentials = new MicrosoftAppCredentials(_appId, _appPassword);
            ConversationReference conversationReference = null;

            var members = await TeamsInfo.GetMembersAsync(turnContext, cancellationToken);

            foreach (var teamMember in members)
            {
                var card = CreateAdaptiveCardAttachment("statusCard.json", "");
                var proactiveMessage = CreateResponse(turnContext.Activity, card);

                var conversationParameters = new ConversationParameters
                {
                    IsGroup = false,
                    Bot = turnContext.Activity.Recipient,
                    Members = new ChannelAccount[] { teamMember },
                    TenantId = turnContext.Activity.Conversation.TenantId,
                };

                await ((BotFrameworkAdapter)turnContext.Adapter).CreateConversationAsync(
                    teamsChannelId,
                    serviceUrl,
                    credentials,
                    conversationParameters,
                    async (t1, c1) =>
                    {
                        conversationReference = t1.Activity.GetConversationReference();
                        await ((BotFrameworkAdapter)turnContext.Adapter).ContinueConversationAsync(
                            _appId,
                            conversationReference,
                            async (t2, c2) =>
                            {
                                await t2.SendActivityAsync(proactiveMessage, c2);
                            },
                            cancellationToken);
                    },
                    cancellationToken);
            }

            await turnContext.SendActivityAsync(MessageFactory.Text("All messages have been sent."), cancellationToken);
        }
    }
}