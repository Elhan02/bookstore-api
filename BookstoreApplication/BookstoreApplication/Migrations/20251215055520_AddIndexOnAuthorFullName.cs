using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnAuthorFullName : Migration
    {
        /// <inheritdoc />
        /// Ubacen je raw sql up i down zbog toLower i trim-a, inace index nece raditi
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE INDEX ""IX_Authors_FullName_LowerTrim"" 
                ON ""Authors"" (LOWER(BTRIM(""FullName"", E' \t\n\r')));"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP INDEX IF EXISTS ""IX_Authors_FullName_LowerTrim"";"
            );
        }
    }
}
