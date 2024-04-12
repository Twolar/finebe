using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace finebe_api.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b2455efb-fd98-4994-b847-d1cb90172ae8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e790f70b-d4ae-4fb4-8fd8-4de3eb77d844"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dbba3695-3a0a-4c4f-9a4f-7f37d4af388c"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("02115383-d61c-40c6-b39d-82c2f3b3f575"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("04109747-2a90-4360-916c-c5013f59e179"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("1d28a00e-11d4-4356-8d5f-0df5514c8e9b"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("862e2a88-3be5-4b5d-b0b9-e8c055ff334f"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("94005f6a-11a5-4049-8913-7593014b420b"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("e7f5e8e8-364b-487d-8fdc-142d2e938dd9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23940176-5b4b-422f-9b82-2e74f18620e8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("422aac77-9248-4663-adfe-a95137bda5fc"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8be9daeb-9c8b-4ed3-b638-fc3c47520384"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("b2455efb-fd98-4994-b847-d1cb90172ae8"), null, "User", "USER" },
                    { new Guid("e790f70b-d4ae-4fb4-8fd8-4de3eb77d844"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("23940176-5b4b-422f-9b82-2e74f18620e8"), 0, "68806551-c54c-4841-acff-53edaf1d2b11", "user3@example.com", false, false, null, null, null, null, null, false, null, false, "user3@example.com" },
                    { new Guid("422aac77-9248-4663-adfe-a95137bda5fc"), 0, "6a4243e3-9a3c-4065-a7c5-45fae3cb7135", "user1@example.com", false, false, null, null, null, null, null, false, null, false, "user1@example.com" },
                    { new Guid("8be9daeb-9c8b-4ed3-b638-fc3c47520384"), 0, "d3e876a0-9c64-4fc7-9e3e-2763f126b311", "user2@example.com", false, false, null, null, null, null, null, false, null, false, "user2@example.com" },
                    { new Guid("dbba3695-3a0a-4c4f-9a4f-7f37d4af388c"), 0, "e810787d-0140-4bad-9b7e-14ebe621927d", "taylor@finebe.com", true, false, null, "TAYLOR@FINEBE.COM", "TAYLOR@FINEBE.COM", "AQAAAAIAAYagAAAAEItbF72xNnhCDVK7Z5jjFO1QnGOhVN9kFet+QIgtJAdLEblojsy/23NqpQoWU5/XWg==", null, false, null, false, "taylor@finebe.com" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Amount", "UserId" },
                values: new object[,]
                {
                    { new Guid("02115383-d61c-40c6-b39d-82c2f3b3f575"), 10m, new Guid("422aac77-9248-4663-adfe-a95137bda5fc") },
                    { new Guid("04109747-2a90-4360-916c-c5013f59e179"), 60m, new Guid("23940176-5b4b-422f-9b82-2e74f18620e8") },
                    { new Guid("1d28a00e-11d4-4356-8d5f-0df5514c8e9b"), 70m, new Guid("8be9daeb-9c8b-4ed3-b638-fc3c47520384") },
                    { new Guid("862e2a88-3be5-4b5d-b0b9-e8c055ff334f"), 20m, new Guid("23940176-5b4b-422f-9b82-2e74f18620e8") },
                    { new Guid("94005f6a-11a5-4049-8913-7593014b420b"), 70m, new Guid("422aac77-9248-4663-adfe-a95137bda5fc") },
                    { new Guid("e7f5e8e8-364b-487d-8fdc-142d2e938dd9"), 70m, new Guid("8be9daeb-9c8b-4ed3-b638-fc3c47520384") }
                });
        }
    }
}
