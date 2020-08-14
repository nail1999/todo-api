using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssignedTo = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    PercentComplete = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "AssignedTo", "Description", "ExpiryDate", "PercentComplete", "Title" },
                values: new object[] { 1, "nail", "Get requirement details and create estimation", new DateTime(2020, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Analyze requirement" });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "AssignedTo", "Description", "ExpiryDate", "PercentComplete", "Title" },
                values: new object[] { 2, "nail", "Create new restful api project", new DateTime(2020, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Develop restful API" });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "AssignedTo", "Description", "ExpiryDate", "PercentComplete", "Title" },
                values: new object[] { 3, "nail", "Unit test the restful api", new DateTime(2020, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Unit test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");
        }
    }
}
