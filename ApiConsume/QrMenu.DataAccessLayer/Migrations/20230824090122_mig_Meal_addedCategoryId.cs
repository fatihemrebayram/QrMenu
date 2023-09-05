using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QrMenu.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_Meal_addedCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Meals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
