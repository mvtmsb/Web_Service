using Microsoft.EntityFrameworkCore.Migrations;

namespace Attractions.WebService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameObject = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Admission = table.Column<string>(nullable: true),
                    PeriodA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "Admission", "Location", "NameObject", "PeriodA" },
                values: new object[] { 1L, "срок действия истек, эксплуатация аттракциона не допускается", "Парк Культуры и отдыха «Сокольники»", "DiscO", "04.2019-12.2019" });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "Admission", "Location", "NameObject", "PeriodA" },
                values: new object[] { 2L, "срок действия истек, эксплуатация аттракциона не допускается", "Парк Культуры и отдыха «Измайловский»", "Деревенский поезд", "04.2019-12.2019" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attractions");
        }
    }
}
