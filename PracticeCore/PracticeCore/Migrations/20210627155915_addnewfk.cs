using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeCore.Migrations
{
    public partial class addnewfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGalary_Books_Booksid",
                table: "BookGalary");

            migrationBuilder.DropIndex(
                name: "IX_BookGalary_Booksid",
                table: "BookGalary");

            migrationBuilder.DropColumn(
                name: "Booksid",
                table: "BookGalary");

            migrationBuilder.CreateIndex(
                name: "IX_BookGalary_BookId",
                table: "BookGalary",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGalary_Books_BookId",
                table: "BookGalary",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGalary_Books_BookId",
                table: "BookGalary");

            migrationBuilder.DropIndex(
                name: "IX_BookGalary_BookId",
                table: "BookGalary");

            migrationBuilder.AddColumn<int>(
                name: "Booksid",
                table: "BookGalary",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGalary_Booksid",
                table: "BookGalary",
                column: "Booksid");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGalary_Books_Booksid",
                table: "BookGalary",
                column: "Booksid",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
