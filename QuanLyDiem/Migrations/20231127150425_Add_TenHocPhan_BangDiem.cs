using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDiem.Migrations
{
    /// <inheritdoc />
    public partial class Add_TenHocPhan_BangDiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenHocPhan",
                table: "BangDiem",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenHocPhan",
                table: "BangDiem");
        }
    }
}
