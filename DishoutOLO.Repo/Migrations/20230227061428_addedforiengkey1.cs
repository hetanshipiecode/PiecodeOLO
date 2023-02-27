using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DishoutOLO.Repo.Migrations
{
    /// <inheritdoc />
    public partial class addedforiengkey1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Categories_CategoryFK",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_CategoryFK",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "CategoryFK",
                table: "Menus");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_CategoryId",
                table: "Menus",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Categories_CategoryId",
                table: "Menus",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Categories_CategoryId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_CategoryId",
                table: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "CategoryFK",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_CategoryFK",
                table: "Menus",
                column: "CategoryFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Categories_CategoryFK",
                table: "Menus",
                column: "CategoryFK",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
