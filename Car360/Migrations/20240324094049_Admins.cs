using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car360.Migrations
{
    /// <inheritdoc />
    public partial class Admins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_admin",
                columns: table => new
                {
                    a_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    a_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a_mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a_pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admin", x => x.a_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_admin");
        }
    }
}
