using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeCMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class settingcorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "Setting");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "Setting",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
