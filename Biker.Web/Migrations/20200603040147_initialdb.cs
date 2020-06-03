using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biker.Web.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikeMakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeMakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BikeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorbikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    FrontTire = table.Column<int>(maxLength: 5, nullable: false),
                    RearTire = table.Column<int>(maxLength: 5, nullable: false),
                    MaxWidthTireF = table.Column<int>(maxLength: 5, nullable: false),
                    MinWidthTireF = table.Column<int>(maxLength: 50, nullable: false),
                    MaxWidthTireR = table.Column<int>(maxLength: 50, nullable: false),
                    MinWidthTireR = table.Column<int>(maxLength: 50, nullable: false),
                    YearSince = table.Column<DateTime>(nullable: false),
                    YearUntil = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    MakerId = table.Column<int>(nullable: true),
                    MotorbikeTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorbikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorbikes_BikeMakers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "BikeMakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motorbikes_BikeTypes_MotorbikeTypeId",
                        column: x => x.MotorbikeTypeId,
                        principalTable: "BikeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeMakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(nullable: true),
                    MakerId = table.Column<int>(nullable: true),
                    MotorbikeTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeMakers_BikeMakers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "BikeMakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeMakers_BikeTypes_MotorbikeTypeId",
                        column: x => x.MotorbikeTypeId,
                        principalTable: "BikeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorbikes_MakerId",
                table: "Motorbikes",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorbikes_MotorbikeTypeId",
                table: "Motorbikes",
                column: "MotorbikeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMakers_MakerId",
                table: "TypeMakers",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeMakers_MotorbikeTypeId",
                table: "TypeMakers",
                column: "MotorbikeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motorbikes");

            migrationBuilder.DropTable(
                name: "TypeMakers");

            migrationBuilder.DropTable(
                name: "BikeMakers");

            migrationBuilder.DropTable(
                name: "BikeTypes");
        }
    }
}
