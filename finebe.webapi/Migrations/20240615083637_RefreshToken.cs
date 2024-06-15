using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finebe.webapi.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRevoked = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b1af8e0-eb6b-4b4e-8d2f-e95aa5347cd2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47b7a64b-2551-4245-9835-dce0b5b4612c", "AQAAAAIAAYagAAAAEFVho9mosyIhER8NsSLmKCwtCuJTqC89aIDE3MnsqbOMmynuacei4XXwxBKfCu1Byg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b1af8e0-eb6b-4b4e-8d2f-e95aa5347cd2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0fc2ef0-7502-4c74-835c-3f05dc6f17a6", "AQAAAAIAAYagAAAAEMcBknE39GtS0nqs0B/Sus5+r2eifH3m9+1gorXZWw8tfHIqn/O2YGmkKbv73yjuOg==" });
        }
    }
}
