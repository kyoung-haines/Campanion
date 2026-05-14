using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.API.Migrations
{
    /// <inheritdoc />
    public partial class NullableProfileTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profiles_AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserProfileId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserProfileId",
                table: "AspNetUsers",
                column: "AppUserProfileId",
                unique: true,
                filter: "[AppUserProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profiles_AppUserProfileId",
                table: "AspNetUsers",
                column: "AppUserProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profiles_AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserProfileId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserProfileId",
                table: "AspNetUsers",
                column: "AppUserProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profiles_AppUserProfileId",
                table: "AspNetUsers",
                column: "AppUserProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
