using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplacePetProj.Migrations
{
    public partial class addCLientImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_ClientId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "products",
                newName: "clientId");

            migrationBuilder.RenameIndex(
                name: "IX_products_ClientId",
                table: "products",
                newName: "IX_products_clientId");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_clientId",
                table: "products",
                column: "clientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_clientId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "products",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_products_clientId",
                table: "products",
                newName: "IX_products_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_ClientId",
                table: "products",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
