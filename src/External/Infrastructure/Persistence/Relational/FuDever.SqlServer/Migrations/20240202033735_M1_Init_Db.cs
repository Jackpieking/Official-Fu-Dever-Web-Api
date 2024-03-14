using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FuDever.SqlServer.V1.Migrations;

/// <inheritdoc />
public partial class M1_Init_Db : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Cvs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StudentFullName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                StudentEmail = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                StudentId = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                StudentCvFileId = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cvs", x => x.Id);
            },
            comment: "Contain cv record.");

        migrationBuilder.CreateTable(
            name: "Departments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Departments", x => x.Id);
            },
            comment: "Contain department record.");

        migrationBuilder.CreateTable(
            name: "Hobbies",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Hobbies", x => x.Id);
            },
            comment: "Contain hobby record.");

        migrationBuilder.CreateTable(
            name: "Majors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Majors", x => x.Id);
            },
            comment: "Contain major record.");

        migrationBuilder.CreateTable(
            name: "Platforms",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Platforms", x => x.Id);
            },
            comment: "Contain platform record.");

        migrationBuilder.CreateTable(
            name: "Positions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Positions", x => x.Id);
            },
            comment: "Contain position record.");

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            },
            comment: "Contain role record.");

        migrationBuilder.CreateTable(
            name: "Skills",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Skills", x => x.Id);
            },
            comment: "Contain skill record.");

        migrationBuilder.CreateTable(
            name: "userJoiningStatuses",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_userJoiningStatuses", x => x.Id);
            },
            comment: "Contain user joining status record.");

        migrationBuilder.CreateTable(
            name: "RoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_RoleClaims_Roles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Contain role claim record.");

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserJoiningStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MajorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                LastName = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                Career = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                Workplaces = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                EducationPlaces = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                BirthDay = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                HomeAddress = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                SelfDescription = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                JoinDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                ActivityPoints = table.Column<short>(type: "smallint", nullable: false),
                AvatarUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Departments_DepartmentId",
                    column: x => x.DepartmentId,
                    principalTable: "Departments",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Users_Majors_MajorId",
                    column: x => x.MajorId,
                    principalTable: "Majors",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Users_Positions_PositionId",
                    column: x => x.PositionId,
                    principalTable: "Positions",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Users_userJoiningStatuses_UserJoiningStatusId",
                    column: x => x.UserJoiningStatusId,
                    principalTable: "userJoiningStatuses",
                    principalColumn: "Id");
            },
            comment: "Contain user record.");

        migrationBuilder.CreateTable(
            name: "Blogs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                Thumbnail = table.Column<string>(type: "NVARCHAR(200)", nullable: false),
                UploadDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RemovedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Blogs", x => x.Id);
                table.ForeignKey(
                    name: "FK_Blogs_Users_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain blog record.");

        migrationBuilder.CreateTable(
            name: "Projects",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                SourceCodeUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                DemoUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                ThumbnailUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Projects", x => x.Id);
                table.ForeignKey(
                    name: "FK_Projects_Users_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain project record.");

        migrationBuilder.CreateTable(
            name: "RefreshTokens",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Value = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                AccessTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ExpiredDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                table.ForeignKey(
                    name: "FK_RefreshTokens_Users_CreatedBy",
                    column: x => x.CreatedBy,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain refresh token record.");

        migrationBuilder.CreateTable(
            name: "UserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserClaims_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Contain user claim record.");

        migrationBuilder.CreateTable(
            name: "UserHobbies",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                HobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserHobbies", x => new { x.UserId, x.HobbyId });
                table.ForeignKey(
                    name: "FK_UserHobbies_Hobbies_HobbyId",
                    column: x => x.HobbyId,
                    principalTable: "Hobbies",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserHobbies_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain user hobby record.");

        migrationBuilder.CreateTable(
            name: "UserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_UserLogins_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Contain user login record.");

        migrationBuilder.CreateTable(
            name: "UserPlatforms",
            columns: table => new
            {
                PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PlatformUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserPlatforms", x => new { x.UserId, x.PlatformId });
                table.ForeignKey(
                    name: "FK_UserPlatforms_Platforms_PlatformId",
                    column: x => x.PlatformId,
                    principalTable: "Platforms",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserPlatforms_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain user platform record.");

        migrationBuilder.CreateTable(
            name: "UserRoles",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_UserRoles_Roles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserRoles_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Contain user role record.");

        migrationBuilder.CreateTable(
            name: "UserSkills",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserSkills", x => new { x.UserId, x.SkillId });
                table.ForeignKey(
                    name: "FK_UserSkills_Skills_SkillId",
                    column: x => x.SkillId,
                    principalTable: "Skills",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserSkills_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain user skill record.");

        migrationBuilder.CreateTable(
            name: "UserTokens",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_UserTokens_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Contain user token record.");

        migrationBuilder.CreateTable(
            name: "BlogComments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UploadDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogComments", x => x.Id);
                table.ForeignKey(
                    name: "FK_BlogComments_Blogs_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blogs",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogComments_Users_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "Users",
                    principalColumn: "Id");
            },
            comment: "Contain blog comment record.");

        migrationBuilder.CreateTable(
            name: "BlogTags",
            columns: table => new
            {
                BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogTags", x => new { x.BlogId, x.SkillId });
                table.ForeignKey(
                    name: "FK_BlogTags_Blogs_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blogs",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BlogTags_Skills_SkillId",
                    column: x => x.SkillId,
                    principalTable: "Skills",
                    principalColumn: "Id");
            },
            comment: "Contain blog tag record.");

        migrationBuilder.CreateIndex(
            name: "IX_BlogComments_AuthorId",
            table: "BlogComments",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogComments_BlogId",
            table: "BlogComments",
            column: "BlogId");

        migrationBuilder.CreateIndex(
            name: "IX_Blogs_AuthorId",
            table: "Blogs",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_BlogTags_SkillId",
            table: "BlogTags",
            column: "SkillId");

        migrationBuilder.CreateIndex(
            name: "IX_Projects_AuthorId",
            table: "Projects",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_RefreshTokens_CreatedBy",
            table: "RefreshTokens",
            column: "CreatedBy");

        migrationBuilder.CreateIndex(
            name: "IX_RoleClaims_RoleId",
            table: "RoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "Roles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_UserClaims_UserId",
            table: "UserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserHobbies_HobbyId",
            table: "UserHobbies",
            column: "HobbyId");

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_UserId",
            table: "UserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserPlatforms_PlatformId",
            table: "UserPlatforms",
            column: "PlatformId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRoles_RoleId",
            table: "UserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "Users",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "IX_Users_DepartmentId",
            table: "Users",
            column: "DepartmentId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_MajorId",
            table: "Users",
            column: "MajorId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_PositionId",
            table: "Users",
            column: "PositionId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_UserJoiningStatusId",
            table: "Users",
            column: "UserJoiningStatusId");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "Users",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_UserSkills_SkillId",
            table: "UserSkills",
            column: "SkillId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BlogComments");

        migrationBuilder.DropTable(
            name: "BlogTags");

        migrationBuilder.DropTable(
            name: "Cvs");

        migrationBuilder.DropTable(
            name: "Projects");

        migrationBuilder.DropTable(
            name: "RefreshTokens");

        migrationBuilder.DropTable(
            name: "RoleClaims");

        migrationBuilder.DropTable(
            name: "UserClaims");

        migrationBuilder.DropTable(
            name: "UserHobbies");

        migrationBuilder.DropTable(
            name: "UserLogins");

        migrationBuilder.DropTable(
            name: "UserPlatforms");

        migrationBuilder.DropTable(
            name: "UserRoles");

        migrationBuilder.DropTable(
            name: "UserSkills");

        migrationBuilder.DropTable(
            name: "UserTokens");

        migrationBuilder.DropTable(
            name: "Blogs");

        migrationBuilder.DropTable(
            name: "Hobbies");

        migrationBuilder.DropTable(
            name: "Platforms");

        migrationBuilder.DropTable(
            name: "Roles");

        migrationBuilder.DropTable(
            name: "Skills");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Departments");

        migrationBuilder.DropTable(
            name: "Majors");

        migrationBuilder.DropTable(
            name: "Positions");

        migrationBuilder.DropTable(
            name: "userJoiningStatuses");
    }
}
