using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DGII.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contribuyentes",
                columns: table => new
                {
                    rnc_cedula = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tipo = table.Column<short>(type: "smallint", nullable: false),
                    estatus = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contribuyentes", x => x.rnc_cedula);
                });

            migrationBuilder.CreateTable(
                name: "comprobantes_fiscales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rnc_cedula = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    ncf = table.Column<string>(type: "character varying(19)", maxLength: 19, nullable: false),
                    monto = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    itbis18 = table.Column<decimal>(type: "numeric(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comprobantes_fiscales", x => x.id);
                    table.ForeignKey(
                        name: "FK_comprobantes_fiscales_contribuyentes_rnc_cedula",
                        column: x => x.rnc_cedula,
                        principalTable: "contribuyentes",
                        principalColumn: "rnc_cedula",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comprobantes_fiscales_ncf",
                table: "comprobantes_fiscales",
                column: "ncf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comprobantes_fiscales_rnc_cedula",
                table: "comprobantes_fiscales",
                column: "rnc_cedula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comprobantes_fiscales");

            migrationBuilder.DropTable(
                name: "contribuyentes");
        }
    }
}
