using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Create_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocKy",
                columns: table => new
                {
                    MaHocKy = table.Column<string>(type: "TEXT", nullable: false),
                    TenHocKy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKy", x => x.MaHocKy);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    MaKhoa = table.Column<string>(type: "TEXT", nullable: false),
                    TenKhoa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "KhoaHoc",
                columns: table => new
                {
                    MaKhoaHoc = table.Column<string>(type: "TEXT", nullable: false),
                    TenKhoaHoc = table.Column<string>(type: "TEXT", nullable: false),
                    NamBatDau = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NamKetThuc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoaHoc", x => x.MaKhoaHoc);
                });

            migrationBuilder.CreateTable(
                name: "ChuyenNganh",
                columns: table => new
                {
                    MaChuyenNganh = table.Column<string>(type: "TEXT", nullable: false),
                    TenChuyenNganh = table.Column<string>(type: "TEXT", nullable: false),
                    MaKhoa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenNganh", x => x.MaChuyenNganh);
                    table.ForeignKey(
                        name: "FK_ChuyenNganh_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    MaGiangVien = table.Column<string>(type: "TEXT", nullable: false),
                    TenGiangVien = table.Column<string>(type: "TEXT", nullable: false),
                    GioiTinh = table.Column<bool>(type: "INTEGER", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaKhoa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien", x => x.MaGiangVien);
                    table.ForeignKey(
                        name: "FK_GiangVien_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HocPhan",
                columns: table => new
                {
                    MaHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    TenHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    SoTinChi = table.Column<int>(type: "INTEGER", nullable: false),
                    MaChuyenNganh = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhan", x => x.MaHocPhan);
                    table.ForeignKey(
                        name: "FK_HocPhan_ChuyenNganh_MaChuyenNganh",
                        column: x => x.MaChuyenNganh,
                        principalTable: "ChuyenNganh",
                        principalColumn: "MaChuyenNganh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSinhVien = table.Column<string>(type: "TEXT", nullable: false),
                    TenSinhVien = table.Column<string>(type: "TEXT", nullable: false),
                    GioiTinh = table.Column<bool>(type: "INTEGER", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TinhTrang = table.Column<string>(type: "TEXT", nullable: false),
                    MaChuyenNganh = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSinhVien);
                    table.ForeignKey(
                        name: "FK_SinhVien_ChuyenNganh_MaChuyenNganh",
                        column: x => x.MaChuyenNganh,
                        principalTable: "ChuyenNganh",
                        principalColumn: "MaChuyenNganh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhan",
                columns: table => new
                {
                    MaLopHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    TenLopHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    MaHocPhan = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhan", x => x.MaLopHocPhan);
                    table.ForeignKey(
                        name: "FK_LopHocPhan_HocPhan_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "HocPhan",
                        principalColumn: "MaHocPhan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BangDiem",
                columns: table => new
                {
                    MaBangDiem = table.Column<string>(type: "TEXT", nullable: false),
                    MaSinhVien = table.Column<string>(type: "TEXT", nullable: false),
                    MaHocPhan = table.Column<string>(type: "TEXT", nullable: false),
                    DiemChuyenCan = table.Column<string>(type: "TEXT", nullable: false),
                    DiemKiemTra = table.Column<string>(type: "TEXT", nullable: false),
                    DiemThi = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangDiem", x => x.MaBangDiem);
                    table.ForeignKey(
                        name: "FK_BangDiem_HocPhan_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "HocPhan",
                        principalColumn: "MaHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BangDiem_SinhVien_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangDiem_MaHocPhan",
                table: "BangDiem",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_BangDiem_MaSinhVien",
                table: "BangDiem",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenNganh_MaKhoa",
                table: "ChuyenNganh",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_MaKhoa",
                table: "GiangVien",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhan_MaChuyenNganh",
                table: "HocPhan",
                column: "MaChuyenNganh");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_MaHocPhan",
                table: "LopHocPhan",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaChuyenNganh",
                table: "SinhVien",
                column: "MaChuyenNganh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangDiem");

            migrationBuilder.DropTable(
                name: "GiangVien");

            migrationBuilder.DropTable(
                name: "HocKy");

            migrationBuilder.DropTable(
                name: "KhoaHoc");

            migrationBuilder.DropTable(
                name: "LopHocPhan");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "HocPhan");

            migrationBuilder.DropTable(
                name: "ChuyenNganh");

            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}
