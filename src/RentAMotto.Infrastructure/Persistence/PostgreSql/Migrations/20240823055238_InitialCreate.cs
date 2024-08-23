using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RentAMotto.Domain.DomainObjects.Enums;

#nullable disable

namespace RentAMotto.Infrastructure.Persistence.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DrivingLicenceNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DrivingLicenceType = table.Column<int>(type: "integer", nullable: false),
                    DriverLicensePicture = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CostPerDay = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    PercentageOfFineForReturnBeforeExpectedEndDatePerDay = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    AmountOfFineForReturnAfterExpectedEndDatePerDay = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Make = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Model = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    YearOfManufacture = table.Column<int>(type: "integer", nullable: false),
                    NumberPlate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    DeliveryDriverId = table.Column<int>(type: "integer", nullable: false),
                    RentalPlanId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RentalAmount = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    FineAmount = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalContracts_DeliveryDrivers_DeliveryDriverId",
                        column: x => x.DeliveryDriverId,
                        principalTable: "DeliveryDrivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalContracts_RentalPlans_RentalPlanId",
                        column: x => x.RentalPlanId,
                        principalTable: "RentalPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_Cnpj",
                table: "DeliveryDrivers",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_DrivingLicenceNumber",
                table: "DeliveryDrivers",
                column: "DrivingLicenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_DeliveryDriverId",
                table: "RentalContracts",
                column: "DeliveryDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_RentalPlanId",
                table: "RentalContracts",
                column: "RentalPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_VehicleId",
                table: "RentalContracts",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_NumberPlate",
                table: "Vehicles",
                column: "NumberPlate",
                unique: true);

            SeedData(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalContracts");

            migrationBuilder.DropTable(
                name: "DeliveryDrivers");

            migrationBuilder.DropTable(
                name: "RentalPlans");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }

        private void SeedData(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow;

            migrationBuilder.InsertData(
                table: "RentalPlans",
                columns: ["Id", "Description", "CostPerDay", "PercentageOfFineForReturnBeforeExpectedEndDatePerDay", "AmountOfFineForReturnAfterExpectedEndDatePerDay", "Status", "Deleted", "CreatedDate", "UpdatedDate"],
                values: new object[,]
                {
                    { 1, "7 dias", 30.0, 20.0, 50.0, (int)StatusType.Active, false, now, null },
                    { 2, "15 dias", 28.0, 40.0, 50.0,(int)StatusType.Active, false, now, null },
                    { 3, "30 dias", 22.0, 0, 50.0, (int)StatusType.Active, false, now, null },
                    { 4, "45 dias", 20.0, 0, 50.0, (int)StatusType.Active, false, now, null },
                    { 5, "50 dias", 18.0, 0, 50.0, (int)StatusType.Active, false, now, null },
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: ["Id", "Type", "Make", "Model", "YearOfManufacture", "NumberPlate", "Status", "Deleted", "CreatedDate", "UpdatedDate"],
                values: new object[,]
                {
                    { 1, (int)VehicleType.Motorcycle, "Honda", "CG 160", 2023, "ABC1234", (int)StatusType.Active, false, now, null },
                    { 2, (int)VehicleType.Motorcycle, "Yamaha", "Fazer 250", 2023, "DEF5678", (int)StatusType.Active, false, now, null },
                    { 3, (int)VehicleType.Motorcycle, "Honda", "Biz 125", 2023, "GHI9012", (int)StatusType.Active, false, now, null },
                    { 4, (int)VehicleType.Motorcycle, "Yamaha", "XTZ 150 Crosser", 2023, "JKL3456", (int)StatusType.Active, false, now, null },
                    { 5, (int)VehicleType.Motorcycle, "Honda", "CB 500X", 2023, "MNO7890", (int)StatusType.Active, false, now, null },
                    { 6, (int)VehicleType.Motorcycle, "Yamaha", "MT-03", 2023, "PQR1234", (int)StatusType.Active, false, now, null },
                    { 7, (int)VehicleType.Motorcycle, "Honda", "XRE 300", 2023, "STU5678", (int)StatusType.Active, false, now, null },
                    { 8, (int)VehicleType.Motorcycle, "Yamaha", "Lander 250", 2023, "VWX9012", (int)StatusType.Active, false, now, null },
                    { 9, (int)VehicleType.Motorcycle, "Honda", "CB 650R", 2023, "YZA3456", (int)StatusType.Active, false, now, null },
                    { 10, (int)VehicleType.Motorcycle, "Yamaha", "NMax 160", 2023, "BCD7890", (int)StatusType.Active, false, now, null },
                    { 11, (int)VehicleType.Motorcycle, "Honda", "PCX 150", 2023, "EFG1234", (int)StatusType.Active, false, now, null },
                    { 12, (int)VehicleType.Motorcycle, "Yamaha", "Factor 150", 2023, "HIJ5678", (int)StatusType.Active, false, now, null },
                    { 13, (int)VehicleType.Motorcycle, "Honda", "CB 300R", 2023, "KLM9012", (int)StatusType.Active, false, now, null },
                    { 14, (int)VehicleType.Motorcycle, "Yamaha", "Tenere 700", 2023, "NOP3456", (int)StatusType.Active, false, now, null },
                    { 15, (int)VehicleType.Motorcycle, "Honda", "Africa Twin", 2023, "QRS7890", (int)StatusType.Active, false, now, null }
                });
        }
    }
}
