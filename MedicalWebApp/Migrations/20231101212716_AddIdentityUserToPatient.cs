using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalWebApp.Migrations
{
    public partial class AddIdentityUserToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Nurses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "92a77ffd-e1b6-4d7e-ac7d-c4d7ca835053", "Doctor", "DOCTOR" },
                    { "2", "c692d6cd-94c6-41bc-8e42-0eb47ba23293", "Patient", "PATIENT" },
                    { "3", "de47883d-197d-404f-99d5-93788fc2fbca", "Admin", "ADMIN" },
                    { "4", "37265d35-63a5-4c50-8636-14e13e425ae1", "Nurse", "NURSE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdentityUserId",
                table: "Patients",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_IdentityUserId",
                table: "Nurses",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_IdentityUserId",
                table: "Doctors",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_IdentityUserId",
                table: "Admins",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_AspNetUsers_IdentityUserId",
                table: "Admins",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_IdentityUserId",
                table: "Doctors",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_AspNetUsers_IdentityUserId",
                table: "Nurses",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_IdentityUserId",
                table: "Patients",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_AspNetUsers_IdentityUserId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_IdentityUserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_AspNetUsers_IdentityUserId",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_IdentityUserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_IdentityUserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_IdentityUserId",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_IdentityUserId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Admins_IdentityUserId",
                table: "Admins");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Admins");
        }
    }
}
