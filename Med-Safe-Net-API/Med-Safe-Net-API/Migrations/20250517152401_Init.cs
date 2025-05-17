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
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[,]
                {
                    { -4, new DateTime(1952, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "patient@example.com", "Eve", "Doe", new byte[] { 201, 23, 181, 76, 60, 34, 218, 222, 68, 211, 236, 136, 17, 113, 173, 231, 15, 140, 51, 107, 201, 177, 189, 28, 74, 126, 21, 219, 90, 47, 166, 232, 240, 162, 227, 33, 31, 120, 237, 144, 190, 53, 138, 52, 46, 164, 129, 17, 141, 254, 143, 136, 217, 31, 28, 120, 165, 64, 221, 203, 49, 161, 222, 127 }, new byte[] { 20, 224, 135, 30, 129, 199, 15, 238, 5, 172, 144, 192, 253, 129, 39, 9, 169, 13, 163, 121, 173, 111, 33, 47, 32, 200, 188, 99, 217, 98, 32, 242, 236, 158, 255, 187, 214, 157, 192, 212, 169, 116, 86, 131, 201, 255, 246, 105, 209, 169, 182, 66, 155, 167, 88, 245, 247, 150, 118, 211, 150, 196, 217, 196, 208, 116, 246, 217, 142, 149, 171, 5, 87, 211, 101, 196, 135, 149, 230, 215, 48, 6, 202, 82, 130, 129, 113, 187, 206, 233, 162, 38, 89, 150, 251, 25, 237, 32, 133, 116, 106, 130, 130, 145, 120, 124, 199, 26, 124, 201, 34, 76, 59, 165, 109, 60, 103, 112, 77, 42, 130, 10, 39, 38, 160, 242, 191, 6 }, "patient1" },
                    { -3, new DateTime(1992, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "cg2@example.com", "Bob", "Johnson", new byte[] { 201, 23, 181, 76, 60, 34, 218, 222, 68, 211, 236, 136, 17, 113, 173, 231, 15, 140, 51, 107, 201, 177, 189, 28, 74, 126, 21, 219, 90, 47, 166, 232, 240, 162, 227, 33, 31, 120, 237, 144, 190, 53, 138, 52, 46, 164, 129, 17, 141, 254, 143, 136, 217, 31, 28, 120, 165, 64, 221, 203, 49, 161, 222, 127 }, new byte[] { 20, 224, 135, 30, 129, 199, 15, 238, 5, 172, 144, 192, 253, 129, 39, 9, 169, 13, 163, 121, 173, 111, 33, 47, 32, 200, 188, 99, 217, 98, 32, 242, 236, 158, 255, 187, 214, 157, 192, 212, 169, 116, 86, 131, 201, 255, 246, 105, 209, 169, 182, 66, 155, 167, 88, 245, 247, 150, 118, 211, 150, 196, 217, 196, 208, 116, 246, 217, 142, 149, 171, 5, 87, 211, 101, 196, 135, 149, 230, 215, 48, 6, 202, 82, 130, 129, 113, 187, 206, 233, 162, 38, 89, 150, 251, 25, 237, 32, 133, 116, 106, 130, 130, 145, 120, 124, 199, 26, 124, 201, 34, 76, 59, 165, 109, 60, 103, 112, 77, 42, 130, 10, 39, 38, 160, 242, 191, 6 }, "caregiver2" },
                    { -2, new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "cg1@example.com", "Alice", "Smith", new byte[] { 201, 23, 181, 76, 60, 34, 218, 222, 68, 211, 236, 136, 17, 113, 173, 231, 15, 140, 51, 107, 201, 177, 189, 28, 74, 126, 21, 219, 90, 47, 166, 232, 240, 162, 227, 33, 31, 120, 237, 144, 190, 53, 138, 52, 46, 164, 129, 17, 141, 254, 143, 136, 217, 31, 28, 120, 165, 64, 221, 203, 49, 161, 222, 127 }, new byte[] { 20, 224, 135, 30, 129, 199, 15, 238, 5, 172, 144, 192, 253, 129, 39, 9, 169, 13, 163, 121, 173, 111, 33, 47, 32, 200, 188, 99, 217, 98, 32, 242, 236, 158, 255, 187, 214, 157, 192, 212, 169, 116, 86, 131, 201, 255, 246, 105, 209, 169, 182, 66, 155, 167, 88, 245, 247, 150, 118, 211, 150, 196, 217, 196, 208, 116, 246, 217, 142, 149, 171, 5, 87, 211, 101, 196, 135, 149, 230, 215, 48, 6, 202, 82, 130, 129, 113, 187, 206, 233, 162, 38, 89, 150, 251, 25, 237, 32, 133, 116, 106, 130, 130, 145, 120, 124, 199, 26, 124, 201, 34, 76, 59, 165, 109, 60, 103, 112, 77, 42, 130, 10, 39, 38, 160, 242, 191, 6 }, "caregiver1" },
                    { -1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Admin", "User", new byte[] { 201, 23, 181, 76, 60, 34, 218, 222, 68, 211, 236, 136, 17, 113, 173, 231, 15, 140, 51, 107, 201, 177, 189, 28, 74, 126, 21, 219, 90, 47, 166, 232, 240, 162, 227, 33, 31, 120, 237, 144, 190, 53, 138, 52, 46, 164, 129, 17, 141, 254, 143, 136, 217, 31, 28, 120, 165, 64, 221, 203, 49, 161, 222, 127 }, new byte[] { 20, 224, 135, 30, 129, 199, 15, 238, 5, 172, 144, 192, 253, 129, 39, 9, 169, 13, 163, 121, 173, 111, 33, 47, 32, 200, 188, 99, 217, 98, 32, 242, 236, 158, 255, 187, 214, 157, 192, 212, 169, 116, 86, 131, 201, 255, 246, 105, 209, 169, 182, 66, 155, 167, 88, 245, 247, 150, 118, 211, 150, 196, 217, 196, 208, 116, 246, 217, 142, 149, 171, 5, 87, 211, 101, 196, 135, 149, 230, 215, 48, 6, 202, 82, 130, 129, 113, 187, 206, 233, 162, 38, 89, 150, 251, 25, 237, 32, 133, 116, 106, 130, 130, 145, 120, 124, 199, 26, 124, 201, 34, 76, 59, 165, 109, 60, 103, 112, 77, 42, 130, 10, 39, 38, 160, 242, 191, 6 }, "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "AppRoleId", "Id" },
                values: new object[,]
                {
                    { -3, 1, -3 },
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
                name: "IX_UserRoles_AppRoleId",
                table: "UserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_Id",
                table: "UserRoles",
                column: "Id");
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
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
