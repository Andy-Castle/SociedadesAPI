using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SociedadesAPI.Migrations
{
    /// <inheritdoc />
    public partial class CracionDeTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Sociedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consecutivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveCasfim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SociedadNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapitalNeto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequerimientoCapital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nicap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Federacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Sociedades", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Sociedades");
        }
    }
}
