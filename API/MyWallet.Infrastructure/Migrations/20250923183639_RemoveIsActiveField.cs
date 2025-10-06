using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsActiveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Persons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Persons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
