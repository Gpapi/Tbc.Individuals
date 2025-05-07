using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tbc.Individuals.Persistance.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslationValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TranslationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationValues_Translations_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IndividualId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => new { x.IndividualId, x.Number, x.Type });
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedIndividuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationshipType = table.Column<int>(type: "int", nullable: false),
                    RelatedIndividualId = table.Column<int>(type: "int", nullable: false),
                    IndividualId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedIndividuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedIndividuals_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedIndividuals_Individuals_RelatedIndividualId",
                        column: x => x.RelatedIndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Translations",
                column: "Id",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8,
                    9,
                    10,
                    11,
                    12,
                    13,
                    14,
                    15,
                    16,
                    17,
                    18,
                    19,
                    20
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "NameId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 },
                    { 11, 11 },
                    { 12, 12 },
                    { 13, 13 },
                    { 14, 14 },
                    { 15, 15 },
                    { 16, 16 },
                    { 17, 17 },
                    { 18, 18 },
                    { 19, 19 },
                    { 20, 20 }
                });

            migrationBuilder.InsertData(
                table: "TranslationValues",
                columns: new[] { "Id", "Language", "TranslationId", "Value" },
                values: new object[,]
                {
                    { 1, "ka", 1, "თბილისი" },
                    { 2, "en", 1, "Tbilisi" },
                    { 3, "ka", 2, "ბათუმი" },
                    { 4, "en", 2, "Batumi" },
                    { 5, "ka", 3, "ქუთაისი" },
                    { 6, "en", 3, "Kutaisi" },
                    { 7, "ka", 4, "რუსთავი" },
                    { 8, "en", 4, "Rustavi" },
                    { 9, "ka", 5, "გორი" },
                    { 10, "en", 5, "Gori" },
                    { 11, "ka", 6, "ზუგდიდი" },
                    { 12, "en", 6, "Zugdidi" },
                    { 13, "ka", 7, "ფოთი" },
                    { 14, "en", 7, "Poti" },
                    { 15, "ka", 8, "თელავი" },
                    { 16, "en", 8, "Telavi" },
                    { 17, "ka", 9, "ახალციხე" },
                    { 18, "en", 9, "Akhaltsikhe" },
                    { 19, "ka", 10, "ოზურგეთი" },
                    { 20, "en", 10, "Ozurgeti" },
                    { 21, "ka", 11, "ამბროლაური" },
                    { 22, "en", 11, "Ambrolauri" },
                    { 23, "ka", 12, "ახალქალაქი" },
                    { 24, "en", 12, "Akhalkalaki" },
                    { 25, "ka", 13, "ბორჯომი" },
                    { 26, "en", 13, "Borjomi" },
                    { 27, "ka", 14, "ლანჩხუთი" },
                    { 28, "en", 14, "Lanchkhuti" },
                    { 29, "ka", 15, "მარნეული" },
                    { 30, "en", 15, "Marneuli" },
                    { 31, "ka", 16, "საჩხერე" },
                    { 32, "en", 16, "Sachkhere" },
                    { 33, "ka", 17, "საგარეჯო" },
                    { 34, "en", 17, "Sagarejo" },
                    { 35, "ka", 18, "ჩხოროწყუ" },
                    { 36, "en", 18, "Chkhorotsku" },
                    { 37, "ka", 19, "ხაშური" },
                    { 38, "en", 19, "Khashuri" },
                    { 39, "ka", 20, "წალკა" },
                    { 40, "en", 20, "Tsalka" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_NameId",
                table: "Cities",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_CityId",
                table: "Individuals",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedIndividuals_IndividualId",
                table: "RelatedIndividuals",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedIndividuals_RelatedIndividualId",
                table: "RelatedIndividuals",
                column: "RelatedIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationValues_TranslationId",
                table: "TranslationValues",
                column: "TranslationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "RelatedIndividuals");

            migrationBuilder.DropTable(
                name: "TranslationValues");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Translations");
        }
    }
}
