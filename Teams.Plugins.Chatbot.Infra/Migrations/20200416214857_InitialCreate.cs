using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teams.Plugins.Chatbot.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserPrincipalName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_UserPrincipalName", x => x.UserPrincipalName);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnsweredQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnsweredQuestion_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnsweredQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnsweredQuestion_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "Created", "Text", "Updated" },
                values: new object[] { 1, new DateTime(2020, 4, 16, 22, 48, 56, 819, DateTimeKind.Local), "Please select from the options below to indicate your status in the office today.", new DateTime(2020, 4, 16, 22, 48, 56, 820, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "Created", "QuestionId", "Text", "Updated" },
                values: new object[] { 1, new DateTime(2020, 4, 16, 22, 48, 56, 821, DateTimeKind.Local), 1, "Off Sick", new DateTime(2020, 4, 16, 22, 48, 56, 821, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "Created", "QuestionId", "Text", "Updated" },
                values: new object[] { 2, new DateTime(2020, 4, 16, 22, 48, 56, 821, DateTimeKind.Local), 1, "Working", new DateTime(2020, 4, 16, 22, 48, 56, 821, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_Text",
                table: "Answer",
                column: "Text",
                unique: true,
                filter: "[Text] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuestion_AnswerId",
                table: "AnsweredQuestion",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuestion_QuestionId",
                table: "AnsweredQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuestion_UserId",
                table: "AnsweredQuestion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Text",
                table: "Question",
                column: "Text",
                unique: true,
                filter: "[Text] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredQuestion");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
