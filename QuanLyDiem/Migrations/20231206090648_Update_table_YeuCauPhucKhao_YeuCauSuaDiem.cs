using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Update_table_YeuCauPhucKhao_YeuCauSuaDiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauPhucKhao_BangDiem_DiemThi",
                table: "YeuCauPhucKhao");

            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauSuaDiem_BangDiem_DiemChuyenCan",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauSuaDiem_DiemChuyenCan",
                table: "YeuCauSuaDiem");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauPhucKhao_DiemThi",
                table: "YeuCauPhucKhao");

            migrationBuilder.AlterColumn<double>(
                name: "DiemKiemTraMoi",
                table: "YeuCauSuaDiem",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "DiemKiemTra",
                table: "YeuCauSuaDiem",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "DiemChuyenCanMoi",
                table: "YeuCauSuaDiem",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "DiemChuyenCan",
                table: "YeuCauSuaDiem",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "MaBangDiem",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "DiemThi",
                table: "YeuCauPhucKhao",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "DiemKiemTraMoi",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "DiemKiemTra",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "DiemChuyenCanMoi",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "DiemChuyenCan",
                table: "YeuCauSuaDiem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "DiemThi",
                table: "YeuCauPhucKhao",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_DiemChuyenCan",
                table: "YeuCauSuaDiem",
                column: "DiemChuyenCan");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauPhucKhao_DiemThi",
                table: "YeuCauPhucKhao",
                column: "DiemThi");

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauPhucKhao_BangDiem_DiemThi",
                table: "YeuCauPhucKhao",
                column: "DiemThi",
                principalTable: "BangDiem",
                principalColumn: "MaBangDiem",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauSuaDiem_BangDiem_DiemChuyenCan",
                table: "YeuCauSuaDiem",
                column: "DiemChuyenCan",
                principalTable: "BangDiem",
                principalColumn: "MaBangDiem",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
