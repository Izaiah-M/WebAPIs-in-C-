using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication_Project1.Migrations
{
    /// <inheritdoc />
    public partial class AccessLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "809d019a-18e7-4916-8ad0-1225cb947080");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8baa9b1f-c8ed-44d3-b388-6678421c06e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d003cf97-56e8-4baf-9f99-b19614d0078b");

            migrationBuilder.AddColumn<string>(
                name: "AccessLevel",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "AccessLevel", "ConcurrencyStamp", "Description", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "453fb60f-3271-45b2-b183-c2d06201add6", "/[\"admin dashboard/\"]", null, "Admin role", "ApiRoles", "Administrator", "ADMINISTRATOR" },
                    { "53fc64bf-cb3c-414d-8167-6d2614b28aee", "/[\"admin dashboard/\", \"hotel dashboard\", \"user dashboard\"]", null, "Super Admin role", "ApiRoles", "Super Administrator", "SUPER ADMINISTRATOR" },
                    { "76671baf-3c76-4c11-b635-8efe754775dd", "/[\"user dashboard/\"]", null, "customer role", "ApiRoles", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453fb60f-3271-45b2-b183-c2d06201add6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53fc64bf-cb3c-414d-8167-6d2614b28aee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76671baf-3c76-4c11-b635-8efe754775dd");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "809d019a-18e7-4916-8ad0-1225cb947080", null, "customer role", "ApiRoles", "User", "USER" },
                    { "8baa9b1f-c8ed-44d3-b388-6678421c06e7", null, "Super Admin role", "ApiRoles", "Super Administrator", "SUPER ADMINISTRATOR" },
                    { "d003cf97-56e8-4baf-9f99-b19614d0078b", null, "Admin role", "ApiRoles", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
