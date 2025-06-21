using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car360.Migrations
{
    /// <inheritdoc />
    public partial class Buys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_buy",
                columns: table => new
                {
                    b_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    b_company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_buy", x => x.b_id);
                    table.ForeignKey(
                        name: "FK_tbl_buy_tbl_sign_User_ID",
                        column: x => x.User_ID,
                        principalTable: "tbl_sign",
                        principalColumn: "s_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_buy_User_ID",
                table: "tbl_buy",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_buy");
        }
    }
}
