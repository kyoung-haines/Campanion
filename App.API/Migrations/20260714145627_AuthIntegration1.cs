using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.API.Migrations
{
    /// <inheritdoc />
    public partial class AuthIntegration1 : Migration
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

            migrationBuilder.RenameColumn(
                name: "AppUserProfileId",
                table: "AspNetUsers",
                newName: "TripId");

            migrationBuilder.AddColumn<string>(
                name: "TripName",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ProfileUsername",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AppUserId",
                table: "Profiles",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TripId",
                table: "AspNetUsers",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trips_TripId",
                table: "AspNetUsers",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_AppUserId",
                table: "Profiles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Trips_TripId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_AppUserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_AppUserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TripId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TripName",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ProfileUsername",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "AspNetUsers",
                newName: "AppUserProfileId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Profiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
