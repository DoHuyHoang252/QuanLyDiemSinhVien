using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Update_BangDiem_SinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DTBTLHe10",
                table: "SinhVien",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DTBTLHe4",
                table: "SinhVien",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MaLopHocPhan",
                table: "BangDiem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SoTinChi",
                table: "BangDiem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BangDiem_MaLopHocPhan",
                table: "BangDiem",
                column: "MaLopHocPhan");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BangDiem_LopHocPhan_MaLopHocPhan",
                table: "BangDiem");

            migrationBuilder.DropIndex(
                name: "IX_BangDiem_MaLopHocPhan",
                table: "BangDiem");

            migrationBuilder.DropColumn(
                name: "DTBTLHe10",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "DTBTLHe4",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "MaLopHocPhan",
                table: "BangDiem");

            migrationBuilder.DropColumn(
                name: "SoTinChi",
                table: "BangDiem");
        }
    }
}
