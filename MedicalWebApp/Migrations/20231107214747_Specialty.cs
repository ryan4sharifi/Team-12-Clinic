using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalWebApp.Migrations
{
    public partial class Specialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "specialty_id",
                table: "Referrals",
                newName: "speciality_id");

            migrationBuilder.RenameIndex(
                name: "IX_Referrals_specialty_id",
                table: "Referrals",
                newName: "IX_Referrals_speciality_id");

            migrationBuilder.RenameColumn(
                name: "specialty_id",
                table: "Doctors",
                newName: "speciality_id");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_specialty_id",
                table: "Doctors",
                newName: "IX_Doctors_speciality_id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "300613e3-2329-48ed-9fa7-727ff2c96499");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4a4b9c35-03ef-42e0-8c0a-9eae9dc0c109");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c4dec35a-8f59-4d6b-bf9f-b29a91f6f3ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "ba0c1598-9a77-4877-8de1-434178eab851");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "speciality_id",
                table: "Referrals",
                newName: "specialty_id");

            migrationBuilder.RenameIndex(
                name: "IX_Referrals_speciality_id",
                table: "Referrals",
                newName: "IX_Referrals_specialty_id");

            migrationBuilder.RenameColumn(
                name: "speciality_id",
                table: "Doctors",
                newName: "specialty_id");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_speciality_id",
                table: "Doctors",
                newName: "IX_Doctors_specialty_id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "cbb13889-11e2-4a50-b6f8-6d03f4f5ca4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "91375cec-9928-4bb2-8f3d-e81238393b8f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "d76e06e3-ac74-4dc5-bad2-6b3b22d60274");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "dd140a1c-f448-4235-a1c2-2d24b4014d4d");
        }
    }
}
