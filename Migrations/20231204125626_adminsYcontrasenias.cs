using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiColegioPagos.Migrations
{
    /// <inheritdoc />
    public partial class adminsYcontrasenias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "contrasenia",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropColumn(
                name: "contrasenia",
                table: "Estudiantes");

            migrationBuilder.RenameColumn(
                name: "Pension",
                table: "Pagos",
                newName: "Pen_id");

            migrationBuilder.RenameColumn(
                name: "Pension",
                table: "Estudiantes",
                newName: "Pen_id");
        }
    }
}
