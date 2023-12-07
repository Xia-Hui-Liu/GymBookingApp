using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBookingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClasses_GymClasses_GymClassId",
                table: "ApplicationUserGymClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGymClasses",
                table: "ApplicationUserGymClasses");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserGymClasses_ApplicationUserId",
                table: "ApplicationUserGymClasses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUserGymClasses");

            migrationBuilder.AlterColumn<int>(
                name: "GymClassId",
                table: "ApplicationUserGymClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserGymClasses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGymClasses",
                table: "ApplicationUserGymClasses",
                columns: new[] { "ApplicationUserId", "GymClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymClasses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClasses_GymClasses_GymClassId",
                table: "ApplicationUserGymClasses",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClasses_GymClasses_GymClassId",
                table: "ApplicationUserGymClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGymClasses",
                table: "ApplicationUserGymClasses");

            migrationBuilder.AlterColumn<int>(
                name: "GymClassId",
                table: "ApplicationUserGymClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserGymClasses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApplicationUserGymClasses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGymClasses",
                table: "ApplicationUserGymClasses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGymClasses_ApplicationUserId",
                table: "ApplicationUserGymClasses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymClasses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClasses_GymClasses_GymClassId",
                table: "ApplicationUserGymClasses",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id");
        }
    }
}
