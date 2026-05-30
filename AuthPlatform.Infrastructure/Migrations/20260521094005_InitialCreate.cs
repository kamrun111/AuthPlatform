using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthAuditActivities",
                columns: table => new
                {
                    AuthAuditActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformedByUserId = table.Column<int>(type: "int", nullable: true),
                    PerformedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthAuditActivities", x => x.AuthAuditActivityId);
                });

            migrationBuilder.CreateTable(
                name: "AuthLoginHistories",
                columns: table => new
                {
                    AuthLoginHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthUserId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    FailureReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthLoginHistories", x => x.AuthLoginHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "AuthPermissions",
                columns: table => new
                {
                    AuthPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PermissionCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthPermissions", x => x.AuthPermissionId);
                });

            migrationBuilder.CreateTable(
                name: "AuthGroups",
                columns: table => new
                {
                    AuthGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthGroups", x => x.AuthGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    AuthUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUsers", x => x.AuthUserId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenditureHeads",
                columns: table => new
                {
                    ExpenditureHeadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenditureHeadName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenditureHeads", x => x.ExpenditureHeadId);
                });

            migrationBuilder.CreateTable(
                name: "AuthGroupPermissions",
                columns: table => new
                {
                    AuthGroupPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthGroupId = table.Column<int>(type: "int", nullable: false),
                    AuthPermissionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthGroupPermissions", x => x.AuthGroupPermissionId);
                    table.ForeignKey(
                        name: "FK_AuthGroupPermissions_AuthPermissions_AuthPermissionId",
                        column: x => x.AuthPermissionId,
                        principalTable: "AuthPermissions",
                        principalColumn: "AuthPermissionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthGroupPermissions_AuthGroups_AuthGroupId",
                        column: x => x.AuthGroupId,
                        principalTable: "AuthGroups",
                        principalColumn: "AuthGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthRefreshTokens",
                columns: table => new
                {
                    AuthRefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthUserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRefreshTokens", x => x.AuthRefreshTokenId);
                    table.ForeignKey(
                        name: "FK_AuthRefreshTokens_AuthUsers_AuthUserId",
                        column: x => x.AuthUserId,
                        principalTable: "AuthUsers",
                        principalColumn: "AuthUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserPermissions",
                columns: table => new
                {
                    AuthUserPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthUserId = table.Column<int>(type: "int", nullable: false),
                    AuthPermissionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserPermissions", x => x.AuthUserPermissionId);
                    table.ForeignKey(
                        name: "FK_AuthUserPermissions_AuthPermissions_AuthPermissionId",
                        column: x => x.AuthPermissionId,
                        principalTable: "AuthPermissions",
                        principalColumn: "AuthPermissionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthUserPermissions_AuthUsers_AuthUserId",
                        column: x => x.AuthUserId,
                        principalTable: "AuthUsers",
                        principalColumn: "AuthUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserGroups",
                columns: table => new
                {
                    AuthUserGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthUserId = table.Column<int>(type: "int", nullable: false),
                    AuthGroupId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserGroups", x => x.AuthUserGroupId);
                    table.ForeignKey(
                        name: "FK_AuthUserGroups_AuthGroups_AuthGroupId",
                        column: x => x.AuthGroupId,
                        principalTable: "AuthGroups",
                        principalColumn: "AuthGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthUserGroups_AuthUsers_AuthUserId",
                        column: x => x.AuthUserId,
                        principalTable: "AuthUsers",
                        principalColumn: "AuthUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenditureInvoices",
                columns: table => new
                {
                    ExpenditureInvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenditureHeadId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenditureInvoices", x => x.ExpenditureInvoiceId);
                    table.ForeignKey(
                        name: "FK_ExpenditureInvoices_ExpenditureHeads_ExpenditureHeadId",
                        column: x => x.ExpenditureHeadId,
                        principalTable: "ExpenditureHeads",
                        principalColumn: "ExpenditureHeadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenditureInvoiceDetails",
                columns: table => new
                {
                    ExpenditureInvoiceDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenditureInvoiceId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedBy = table.Column<int>(type: "int", nullable: true),
                    RecordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenditureInvoiceDetails", x => x.ExpenditureInvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_ExpenditureInvoiceDetails_ExpenditureInvoices_ExpenditureInvoiceId",
                        column: x => x.ExpenditureInvoiceId,
                        principalTable: "ExpenditureInvoices",
                        principalColumn: "ExpenditureInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuthPermissions",
                columns: new[] { "AuthPermissionId", "Description", "IsActive", "PermissionCode", "PermissionName", "RecordCreatedBy", "RecordCreatedDate", "RecordUpdatedBy", "RecordUpdatedDate" },
                values: new object[,]
                {
                    { 1, "Allows user to view expenditure heads", true, "ExpenditureHead.View", "View Expenditure Heads", null, null, null, null },
                    { 2, "Allows user to create expenditure heads", true, "ExpenditureHead.Create", "Create Expenditure Heads", null, null, null, null },
                    { 3, "Allows user to edit expenditure heads", true, "ExpenditureHead.Edit", "Edit Expenditure Heads", null, null, null, null },
                    { 4, "Allows user to delete expenditure heads", true, "ExpenditureHead.Delete", "Delete Expenditure Heads", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "AuthGroups",
                columns: new[] { "AuthGroupId", "Description", "IsActive", "RecordCreatedBy", "RecordCreatedDate", "RecordUpdatedBy", "RecordUpdatedDate", "GroupName" },
                values: new object[,]
                {
                    { 1, "System administrator", true, null, null, null, null, "Admin" },
                    { 2, "Read-only user", true, null, null, null, null, "Viewer" }
                });

            migrationBuilder.InsertData(
                table: "AuthUsers",
                columns: new[] { "AuthUserId", "Email", "FailedLoginAttempts", "FirstName", "IsActive", "IsLocked", "LastName", "PasswordHash", "RecordCreatedBy", "RecordCreatedDate", "RecordUpdatedBy", "RecordUpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@authplatform.com", 0, "System", true, false, "Admin", "$2a$11$PSakMju1u1ZzRcUAMf5N/eFytXLA8thb3FrKWDIw3SRnbIM.xeepa", null, null, null, null, "admin" },
                    { 2, "viewer@authplatform.com", 0, "Normal", true, false, "Viewer", "$2a$11$PSakMju1u1ZzRcUAMf5N/eFytXLA8thb3FrKWDIw3SRnbIM.xeepa", null, null, null, null, "viewer" }
                });

            migrationBuilder.InsertData(
                table: "AuthGroupPermissions",
                columns: new[] { "AuthGroupPermissionId", "AuthPermissionId", "AuthGroupId", "IsActive", "RecordCreatedBy", "RecordCreatedDate", "RecordUpdatedBy", "RecordUpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, 1, true, null, null, null, null },
                    { 2, 2, 1, true, null, null, null, null },
                    { 3, 3, 1, true, null, null, null, null },
                    { 4, 4, 1, true, null, null, null, null },
                    { 5, 1, 2, true, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "AuthUserPermissions",
                columns: new[] { "AuthUserPermissionId", "AuthPermissionId", "AuthUserId", "IsActive", "RecordCreatedBy", "RecordCreatedDate", "RecordUpdatedBy", "RecordUpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, 1, true, null, null, null, null },
                    { 2, 2, 1, true, null, null, null, null },
                    { 3, 3, 1, true, null, null, null, null },
                    { 4, 4, 1, true, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "AuthUserGroups",
                columns: new[] { "AuthUserGroupId", "AuthGroupId", "AuthUserId", "IsActive", "RecordCreatedBy", "RecordCreatedDate", "RecordUpdatedBy", "RecordUpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, 1, true, null, null, null, null },
                    { 2, 2, 2, true, null, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthPermissions_PermissionCode",
                table: "AuthPermissions",
                column: "PermissionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthRefreshTokens_AuthUserId",
                table: "AuthRefreshTokens",
                column: "AuthUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthGroupPermissions_AuthPermissionId",
                table: "AuthGroupPermissions",
                column: "AuthPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthGroupPermissions_AuthGroupId",
                table: "AuthGroupPermissions",
                column: "AuthGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserPermissions_AuthPermissionId",
                table: "AuthUserPermissions",
                column: "AuthPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserPermissions_AuthUserId",
                table: "AuthUserPermissions",
                column: "AuthUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserGroups_AuthGroupId",
                table: "AuthUserGroups",
                column: "AuthGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserGroups_AuthUserId",
                table: "AuthUserGroups",
                column: "AuthUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUsers_Email",
                table: "AuthUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthUsers_UserName",
                table: "AuthUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenditureInvoiceDetails_ExpenditureInvoiceId",
                table: "ExpenditureInvoiceDetails",
                column: "ExpenditureInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenditureInvoices_ExpenditureHeadId",
                table: "ExpenditureInvoices",
                column: "ExpenditureHeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthAuditActivities");

            migrationBuilder.DropTable(
                name: "AuthLoginHistories");

            migrationBuilder.DropTable(
                name: "AuthRefreshTokens");

            migrationBuilder.DropTable(
                name: "AuthGroupPermissions");

            migrationBuilder.DropTable(
                name: "AuthUserPermissions");

            migrationBuilder.DropTable(
                name: "AuthUserGroups");

            migrationBuilder.DropTable(
                name: "ExpenditureInvoiceDetails");

            migrationBuilder.DropTable(
                name: "AuthPermissions");

            migrationBuilder.DropTable(
                name: "AuthGroups");

            migrationBuilder.DropTable(
                name: "AuthUsers");

            migrationBuilder.DropTable(
                name: "ExpenditureInvoices");

            migrationBuilder.DropTable(
                name: "ExpenditureHeads");
        }
    }
}
