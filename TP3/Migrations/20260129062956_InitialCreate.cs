using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membershiptypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SignUpFee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    DiscountRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membershiptypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    genre_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAjoutMovie = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_genres_genre_Id",
                        column: x => x.genre_Id,
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    membershiptype_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Membershiptypes_membershiptype_Id",
                        column: x => x.membershiptype_Id,
                        principalTable: "Membershiptypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMovie",
                columns: table => new
                {
                    CustomersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMovie", x => new { x.CustomersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CustomerMovie_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Membershiptypes",
                columns: new[] { "Id", "DiscountRate", "DurationInMonths", "SignUpFee" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0.0, 1, "0" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 10.0, 3, "30" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 15.0, 6, "90" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 20.0, 12, "300" }
                });

            migrationBuilder.InsertData(
                table: "genres",
                columns: new[] { "Id", "GenreName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Action" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Comedy" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DateAjoutMovie", "ImageFile", "Month", "Name", "Year", "genre_Id" },
                values: new object[,]
                {
                    { new Guid("55c00488-dc7e-4909-bb85-708cba3a48e7"), null, null, 5, "Movie 2", 2022, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("e1081557-8301-405c-887a-c44e6be1954e"), null, null, 1, "Movie 1", 2023, new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMovie_MoviesId",
                table: "CustomerMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_membershiptype_Id",
                table: "Customers",
                column: "membershiptype_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_genre_Id",
                table: "Movies",
                column: "genre_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CustomerMovie");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Membershiptypes");

            migrationBuilder.DropTable(
                name: "genres");
        }
    }
}
