using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Follower",
            //     columns: table => new
            //     {
            //         who_id = table.Column<int>(type: "integer", nullable: false),
            //         whom_id = table.Column<int>(type: "integer", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Follower", x => new { x.who_id, x.whom_id });
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Message",
            //     columns: table => new
            //     {
            //         message_id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         author_id = table.Column<int>(type: "integer", nullable: false),
            //         text = table.Column<string>(type: "text", nullable: false),
            //         pub_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         flagged = table.Column<int>(type: "integer", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Message", x => x.message_id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "User",
            //     columns: table => new
            //     {
            //         user_id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         username = table.Column<string>(type: "text", nullable: false),
            //         email = table.Column<string>(type: "text", nullable: false),
            //         pwd = table.Column<string>(type: "text", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_User", x => x.user_id);
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_User_username",
            //     table: "User",
            //     column: "username",
            //     unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follower");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
