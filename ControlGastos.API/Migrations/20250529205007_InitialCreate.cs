using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlGastos.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FondosMonetarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FondosMonetarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposGasto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposGasto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Depositos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FondoMonetarioId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depositos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depositos_FondosMonetarios_FondoMonetarioId",
                        column: x => x.FondoMonetarioId,
                        principalTable: "FondosMonetarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GastoEncabezados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FondoMonetarioId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastoEncabezados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GastoEncabezados_FondosMonetarios_FondoMonetarioId",
                        column: x => x.FondoMonetarioId,
                        principalTable: "FondosMonetarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presupuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoGastoId = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    MontoPresupuestado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuestos_TiposGasto_TipoGastoId",
                        column: x => x.TipoGastoId,
                        principalTable: "TiposGasto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GastoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GastoEncabezadoId = table.Column<int>(type: "int", nullable: false),
                    TipoGastoId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GastoDetalles_GastoEncabezados_GastoEncabezadoId",
                        column: x => x.GastoEncabezadoId,
                        principalTable: "GastoEncabezados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GastoDetalles_TiposGasto_TipoGastoId",
                        column: x => x.TipoGastoId,
                        principalTable: "TiposGasto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depositos_FondoMonetarioId",
                table: "Depositos",
                column: "FondoMonetarioId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoDetalles_GastoEncabezadoId",
                table: "GastoDetalles",
                column: "GastoEncabezadoId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoDetalles_TipoGastoId",
                table: "GastoDetalles",
                column: "TipoGastoId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoEncabezados_FondoMonetarioId",
                table: "GastoEncabezados",
                column: "FondoMonetarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_TipoGastoId",
                table: "Presupuestos",
                column: "TipoGastoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depositos");

            migrationBuilder.DropTable(
                name: "GastoDetalles");

            migrationBuilder.DropTable(
                name: "Presupuestos");

            migrationBuilder.DropTable(
                name: "GastoEncabezados");

            migrationBuilder.DropTable(
                name: "TiposGasto");

            migrationBuilder.DropTable(
                name: "FondosMonetarios");
        }
    }
}
