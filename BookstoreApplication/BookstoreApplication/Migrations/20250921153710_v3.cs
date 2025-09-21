using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "Author of Harry Potter", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc), "J.K. Rowling" },
                    { 2, "Author of A Song of Ice and Fire", new DateTime(1948, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), "George R.R. Martin" },
                    { 3, "Queen of Mystery", new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Agatha Christie" },
                    { 4, "King of Horror", new DateTime(1947, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Stephen King" },
                    { 5, "Science fiction writer", new DateTime(1920, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Isaac Asimov" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "StartedAt" },
                values: new object[,]
                {
                    { 1, "Award for achievements in journalism and literature.", "Pulitzer Prize", new DateTime(1917, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Award for science fiction or fantasy works.", "Hugo Award", new DateTime(1953, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Annual award for the best science fiction or fantasy works.", "Nebula Award", new DateTime(1965, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Literary prize awarded each year for the best original novel.", "Booker Prize", new DateTime(1969, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "New York, USA", "Penguin Random House", "https://www.penguinrandomhouse.com" },
                    { 2, "New York, USA", "HarperCollins", "https://www.harpercollins.com" },
                    { 3, "London, UK", "Macmillan Publishers", "https://us.macmillan.com" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId", "YearAwarded" },
                values: new object[,]
                {
                    { 1, 1, 2001 },
                    { 1, 2, 2005 },
                    { 1, 4, 2010 },
                    { 2, 2, 1998 },
                    { 2, 3, 2000 },
                    { 2, 4, 2012 },
                    { 3, 1, 1940 },
                    { 3, 2, 1939 },
                    { 3, 4, 1950 },
                    { 4, 1, 1980 },
                    { 4, 2, 1985 },
                    { 4, 3, 1990 },
                    { 5, 2, 1960 },
                    { 5, 3, 1965 },
                    { 5, 4, 1970 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "978-0439708180", 309, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Harry Potter and the Sorcerer's Stone" },
                    { 2, 1, "978-0439064873", 341, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Harry Potter and the Chamber of Secrets" },
                    { 3, 2, "978-0553103540", 694, new DateTime(1996, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), 2, "A Game of Thrones" },
                    { 4, 2, "978-0553108033", 768, new DateTime(1998, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), 2, "A Clash of Kings" },
                    { 5, 3, "978-0062693662", 256, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Murder on the Orient Express" },
                    { 6, 3, "978-0062073488", 272, new DateTime(1939, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3, "And Then There Were None" },
                    { 7, 4, "978-0307743657", 447, new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Shining" },
                    { 8, 4, "978-1501142970", 1138, new DateTime(1986, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, "It" },
                    { 9, 5, "978-0553293357", 255, new DateTime(1951, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Foundation" },
                    { 10, 5, "978-0553294385", 224, new DateTime(1950, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), 2, "I, Robot" },
                    { 11, 4, "978-0307743664", 199, new DateTime(1974, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Carrie" },
                    { 12, 1, "978-0316228534", 503, new DateTime(2012, 9, 27, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Casual Vacancy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
