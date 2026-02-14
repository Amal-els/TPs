using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP4.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Action" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Comedy" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Drama" }
                });

            migrationBuilder.InsertData(
                table: "memberships",
                columns: new[] { "Id", "Discount", "TypeName" },
                values: new object[,]
                {
                    { 1, 5, "Basic" },
                    { 2, 15, "Premium" },
                    { 3, 25, "VIP" }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "FullName", "IsSubscribed", "MembershipId" },
                values: new object[,]
                {
                    { 1, "Alice Smith", true, 2 },
                    { 2, "Bob Johnson", false, 1 },
                    { 3, "Charlie Brown", true, 3 }
                });

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "Id", "GenreId", "ReleaseDate", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Inception" },
                    { 2, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "The Dark Knight" },
                    { 3, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2009, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "The Hangover" },
                    { 4, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Forrest Gump" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "genres",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "genres",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "genres",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "memberships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "memberships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "memberships",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
