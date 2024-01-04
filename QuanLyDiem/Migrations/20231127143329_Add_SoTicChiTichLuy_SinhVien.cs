using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Add_SoTicChiTichLuy_SinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoTinChiTichLuy",
                table: "SinhVien",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SinhVienMaSinhVien",
                table: "HocPhan",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HocPhan_SinhVienMaSinhVien",
                table: "HocPhan",
                column: "SinhVienMaSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhan_SinhVien_SinhVienMaSinhVien",
                table: "HocPhan",
                column: "SinhVienMaSinhVien",
                principalTable: "SinhVien",
                principalColumn: "MaSinhVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocPhan_SinhVien_SinhVienMaSinhVien",
                table: "HocPhan");

            migrationBuilder.DropIndex(
                name: "IX_HocPhan_SinhVienMaSinhVien",
                table: "HocPhan");

            migrationBuilder.DropColumn(
                name: "SoTinChiTichLuy",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "SinhVienMaSinhVien",
                table: "HocPhan");
        }
    }
}
