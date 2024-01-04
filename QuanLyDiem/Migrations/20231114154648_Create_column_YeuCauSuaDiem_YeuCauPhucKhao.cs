using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Create_column_YeuCauSuaDiem_YeuCauPhucKhao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YeuCauPhucKhao",
                columns: table => new
                {
                    MaYeuCauPhucKhao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaSinhVien = table.Column<string>(type: "TEXT", nullable: false),
                    MaHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    DiemThi = table.Column<int>(type: "INTEGER", nullable: false),
                    LyDo = table.Column<string>(type: "TEXT", nullable: false),
                    TrangThai = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCauPhucKhao", x => x.MaYeuCauPhucKhao);
                    table.ForeignKey(
                        name: "FK_YeuCauPhucKhao_BangDiem_DiemThi",
                        column: x => x.DiemThi,
                        principalTable: "BangDiem",
                        principalColumn: "MaBangDiem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YeuCauPhucKhao_HocPhan_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "HocPhan",
                        principalColumn: "MaHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YeuCauPhucKhao_SinhVien_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YeuCauSuaDiem",
                columns: table => new
                {
                    MaYeuCauSuaDiem = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaGiangVien = table.Column<string>(type: "TEXT", nullable: false),
                    MaSinhVien = table.Column<string>(type: "TEXT", nullable: false),
                    MaHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    DiemChuyenCan = table.Column<int>(type: "INTEGER", nullable: false),
                    DiemKiemTra = table.Column<int>(type: "INTEGER", nullable: false),
                    DiemChuyenCanMoi = table.Column<int>(type: "INTEGER", nullable: false),
                    DiemKiemTraMoi = table.Column<int>(type: "INTEGER", nullable: false),
                    LyDo = table.Column<string>(type: "TEXT", nullable: false),
                    TrangThai = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCauSuaDiem", x => x.MaYeuCauSuaDiem);
                    table.ForeignKey(
                        name: "FK_YeuCauSuaDiem_BangDiem_DiemKiemTra",
                        column: x => x.DiemKiemTra,
                        principalTable: "BangDiem",
                        principalColumn: "MaBangDiem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YeuCauSuaDiem_GiangVien_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "GiangVien",
                        principalColumn: "MaGiangVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YeuCauSuaDiem_HocPhan_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "HocPhan",
                        principalColumn: "MaHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YeuCauSuaDiem_SinhVien_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauPhucKhao_DiemThi",
                table: "YeuCauPhucKhao",
                column: "DiemThi");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauPhucKhao_MaHocPhan",
                table: "YeuCauPhucKhao",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauPhucKhao_MaSinhVien",
                table: "YeuCauPhucKhao",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_DiemKiemTra",
                table: "YeuCauSuaDiem",
                column: "DiemKiemTra");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_MaGiangVien",
                table: "YeuCauSuaDiem",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_MaHocPhan",
                table: "YeuCauSuaDiem",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaDiem_MaSinhVien",
                table: "YeuCauSuaDiem",
                column: "MaSinhVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YeuCauPhucKhao");

            migrationBuilder.DropTable(
                name: "YeuCauSuaDiem");
        }
    }
}
