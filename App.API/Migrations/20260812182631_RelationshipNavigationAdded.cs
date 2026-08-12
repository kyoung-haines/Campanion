using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.API.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipNavigationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCampground");

            migrationBuilder.DropColumn(
                name: "CampgroundActivities",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundCity",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundCloseDate",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundCountry",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundEmail",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundFacilities",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundHasActivities",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundIsOpenYearRound",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundName",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundOpenDate",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundPhone",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundPostalCode",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundProvince",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundStreetName",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundType",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CampgroundUrl",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "CamproundImagePath",
                table: "Campgrounds");

            migrationBuilder.AddColumn<DateTime>(
                name: "TripCampgroundAddedAt",
                table: "TripCampgrounds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppUserTripeAddedAt",
                table: "AppUserTrips",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AppUserTrips",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AppUserFavouriteCampgrounds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTrips_AppUserId",
                table: "AppUserTrips",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFavouriteCampgrounds_CampgroundId",
                table: "AppUserFavouriteCampgrounds",
                column: "CampgroundId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFavouriteCampgrounds_AspNetUsers_AppUserId",
                table: "AppUserFavouriteCampgrounds",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFavouriteCampgrounds_Campgrounds_CampgroundId",
                table: "AppUserFavouriteCampgrounds",
                column: "CampgroundId",
                principalTable: "Campgrounds",
                principalColumn: "CampgroundId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserTrips_AspNetUsers_AppUserId",
                table: "AppUserTrips",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserTrips_Trips_TripId",
                table: "AppUserTrips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFavouriteCampgrounds_AspNetUsers_AppUserId",
                table: "AppUserFavouriteCampgrounds");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFavouriteCampgrounds_Campgrounds_CampgroundId",
                table: "AppUserFavouriteCampgrounds");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserTrips_AspNetUsers_AppUserId",
                table: "AppUserTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserTrips_Trips_TripId",
                table: "AppUserTrips");

            migrationBuilder.DropIndex(
                name: "IX_AppUserTrips_AppUserId",
                table: "AppUserTrips");

            migrationBuilder.DropIndex(
                name: "IX_AppUserFavouriteCampgrounds_CampgroundId",
                table: "AppUserFavouriteCampgrounds");

            migrationBuilder.DropColumn(
                name: "TripCampgroundAddedAt",
                table: "TripCampgrounds");

            migrationBuilder.AddColumn<string>(
                name: "CampgroundActivities",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CampgroundCity",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CampgroundCloseDate",
                table: "Campgrounds",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CampgroundCountry",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CampgroundEmail",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CampgroundFacilities",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CampgroundHasActivities",
                table: "Campgrounds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CampgroundIsOpenYearRound",
                table: "Campgrounds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CampgroundName",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CampgroundOpenDate",
                table: "Campgrounds",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CampgroundPhone",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CampgroundPostalCode",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CampgroundProvince",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CampgroundStreetName",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CampgroundType",
                table: "Campgrounds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CampgroundUrl",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CamproundImagePath",
                table: "Campgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AppUserTripeAddedAt",
                table: "AppUserTrips",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "AppUserTrips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "AppUserFavouriteCampgrounds",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "AppUserCampground",
                columns: table => new
                {
                    AppUserFavouriteCampgroundsCampgroundId = table.Column<int>(type: "int", nullable: false),
                    FavouritedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCampground", x => new { x.AppUserFavouriteCampgroundsCampgroundId, x.FavouritedById });
                    table.ForeignKey(
                        name: "FK_AppUserCampground_AspNetUsers_FavouritedById",
                        column: x => x.FavouritedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserCampground_Campgrounds_AppUserFavouriteCampgroundsCampgroundId",
                        column: x => x.AppUserFavouriteCampgroundsCampgroundId,
                        principalTable: "Campgrounds",
                        principalColumn: "CampgroundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCampground_FavouritedById",
                table: "AppUserCampground",
                column: "FavouritedById");
        }
    }
}
