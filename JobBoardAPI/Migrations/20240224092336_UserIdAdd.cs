using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserIdAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobAdvertisements_JobAdvertisementId",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "JobAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertisements_UserId",
                table: "JobAdvertisements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdvertisements_Users_UserId",
                table: "JobAdvertisements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobAdvertisements_JobAdvertisementId",
                table: "JobApplications",
                column: "JobAdvertisementId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Users_UserId",
                table: "JobApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdvertisements_Users_UserId",
                table: "JobAdvertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobAdvertisements_JobAdvertisementId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Users_UserId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobAdvertisements_UserId",
                table: "JobAdvertisements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobAdvertisements");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobAdvertisements_JobAdvertisementId",
                table: "JobApplications",
                column: "JobAdvertisementId",
                principalTable: "JobAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
