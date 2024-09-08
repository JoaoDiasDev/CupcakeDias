using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupcakeDias.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges_Entities_JsonIgnore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cupcakes_CupcakeId",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Cupcakes",
                type: "varchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cupcakes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(155)",
                oldMaxLength: 155)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "CupcakeId",
                table: "CartItems",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("bcaa2fbe-9209-46a2-b41c-176815419ab5"),
                column: "RoleName",
                value: "Manager");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("cb4e3c76-d337-4f4b-82df-04e9acc4fb74"),
                column: "RoleName",
                value: "Admin");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cupcakes_CupcakeId",
                table: "CartItems",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "CupcakeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cupcakes_CupcakeId",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Cupcakes",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(400)",
                oldMaxLength: 400)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cupcakes",
                type: "varchar(155)",
                maxLength: 155,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "CupcakeId",
                table: "CartItems",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("bcaa2fbe-9209-46a2-b41c-176815419ab5"),
                column: "RoleName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("cb4e3c76-d337-4f4b-82df-04e9acc4fb74"),
                column: "RoleName",
                value: "Manager");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cupcakes_CupcakeId",
                table: "CartItems",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "CupcakeId");
        }
    }
}
