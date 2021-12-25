using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP.Data.Migrations
{
    public partial class SelectedAttributesToCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributesId",
                table: "ItemToCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemToCarts_AttributesId",
                table: "ItemToCarts",
                column: "AttributesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemToCarts_Attributes_AttributesId",
                table: "ItemToCarts",
                column: "AttributesId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemToCarts_Attributes_AttributesId",
                table: "ItemToCarts");

            migrationBuilder.DropIndex(
                name: "IX_ItemToCarts_AttributesId",
                table: "ItemToCarts");

            migrationBuilder.DropColumn(
                name: "AttributesId",
                table: "ItemToCarts");
        }
    }
}
