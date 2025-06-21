using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car360.Migrations
{
    /// <inheritdoc />
    public partial class Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.p_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_product");
        }
    }
}
