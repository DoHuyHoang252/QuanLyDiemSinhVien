using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Update_Models_Create_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaKhoaHoc",
                table: "SinhVien",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaGiangVien",
                table: "LopHocPhan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaHocKy",
                table: "LopHocPhan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaKhoaHoc",
                table: "HocKy",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaChuyenNganh",
                table: "GiangVien",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MaBangDiem",
                table: "BangDiem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaKhoaHoc",
                table: "SinhVien",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_MaGiangVien",
                table: "LopHocPhan",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_MaHocKy",
                table: "LopHocPhan",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_HocKy_MaKhoaHoc",
                table: "HocKy",
                column: "MaKhoaHoc");

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_MaChuyenNganh",
                table: "GiangVien",
                column: "MaChuyenNganh");

            migrationBuilder.AddForeignKey(
                name: "FK_GiangVien_ChuyenNganh_MaChuyenNganh",
                table: "GiangVien",
                column: "MaChuyenNganh",
                principalTable: "ChuyenNganh",
                principalColumn: "MaChuyenNganh",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HocKy_KhoaHoc_MaKhoaHoc",
                table: "HocKy",
                column: "MaKhoaHoc",
                principalTable: "KhoaHoc",
                principalColumn: "MaKhoaHoc",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_HocKy_MaHocKy",
                table: "LopHocPhan",
                column: "MaHocKy",
                principalTable: "HocKy",
                principalColumn: "MaHocKy",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_GiangVien_MaGiangVien",
                table: "LopHocPhan",
                column: "MaGiangVien",
                principalTable: "GiangVien",
                principalColumn: "MaGiangVien",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_KhoaHoc_MaKhoaHoc",
                table: "SinhVien",
                column: "MaKhoaHoc",
                principalTable: "KhoaHoc",
                principalColumn: "MaKhoaHoc",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangVien_ChuyenNganh_MaChuyenNganh",
                table: "GiangVien");

            migrationBuilder.DropForeignKey(
                name: "FK_HocKy_KhoaHoc_MaKhoaHoc",
                table: "HocKy");

            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_GiangVien_MaGiangVien",
                table: "LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_HocKy_MaHocKy",
                table: "LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_KhoaHoc_MaKhoaHoc",
                table: "SinhVien");

            migrationBuilder.DropIndex(
                name: "IX_SinhVien_MaKhoaHoc",
                table: "SinhVien");

            migrationBuilder.DropIndex(
                name: "IX_LopHocPhan_MaGiangVien",
                table: "LopHocPhan");

            migrationBuilder.DropIndex(
                name: "IX_LopHocPhan_MaHocKy",
                table: "LopHocPhan");

            migrationBuilder.DropIndex(
                name: "IX_HocKy_MaKhoaHoc",
                table: "HocKy");

            migrationBuilder.DropIndex(
                name: "IX_GiangVien_MaChuyenNganh",
                table: "GiangVien");

            migrationBuilder.DropColumn(
                name: "MaKhoaHoc",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "MaGiangVien",
                table: "LopHocPhan");

            migrationBuilder.DropColumn(
                name: "MaHocKy",
                table: "LopHocPhan");

            migrationBuilder.DropColumn(
                name: "MaKhoaHoc",
                table: "HocKy");

            migrationBuilder.DropColumn(
                name: "MaChuyenNganh",
                table: "GiangVien");

            migrationBuilder.AlterColumn<string>(
                name: "MaBangDiem",
                table: "BangDiem",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
