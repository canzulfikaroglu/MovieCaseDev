using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCaseDev.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApiPageNumber",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiPageNumber",
                table: "Movies");
        }
    }
}
