using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car360.Migrations
{
    /// <inheritdoc />
    public partial class Signs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_sign",
                columns: table => new
                {
                    s_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    s_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sign", x => x.s_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_sign");
        }
    }
}
