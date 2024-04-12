using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace finebe_api.Migrations
{
    /// <inheritdoc />
    public partial class Reverse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("50013fcd-4f28-46e1-947e-f947b8d857d6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b6012192-27d1-4f59-8fe8-5433d4766eed"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("43c724bb-2e48-46e2-92a8-2e5e45fe7a3e"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("3b2faac0-1c32-4f84-a842-fb3bb11d2c2e"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("421971e7-b1c9-41bf-8661-219cf6cf71f8"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("46a5c00c-696f-449e-aaa1-4badda8d52a1"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("86f12a17-6eae-4606-8ac5-ea5d0c640e5c"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("af350eba-a57b-4f62-8dc9-28c205ef1f6a"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("e8eaad20-3c28-40d5-a9a9-6f96150d8b91"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4bc7eace-6197-4afe-84b5-e7feb09bcf4f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8aac624f-a855-4df3-a844-886137b5f9e9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0dc76f1-06f9-46e1-b509-2185f190632d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("15ba19e6-42fa-4fd5-989f-08684f82bd66"), null, "User", "USER" },
                    { new Guid("fc35a386-1a5b-4c4e-bf77-f63db59dc110"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("318718b3-1f3b-44d9-b492-5f825e2dcc40"), 0, "c9ebccd3-47b8-4798-ba71-eb3da10763fa", "taylor@finebe.com", true, false, null, "TAYLOR@FINEBE.COM", "TAYLOR@FINEBE.COM", "AQAAAAIAAYagAAAAEJCY7244OLwWrrg4jJVR6xwNoiYu396VrEQZlPA7FrogfoGo35ftzNQA7kN177xkMg==", null, false, null, false, "taylor@finebe.com" },
                    { new Guid("40dcb88d-4553-418f-abdc-3e58444eaa99"), 0, "c0be062a-819c-41c2-9b31-6abb87299da2", "user1@example.com", false, false, null, null, null, null, null, false, null, false, "user1@example.com" },
                    { new Guid("43edd289-fe6a-4bbc-b153-b0639c050a34"), 0, "00765f0e-ba32-493b-a91c-427391167fd2", "user2@example.com", false, false, null, null, null, null, null, false, null, false, "user2@example.com" },
                    { new Guid("95879a61-c88f-4bfd-ac7f-dec1c4bd272f"), 0, "e61e1ed2-3633-4a2a-9764-702a27a474a5", "user3@example.com", false, false, null, null, null, null, null, false, null, false, "user3@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Amount", "UserId" },
                values: new object[,]
                {
                    { new Guid("005a60c8-ac99-4ace-b9a1-ff9017a0026b"), 40m, new Guid("40dcb88d-4553-418f-abdc-3e58444eaa99") },
                    { new Guid("4318c874-d2b5-49a0-8da3-b7797bcc730d"), 80m, new Guid("95879a61-c88f-4bfd-ac7f-dec1c4bd272f") },
                    { new Guid("c1b878f6-d6af-45d0-8231-31940ff041f3"), 80m, new Guid("95879a61-c88f-4bfd-ac7f-dec1c4bd272f") },
                    { new Guid("ca3c5ea1-5245-4e62-9a31-e488e247aebc"), 20m, new Guid("43edd289-fe6a-4bbc-b153-b0639c050a34") },
                    { new Guid("dc93f6e3-753b-4421-a3d6-91ae2ff6606f"), 50m, new Guid("40dcb88d-4553-418f-abdc-3e58444eaa99") },
                    { new Guid("e841ef88-c91b-4bef-a64e-1f404c058a98"), 30m, new Guid("43edd289-fe6a-4bbc-b153-b0639c050a34") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15ba19e6-42fa-4fd5-989f-08684f82bd66"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc35a386-1a5b-4c4e-bf77-f63db59dc110"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("318718b3-1f3b-44d9-b492-5f825e2dcc40"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("005a60c8-ac99-4ace-b9a1-ff9017a0026b"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("4318c874-d2b5-49a0-8da3-b7797bcc730d"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("c1b878f6-d6af-45d0-8231-31940ff041f3"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca3c5ea1-5245-4e62-9a31-e488e247aebc"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("dc93f6e3-753b-4421-a3d6-91ae2ff6606f"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("e841ef88-c91b-4bef-a64e-1f404c058a98"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("40dcb88d-4553-418f-abdc-3e58444eaa99"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("43edd289-fe6a-4bbc-b153-b0639c050a34"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95879a61-c88f-4bfd-ac7f-dec1c4bd272f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("50013fcd-4f28-46e1-947e-f947b8d857d6"), null, "User", "USER" },
                    { new Guid("b6012192-27d1-4f59-8fe8-5433d4766eed"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("43c724bb-2e48-46e2-92a8-2e5e45fe7a3e"), 0, "13f08acd-e59e-48f2-accd-be7456b768a4", "taylor@finebe.com", true, false, null, "TAYLOR@FINEBE.COM", "TAYLOR@FINEBE.COM", "AQAAAAIAAYagAAAAECH5/JhSF+X82DT5Tuy8uaEgDgv/nIzg+kXpezodCcSw/5h75erw3zENm96zhW3bNw==", null, false, null, false, "taylor@finebe.com" },
                    { new Guid("4bc7eace-6197-4afe-84b5-e7feb09bcf4f"), 0, "8b7fc59e-eacf-4bce-b29b-e79ad8ed2d6c", "user1@example.com", false, false, null, null, null, null, null, false, null, false, "user1@example.com" },
                    { new Guid("8aac624f-a855-4df3-a844-886137b5f9e9"), 0, "c983e8ee-ab37-4e08-a31c-98a2af35593c", "user3@example.com", false, false, null, null, null, null, null, false, null, false, "user3@example.com" },
                    { new Guid("a0dc76f1-06f9-46e1-b509-2185f190632d"), 0, "4d93b8ec-6048-400b-ba10-d59e8142ae2b", "user2@example.com", false, false, null, null, null, null, null, false, null, false, "user2@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Amount", "UserId" },
                values: new object[,]
                {
                    { new Guid("3b2faac0-1c32-4f84-a842-fb3bb11d2c2e"), 80m, new Guid("a0dc76f1-06f9-46e1-b509-2185f190632d") },
                    { new Guid("421971e7-b1c9-41bf-8661-219cf6cf71f8"), 10m, new Guid("4bc7eace-6197-4afe-84b5-e7feb09bcf4f") },
                    { new Guid("46a5c00c-696f-449e-aaa1-4badda8d52a1"), 10m, new Guid("8aac624f-a855-4df3-a844-886137b5f9e9") },
                    { new Guid("86f12a17-6eae-4606-8ac5-ea5d0c640e5c"), 30m, new Guid("4bc7eace-6197-4afe-84b5-e7feb09bcf4f") },
                    { new Guid("af350eba-a57b-4f62-8dc9-28c205ef1f6a"), 50m, new Guid("a0dc76f1-06f9-46e1-b509-2185f190632d") },
                    { new Guid("e8eaad20-3c28-40d5-a9a9-6f96150d8b91"), 60m, new Guid("8aac624f-a855-4df3-a844-886137b5f9e9") }
                });
        }
    }
}
