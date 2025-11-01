using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ignoreNormalizedUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
