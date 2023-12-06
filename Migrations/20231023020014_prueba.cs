using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiColegioPagos.Migrations
{
    /// <inheritdoc />
    public partial class prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Est_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Est_cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Est_nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Est_direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Est_activo = table.Column<bool>(type: "bit", nullable: false),
                    Pension = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Est_id);
                });

            migrationBuilder.CreateTable(
                name: "Globals",
                columns: table => new
                {
                    Glo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Glo_nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Glo_valor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Globals", x => x.Glo_id);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Pag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pag_cuota = table.Column<int>(type: "int", nullable: false),
                    Estudiante = table.Column<int>(type: "int", nullable: false),
                    Pension = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Pag_id);
                });

            migrationBuilder.CreateTable(
                name: "Pensiones",
                columns: table => new
                {
                    Pen_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pen_nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pen_valor = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pensiones", x => x.Pen_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Globals");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Pensiones");
        }
    }
}
