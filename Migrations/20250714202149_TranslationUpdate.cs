using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_eCommerceProject.Migrations
{
    /// <inheritdoc />
    public partial class TranslationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId1",
                table: "CategoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_CategoryId1",
                table: "CategoryTranslations");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CategoryTranslations");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryTranslations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "CategoryTranslations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "CategoryTranslations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_CategoryId1",
                table: "CategoryTranslations",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId1",
                table: "CategoryTranslations",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
