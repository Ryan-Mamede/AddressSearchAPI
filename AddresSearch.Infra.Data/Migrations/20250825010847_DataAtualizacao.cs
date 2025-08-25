using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddressSearch.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataAtualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataAtulaizacao",
                table: "Localizacao",
                newName: "DataAtualizacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "Localizacao",
                newName: "DataAtulaizacao");
        }
    }
}
