using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Add_TenSinhVien_BangDiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenSinhVien",
                table: "BangDiem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenSinhVien",
                table: "BangDiem");
        }
    }
}
