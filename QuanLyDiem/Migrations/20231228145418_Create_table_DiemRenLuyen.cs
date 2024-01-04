using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_DiemRenLuyen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiemRenLuyen",
                columns: table => new
                {
                    MaDiemRenLuyen = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiemRL = table.Column<int>(type: "INTEGER", nullable: false),
                    MaSinhVien = table.Column<string>(type: "TEXT", nullable: false),
                    MaHocKy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemRenLuyen", x => x.MaDiemRenLuyen);
                    table.ForeignKey(
                        name: "FK_DiemRenLuyen_HocKy_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "HocKy",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiemRenLuyen_SinhVien_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiemRenLuyen_MaHocKy",
                table: "DiemRenLuyen",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_DiemRenLuyen_MaSinhVien",
                table: "DiemRenLuyen",
                column: "MaSinhVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiemRenLuyen");
        }
    }
}
