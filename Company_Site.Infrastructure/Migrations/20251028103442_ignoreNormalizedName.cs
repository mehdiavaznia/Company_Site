using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ignoreNormalizedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
