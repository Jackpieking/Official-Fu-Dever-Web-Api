using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.SqlServer2016.Migrations
{
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
                    FullName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CvFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Type = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
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
                    Thumbnail = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    AccessTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
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

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("0f74178a-ce63-40fd-a4b7-e610a890772a"), "Board of Directors", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a4d8e1e0-2143-4814-932b-2b489ee13e6a"), "Events Board", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a8168a8a-65e6-4c39-98e2-d235e49b4f56"), "Academic Board", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("eb0d949f-01e7-4b93-b7fe-fadfe4257419"), "Administrative Board", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("fca05289-0706-4574-a808-eaff6ca384a5"), "Media Board", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "Id", "Name", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("03bb273f-b40b-4de6-baa9-e1c4afe9d910"), "Traveling", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("07b5d72c-7f0e-45d4-97e9-23a1a14d2c6e"), "Volunteering", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("10216efe-e74a-4f63-840e-40c53da22c93"), "Home Improvement", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11b17fd8-1649-4095-bfd7-e0a13185cc46"), "Reading", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("14a1a41e-487c-4385-ae39-b76f7721cb76"), "Dancing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("238a4219-cbb5-4742-b0e4-cdb88d3c62a9"), "Chess", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5b77e7d5-35bd-45bb-87ff-30c890a96340"), "Drawing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5bdb3fff-5543-4f39-a1f9-2de18660d5f6"), "Collecting", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5fe08a51-c6c5-449f-974e-fa66fdce66c4"), "DIY Crafts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("66a2c19c-ce19-489c-9a7f-476312065ed1"), "Coding", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6a1824e0-d1e4-4f6c-a75e-bccc92f8b7b9"), "Games", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7a4a2fc7-9c50-4338-8821-8f1df687a5a8"), "Taekwondo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8436a55f-491e-43d9-b3ab-8d811b9b446b"), "Playing an Instrument", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9308b778-a6c7-4716-9001-8b28e75e1686"), "Photography", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9ac80e0a-b4b3-4ba5-b338-8b4fab4cd11f"), "Gardening", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a95a6144-78f8-4850-a700-7a1fa7dc7874"), "Sports", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("aa3b9cc4-85b0-4558-8985-2e0840dd5d84"), "Writing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("afeaa28e-50d9-4633-936c-8ca1ac04f064"), "Hiking", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b332c9ed-e9a8-44cf-8aee-7f0bc87680b7"), "Camping", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c030c512-38de-4db1-b6fa-9b8371993cfd"), "Birdwatching", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d0bd66b0-200b-4680-a60b-2a285e848c29"), "Painting", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("dfbc0ba0-fdcf-4e9e-a59e-fecd0cebcbb6"), "Yoga", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ef003d04-2145-45ef-91b1-e31c96683a58"), "Fishing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f3725983-20a6-4c70-bd07-32c81a4a7acd"), "Cooking", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f39aa8ce-ab90-4973-919c-c61016f140f3"), "Surfing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f52fc708-09a0-4091-8135-564fba86ea46"), "Baking", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "Id", "Name", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("589b9229-d544-49b5-923e-8085dc9c0ed0"), "Artificial Intelligence", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("66a742b0-fe6c-43c6-b8c5-5e33af9804a4"), "Digital Art Design", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("77eb22b8-2ecc-458c-87e0-107ba2c12843"), "Information Security", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9a5dda0a-8bb9-46b2-a33b-50ee7a24d4f9"), "Information System", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("fc29c55b-0e13-4cf2-83bb-36b87f800aa2"), "Software Engineering", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("2fed0532-ac5d-4711-9838-786f14f0994e"), "Instagram", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("390989b1-630d-4aa2-8932-887e5b3109ba"), "Facebook", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("68521e26-399c-4f17-8609-3e794861a1eb"), "Youtube", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("95226acf-53f7-47ed-96ab-4cfbfa7995a8"), "LinkedIn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c6ff82a8-249e-41d7-a352-940b49e3a886"), "Twitter", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f861a890-4c53-4705-9a24-45a3856b72ff"), "GitHub", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("02f79406-56cd-4919-9e8a-887f545f9bc0"), "Member", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("14c75af2-39d8-4210-8240-82b6a154139c"), "Secretary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("1af8680d-5c44-4cd2-99cf-99160f2db2ca"), "Events Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("39ac4b14-42fc-40d7-89b9-4b5b279448c8"), "Club President", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3e23f191-d4ed-45c4-b012-04f649c107d2"), "Vice Administrative Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("459e268d-2e5c-4514-8da1-8a0afa345c27"), "Media Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("501d886f-5e10-4577-87ee-3c85293886da"), "Administrative Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("52c67c43-c548-4a18-bb2a-0eaa9ac37cba"), "Vice Club President", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8acf5da9-95dd-44ec-a798-41c4c46fb205"), "Vice Academic Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cab3820c-b1f8-47d1-ba50-79ae813a5101"), "Vice Events Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d73b17e1-df63-4c7b-b92d-0e51e5367e26"), "Academic Department Head", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("2d30e5ff-214e-4a29-b396-c25fec7d6a18"), null, "admin", "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("80ca7b9b-0e7b-4a0a-bbba-7911b5ec34c1"), null, "user", "USER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name", "RemovedAt", "RemovedBy" },
                values: new object[,]
                {
                    { new Guid("0390d6a7-4df3-443e-abbe-e1b5eb5fe06e"), "C#", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("04a4c22d-1c76-4514-a0e7-a63aec9fff87"), "Swift", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("05ad780c-63c9-4ab0-a206-92ff9eb44237"), "Typescript", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("05d6e638-6811-4353-9c88-b68fd582efc2"), "Flutter", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("08f873c2-9e5a-4997-a707-662347d28355"), "Django", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0c536cca-9889-4e89-a11d-8cf0ac9c5b7f"), "Angular.js", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0ea38c1b-2a03-4224-b7f8-594f20d48e00"), "SQL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("105a0e1a-9f78-4da8-93dd-f5788f6a28a6"), "Docker", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("12dbe1f8-b2e0-43eb-a938-ae029c203bf2"), "PostgreSQL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("17588797-40d9-42f3-9a40-dcfdd28296fb"), "Spring Boot", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("187742d7-e49c-4a30-9771-097404d9aba6"), "MongoDB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("1c6d3cae-af5e-4cf5-bbd4-1e83302c26df"), "Apache", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("2741e17f-f3e6-4bbd-a7a8-2acbcc7ba047"), "Vue.js", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3475009c-0799-4b6a-9629-9a78cb280b68"), "Next.js", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("371fb909-9f4b-40c4-ba76-d076d789e30d"), "Authentication", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3a9ef04c-e647-4381-a360-cf3b68d9d010"), "RESTful API", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("48418756-cca1-4238-b4c4-8d2776beb60f"), "React Native", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4882a134-dcbd-40a2-9075-18a01aca1059"), "Dart", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("57375c24-5316-452a-9e84-9cbf0f98c6f8"), "Express.js", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5d5bac2d-4a27-4308-bc31-4bb108a80656"), "JavaScript", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("64698363-90dc-4515-9e73-7bf44615944c"), "Ruby", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6755dcda-b053-41c0-a62c-1abd7b26b72c"), "Authorization", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("68c626e8-4f79-4a5f-a9c1-4d500efee42e"), "Flask", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6da0a77f-7019-4194-8fb3-303570d833ba"), "Bootstrap", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("734ac6f5-14b5-4124-9e45-ab303acfdd14"), "Devops", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8360d12c-0a66-4ee1-b9bb-07ad7125f8c8"), "SQL Server", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("927c8d5c-3415-4978-bc57-04b359264186"), "HTML/CSS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9cb59ab8-3892-40e6-9a3e-8bd38bc6796f"), "Flexbox", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a63b19d5-3a19-44f6-9e19-738b6705b672"), "Python", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b4d26272-752e-4fe7-9693-fe1c7c041499"), "Web Security", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("bc1ea72b-2b34-42e1-8dfc-4f60bb3a13af"), "MySQL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("bd906ff9-730e-4822-9b59-7f8749da879c"), "C++", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c210f92c-e6c1-4320-aff1-01b086f120f5"), "Git", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c35ba97e-0de1-4865-8564-f9efbcb99d99"), "Java", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c5b53581-648c-4cf8-98a9-6d5bd1de4543"), "GraphQL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c9ebe8a8-2bbd-4669-bf02-a34b4264c326"), "Agile", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cc515467-70f0-468f-a880-7370f63028bc"), "Github", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cff3ba40-e78d-4f3b-9f20-4a01bd6d5eb3"), "React.js", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e7f4e74b-f7ee-4fb4-86d4-c3df1aa01042"), "C", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ec930d0b-e7d0-4578-8b4f-a9b03a111ad0"), "ASP.NET Core", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ef2f9f8b-ff5a-492f-b2af-412f950fd5e3"), "Node.js", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f9bb8c92-0bca-409f-bfe7-bbcd4f661de1"), "Caching", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("fc39079c-3512-4967-9794-615c1a7ce50a"), "PHP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "userJoiningStatuses",
                columns: new[] { "Id", "RemovedAt", "RemovedBy", "Type" },
                values: new object[,]
                {
                    { new Guid("1d918610-7094-4f8c-838e-94e9adec6c33"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "Approved" },
                    { new Guid("52416e2b-47e2-4962-8274-6f411168df2e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "Expired" },
                    { new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "" },
                    { new Guid("6f60c051-8648-4a0d-8cfb-579966571d70"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "Pending" },
                    { new Guid("a79e7c37-cd5d-4845-b8be-d22768111e1f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ActivityPoints", "AvatarUrl", "BirthDay", "Career", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DepartmentId", "EducationPlaces", "Email", "EmailConfirmed", "FirstName", "HomeAddress", "JoinDate", "LastName", "LockoutEnabled", "LockoutEnd", "MajorId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "RemovedAt", "RemovedBy", "SecurityStamp", "SelfDescription", "TwoFactorEnabled", "UpdatedAt", "UpdatedBy", "UserJoiningStatusId", "UserName", "Workplaces" },
                values: new object[] { new Guid("495decca-121a-41c9-a067-622d227d5017"), 0, (short)0, "https://firebasestorage.googleapis.com/v0/b/comic-image-storage.appspot.com/o/blank-profile-picture-973460_1280.png?alt=media&token=2309abba-282c-4f81-846e-6336235103dc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "dea1fb0b-6f33-48f6-a73f-2c1973476593", new DateTime(2024, 1, 16, 8, 5, 4, 152, DateTimeKind.Utc).AddTicks(9470), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "", "ledinhdangkhoa10a9@gmail.com", true, "", "", new DateTime(2024, 1, 16, 8, 5, 4, 152, DateTimeKind.Utc).AddTicks(9467), "", false, null, new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), "LEDINHDANGKHOA10A9@GMAIL.COM", "LEDINHDANGKHOA10A9@GMAIL.COM", "AQAAAAIAAYagAAAAEGD9QHVXd3PD1ei6rn9eZ7GAcKhCItbHzShc+K3bZnL4Se1bpolsfxTjkJ6PExNTlA==", null, false, new Guid("590e57ba-235f-4c24-be80-4720ee1771b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), null, "", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("52416e2b-47e2-4962-8274-6f411168df2e"), "ledinhdangkhoa10a9@gmail.com", "" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { new Guid("2d30e5ff-214e-4a29-b396-c25fec7d6a18"), new Guid("495decca-121a-41c9-a067-622d227d5017"), "UserRole" });

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
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

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
}
