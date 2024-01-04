using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Delete_ForeignKey_YCPK_YCSD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauPhucKhao_BangDiem_MaBangDiem",
                table: "YeuCauPhucKhao");

            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauSuaDiem_BangDiem_MaBangDiem",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauSuaDiem_MaBangDiem",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauPhucKhao_MaBangDiem",
                table: "YeuCauPhucKhao");

            migrationBuilder.DropColumn(
                name: "MaBangDiem",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropColumn(
                name: "MaBangDiem",
                table: "YeuCauPhucKhao");

            migrationBuilder.AddColumn<int>(
                name: "BangDiemMaBangDiem",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BangDiemMaBangDiem",
                table: "YeuCauPhucKhao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_BangDiemMaBangDiem",
                table: "YeuCauSuaDiem",
                column: "BangDiemMaBangDiem");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauPhucKhao_BangDiemMaBangDiem",
                table: "YeuCauPhucKhao",
                column: "BangDiemMaBangDiem");

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauPhucKhao_BangDiem_BangDiemMaBangDiem",
                table: "YeuCauPhucKhao",
                column: "BangDiemMaBangDiem",
                principalTable: "BangDiem",
                principalColumn: "MaBangDiem");

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauSuaDiem_BangDiem_BangDiemMaBangDiem",
                table: "YeuCauSuaDiem",
                column: "BangDiemMaBangDiem",
                principalTable: "BangDiem",
                principalColumn: "MaBangDiem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauPhucKhao_BangDiem_BangDiemMaBangDiem",
                table: "YeuCauPhucKhao");

            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauSuaDiem_BangDiem_BangDiemMaBangDiem",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauSuaDiem_BangDiemMaBangDiem",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauPhucKhao_BangDiemMaBangDiem",
                table: "YeuCauPhucKhao");

            migrationBuilder.DropColumn(
                name: "BangDiemMaBangDiem",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropColumn(
                name: "BangDiemMaBangDiem",
                table: "YeuCauPhucKhao");

            migrationBuilder.AddColumn<int>(
                name: "MaBangDiem",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaBangDiem",
                table: "YeuCauPhucKhao",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_MaBangDiem",
                table: "YeuCauSuaDiem",
                column: "MaBangDiem");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauPhucKhao_MaBangDiem",
                table: "YeuCauPhucKhao",
                column: "MaBangDiem");

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauPhucKhao_BangDiem_MaBangDiem",
                table: "YeuCauPhucKhao",
                column: "MaBangDiem",
                principalTable: "BangDiem",
                principalColumn: "MaBangDiem",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauSuaDiem_BangDiem_MaBangDiem",
                table: "YeuCauSuaDiem",
                column: "MaBangDiem",
                principalTable: "BangDiem",
                principalColumn: "MaBangDiem",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
