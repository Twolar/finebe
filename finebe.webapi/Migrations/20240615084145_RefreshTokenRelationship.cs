using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finebe.webapi.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b1af8e0-eb6b-4b4e-8d2f-e95aa5347cd2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3338d8bf-167c-492b-9715-d24e159412b1", "AQAAAAIAAYagAAAAEIGNf3jl+0GJQdwFIwr8pGbcKipkJN2Oy6+sObKcAfZCZeD6HMhJ1VmgN1vjZa8gog==" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b1af8e0-eb6b-4b4e-8d2f-e95aa5347cd2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47b7a64b-2551-4245-9835-dce0b5b4612c", "AQAAAAIAAYagAAAAEFVho9mosyIhER8NsSLmKCwtCuJTqC89aIDE3MnsqbOMmynuacei4XXwxBKfCu1Byg==" });
        }
    }
}
