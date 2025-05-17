using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Med_Safe_Net_API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    AppRoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AppRoleName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.AppRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserCode = table.Column<Guid>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeartRates",
                columns: table => new
                {
                    HeartRateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Measurement = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeartRates", x => x.HeartRateId);
                    table.ForeignKey(
                        name: "FK_HeartRates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HighHeartRates",
                columns: table => new
                {
                    HighHeartRateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Measurement = table.Column<double>(type: "REAL", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Confirm = table.Column<bool>(type: "INTEGER", nullable: true),
                    TimeOfConfirmation = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighHeartRates", x => x.HighHeartRateId);
                    table.ForeignKey(
                        name: "FK_HighHeartRates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuddenMovements",
                columns: table => new
                {
                    MovementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Confirm = table.Column<bool>(type: "INTEGER", nullable: true),
                    TimeOfConfirmation = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuddenMovements", x => x.MovementId);
                    table.ForeignKey(
                        name: "FK_SuddenMovements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLinks",
                columns: table => new
                {
                    UserLinkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CareGiverId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinks", x => x.UserLinkId);
                    table.ForeignKey(
                        name: "FK_UserLinks_Users_CareGiverId",
                        column: x => x.CareGiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLinks_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    AppRoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_AppRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "AppRoles",
                        principalColumn: "AppRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "AppRoleId", "AppRoleName" },
                values: new object[,]
                {
                    { 1, "Patient" },
                    { 2, "Caregiver" },
                    { 3, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UserCode", "Username" },
                values: new object[,]
                {
                    { -5, new DateTime(1962, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "patient2@example.com", "John", "Doe", new byte[] { 20, 196, 210, 15, 109, 54, 194, 255, 98, 194, 116, 98, 140, 56, 152, 72, 188, 204, 141, 251, 122, 71, 56, 119, 176, 210, 226, 17, 230, 17, 100, 99, 14, 117, 230, 236, 144, 178, 29, 226, 26, 172, 4, 118, 26, 159, 192, 122, 39, 226, 114, 181, 95, 214, 214, 150, 78, 108, 47, 178, 134, 80, 54, 4 }, new byte[] { 219, 72, 188, 153, 201, 97, 63, 7, 223, 149, 139, 92, 149, 109, 38, 125, 112, 228, 184, 218, 97, 134, 32, 129, 37, 29, 96, 116, 228, 15, 161, 56, 150, 240, 72, 222, 53, 145, 70, 100, 111, 144, 98, 53, 80, 66, 132, 151, 195, 19, 54, 179, 174, 54, 134, 252, 23, 133, 95, 56, 161, 57, 248, 45, 27, 231, 194, 246, 132, 181, 46, 109, 110, 85, 90, 66, 145, 77, 120, 44, 142, 240, 207, 248, 203, 254, 158, 127, 20, 238, 216, 219, 97, 245, 65, 18, 223, 231, 186, 89, 233, 181, 154, 112, 76, 83, 59, 154, 183, 255, 3, 87, 187, 64, 75, 142, 72, 28, 13, 92, 252, 150, 94, 118, 185, 45, 167, 11 }, new Guid("657bbfc8-37ae-41b6-b602-103832142593"), "patient2" },
                    { -4, new DateTime(1952, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "patient@example.com", "Eve", "Doe", new byte[] { 20, 196, 210, 15, 109, 54, 194, 255, 98, 194, 116, 98, 140, 56, 152, 72, 188, 204, 141, 251, 122, 71, 56, 119, 176, 210, 226, 17, 230, 17, 100, 99, 14, 117, 230, 236, 144, 178, 29, 226, 26, 172, 4, 118, 26, 159, 192, 122, 39, 226, 114, 181, 95, 214, 214, 150, 78, 108, 47, 178, 134, 80, 54, 4 }, new byte[] { 219, 72, 188, 153, 201, 97, 63, 7, 223, 149, 139, 92, 149, 109, 38, 125, 112, 228, 184, 218, 97, 134, 32, 129, 37, 29, 96, 116, 228, 15, 161, 56, 150, 240, 72, 222, 53, 145, 70, 100, 111, 144, 98, 53, 80, 66, 132, 151, 195, 19, 54, 179, 174, 54, 134, 252, 23, 133, 95, 56, 161, 57, 248, 45, 27, 231, 194, 246, 132, 181, 46, 109, 110, 85, 90, 66, 145, 77, 120, 44, 142, 240, 207, 248, 203, 254, 158, 127, 20, 238, 216, 219, 97, 245, 65, 18, 223, 231, 186, 89, 233, 181, 154, 112, 76, 83, 59, 154, 183, 255, 3, 87, 187, 64, 75, 142, 72, 28, 13, 92, 252, 150, 94, 118, 185, 45, 167, 11 }, new Guid("77fa3ba7-1a22-4fd4-8dce-28839ec9aab0"), "patient1" },
                    { -3, new DateTime(1992, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "cg2@example.com", "Bob", "Johnson", new byte[] { 20, 196, 210, 15, 109, 54, 194, 255, 98, 194, 116, 98, 140, 56, 152, 72, 188, 204, 141, 251, 122, 71, 56, 119, 176, 210, 226, 17, 230, 17, 100, 99, 14, 117, 230, 236, 144, 178, 29, 226, 26, 172, 4, 118, 26, 159, 192, 122, 39, 226, 114, 181, 95, 214, 214, 150, 78, 108, 47, 178, 134, 80, 54, 4 }, new byte[] { 219, 72, 188, 153, 201, 97, 63, 7, 223, 149, 139, 92, 149, 109, 38, 125, 112, 228, 184, 218, 97, 134, 32, 129, 37, 29, 96, 116, 228, 15, 161, 56, 150, 240, 72, 222, 53, 145, 70, 100, 111, 144, 98, 53, 80, 66, 132, 151, 195, 19, 54, 179, 174, 54, 134, 252, 23, 133, 95, 56, 161, 57, 248, 45, 27, 231, 194, 246, 132, 181, 46, 109, 110, 85, 90, 66, 145, 77, 120, 44, 142, 240, 207, 248, 203, 254, 158, 127, 20, 238, 216, 219, 97, 245, 65, 18, 223, 231, 186, 89, 233, 181, 154, 112, 76, 83, 59, 154, 183, 255, 3, 87, 187, 64, 75, 142, 72, 28, 13, 92, 252, 150, 94, 118, 185, 45, 167, 11 }, new Guid("0312b76d-6351-4b5a-bc41-00f69e21f6e0"), "caregiver2" },
                    { -2, new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "cg1@example.com", "Alice", "Smith", new byte[] { 20, 196, 210, 15, 109, 54, 194, 255, 98, 194, 116, 98, 140, 56, 152, 72, 188, 204, 141, 251, 122, 71, 56, 119, 176, 210, 226, 17, 230, 17, 100, 99, 14, 117, 230, 236, 144, 178, 29, 226, 26, 172, 4, 118, 26, 159, 192, 122, 39, 226, 114, 181, 95, 214, 214, 150, 78, 108, 47, 178, 134, 80, 54, 4 }, new byte[] { 219, 72, 188, 153, 201, 97, 63, 7, 223, 149, 139, 92, 149, 109, 38, 125, 112, 228, 184, 218, 97, 134, 32, 129, 37, 29, 96, 116, 228, 15, 161, 56, 150, 240, 72, 222, 53, 145, 70, 100, 111, 144, 98, 53, 80, 66, 132, 151, 195, 19, 54, 179, 174, 54, 134, 252, 23, 133, 95, 56, 161, 57, 248, 45, 27, 231, 194, 246, 132, 181, 46, 109, 110, 85, 90, 66, 145, 77, 120, 44, 142, 240, 207, 248, 203, 254, 158, 127, 20, 238, 216, 219, 97, 245, 65, 18, 223, 231, 186, 89, 233, 181, 154, 112, 76, 83, 59, 154, 183, 255, 3, 87, 187, 64, 75, 142, 72, 28, 13, 92, 252, 150, 94, 118, 185, 45, 167, 11 }, new Guid("6571d10f-90b1-4785-b151-fb908cdb6e34"), "caregiver1" },
                    { -1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Admin", "User", new byte[] { 20, 196, 210, 15, 109, 54, 194, 255, 98, 194, 116, 98, 140, 56, 152, 72, 188, 204, 141, 251, 122, 71, 56, 119, 176, 210, 226, 17, 230, 17, 100, 99, 14, 117, 230, 236, 144, 178, 29, 226, 26, 172, 4, 118, 26, 159, 192, 122, 39, 226, 114, 181, 95, 214, 214, 150, 78, 108, 47, 178, 134, 80, 54, 4 }, new byte[] { 219, 72, 188, 153, 201, 97, 63, 7, 223, 149, 139, 92, 149, 109, 38, 125, 112, 228, 184, 218, 97, 134, 32, 129, 37, 29, 96, 116, 228, 15, 161, 56, 150, 240, 72, 222, 53, 145, 70, 100, 111, 144, 98, 53, 80, 66, 132, 151, 195, 19, 54, 179, 174, 54, 134, 252, 23, 133, 95, 56, 161, 57, 248, 45, 27, 231, 194, 246, 132, 181, 46, 109, 110, 85, 90, 66, 145, 77, 120, 44, 142, 240, 207, 248, 203, 254, 158, 127, 20, 238, 216, 219, 97, 245, 65, 18, 223, 231, 186, 89, 233, 181, 154, 112, 76, 83, 59, 154, 183, 255, 3, 87, 187, 64, 75, 142, 72, 28, 13, 92, 252, 150, 94, 118, 185, 45, 167, 11 }, new Guid("9d7ec4b8-4a55-41e5-a5ce-c06e8de95b2c"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserLinks",
                columns: new[] { "UserLinkId", "CareGiverId", "PatientId" },
                values: new object[,]
                {
                    { -5, -1, -5 },
                    { -4, -1, -4 },
                    { -3, -2, -5 },
                    { -2, -3, -4 },
                    { -1, -2, -4 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "AppRoleId", "Id" },
                values: new object[,]
                {
                    { -5, 1, -5 },
                    { -4, 1, -4 },
                    { -3, 2, -3 },
                    { -2, 2, -2 },
                    { -1, 3, -1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeartRates_UserId",
                table: "HeartRates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HighHeartRates_UserId",
                table: "HighHeartRates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuddenMovements_UserId",
                table: "SuddenMovements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLinks_CareGiverId",
                table: "UserLinks",
                column: "CareGiverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLinks_PatientId_CareGiverId",
                table: "UserLinks",
                columns: new[] { "PatientId", "CareGiverId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AppRoleId",
                table: "UserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_Id",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserCode",
                table: "Users",
                column: "UserCode",
                unique: true,
                filter: "[UserCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeartRates");

            migrationBuilder.DropTable(
                name: "HighHeartRates");

            migrationBuilder.DropTable(
                name: "SuddenMovements");

            migrationBuilder.DropTable(
                name: "UserLinks");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
