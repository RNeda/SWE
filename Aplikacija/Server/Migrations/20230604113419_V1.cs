using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojGradproject.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipKorisnika = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dogadjaji",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumVreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizatorID = table.Column<int>(type: "int", nullable: true),
                    CenaKarte = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadjaji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dogadjaji_Korisnici_OrganizatorID",
                        column: x => x.OrganizatorID,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    KratakOpis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VremeTrajanjaUMin = table.Column<int>(type: "int", nullable: false),
                    BrMesta = table.Column<int>(type: "int", nullable: false),
                    Zanr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Filmovi_Dogadjaji_ID",
                        column: x => x.ID,
                        principalTable: "Dogadjaji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filmovi_Korisnici_KorisniciID",
                        column: x => x.KorisniciID,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Koncerti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Izvodjac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrMesta = table.Column<int>(type: "int", nullable: false),
                    KorisniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koncerti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Koncerti_Dogadjaji_ID",
                        column: x => x.ID,
                        principalTable: "Dogadjaji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Koncerti_Korisnici_KorisniciID",
                        column: x => x.KorisniciID,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OstaliDogadjaji",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    KratakOpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrajeDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OstaliDogadjaji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OstaliDogadjaji_Dogadjaji_ID",
                        column: x => x.ID,
                        principalTable: "Dogadjaji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OstaliDogadjaji_Korisnici_KorisniciID",
                        column: x => x.KorisniciID,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Predstave",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    BrMesta = table.Column<int>(type: "int", nullable: false),
                    KratakOpis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predstave", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Predstave_Dogadjaji_ID",
                        column: x => x.ID,
                        principalTable: "Dogadjaji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Predstave_Korisnici_KorisniciID",
                        column: x => x.KorisniciID,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Utakmice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Sport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Klub1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Klub2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrMesta = table.Column<int>(type: "int", nullable: false),
                    KorisniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utakmice_Dogadjaji_ID",
                        column: x => x.ID,
                        principalTable: "Dogadjaji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utakmice_Korisnici_KorisniciID",
                        column: x => x.KorisniciID,
                        principalTable: "Korisnici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Gosti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KoncertiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gosti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Gosti_Koncerti_KoncertiID",
                        column: x => x.KoncertiID,
                        principalTable: "Koncerti",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Glumci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmoviID = table.Column<int>(type: "int", nullable: true),
                    PredstaveID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glumci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Glumci_Filmovi_FilmoviID",
                        column: x => x.FilmoviID,
                        principalTable: "Filmovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Glumci_Predstave_PredstaveID",
                        column: x => x.PredstaveID,
                        principalTable: "Predstave",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaji_OrganizatorID",
                table: "Dogadjaji",
                column: "OrganizatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Filmovi_KorisniciID",
                table: "Filmovi",
                column: "KorisniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Glumci_FilmoviID",
                table: "Glumci",
                column: "FilmoviID");

            migrationBuilder.CreateIndex(
                name: "IX_Glumci_PredstaveID",
                table: "Glumci",
                column: "PredstaveID");

            migrationBuilder.CreateIndex(
                name: "IX_Gosti_KoncertiID",
                table: "Gosti",
                column: "KoncertiID");

            migrationBuilder.CreateIndex(
                name: "IX_Koncerti_KorisniciID",
                table: "Koncerti",
                column: "KorisniciID");

            migrationBuilder.CreateIndex(
                name: "IX_OstaliDogadjaji_KorisniciID",
                table: "OstaliDogadjaji",
                column: "KorisniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Predstave_KorisniciID",
                table: "Predstave",
                column: "KorisniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_KorisniciID",
                table: "Utakmice",
                column: "KorisniciID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Glumci");

            migrationBuilder.DropTable(
                name: "Gosti");

            migrationBuilder.DropTable(
                name: "OstaliDogadjaji");

            migrationBuilder.DropTable(
                name: "Utakmice");

            migrationBuilder.DropTable(
                name: "Filmovi");

            migrationBuilder.DropTable(
                name: "Predstave");

            migrationBuilder.DropTable(
                name: "Koncerti");

            migrationBuilder.DropTable(
                name: "Dogadjaji");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
