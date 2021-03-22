using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TownerTown.DataAccess.Migrations
{
    public partial class createAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DayToTimeMapping",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    Closed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayToTimeMapping", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PinCode = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Lattitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionNumber = table.Column<string>(nullable: true),
                    PayedOn = table.Column<DateTime>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    WhatsAppNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Timings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MondayID = table.Column<int>(nullable: true),
                    TuesdayID = table.Column<int>(nullable: true),
                    WednesdayID = table.Column<int>(nullable: true),
                    ThursdayID = table.Column<int>(nullable: true),
                    FridayID = table.Column<int>(nullable: true),
                    SaturdayID = table.Column<int>(nullable: true),
                    SundayID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_FridayID",
                        column: x => x.FridayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_MondayID",
                        column: x => x.MondayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_SaturdayID",
                        column: x => x.SaturdayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_SundayID",
                        column: x => x.SundayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_ThursdayID",
                        column: x => x.ThursdayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_TuesdayID",
                        column: x => x.TuesdayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timings_DayToTimeMapping_WednesdayID",
                        column: x => x.WednesdayID,
                        principalTable: "DayToTimeMapping",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessID = table.Column<string>(nullable: true),
                    BusinessName = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    BusinessOwnerID = table.Column<int>(nullable: true),
                    RegisteredOn = table.Column<DateTime>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    AddressID = table.Column<int>(nullable: true),
                    TimingsID = table.Column<int>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    Products = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true),
                    WebsiteLink = table.Column<string>(nullable: true),
                    Membership = table.Column<int>(nullable: false),
                    PaymentID = table.Column<int>(nullable: true),
                    OverallRating = table.Column<int>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    BlockReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Business_Locations_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Business_Users_BusinessOwnerID",
                        column: x => x.BusinessOwnerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Business_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Business_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Business_Timings_TimingsID",
                        column: x => x.TimingsID,
                        principalTable: "Timings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    BusinessID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Business_BusinessID",
                        column: x => x.BusinessID,
                        principalTable: "Business",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilePath",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    BusinessID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePath", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FilePath_Business_BusinessID",
                        column: x => x.BusinessID,
                        principalTable: "Business",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(nullable: false),
                    ReviewMessage = table.Column<string>(nullable: true),
                    ReviewdOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<long>(nullable: false),
                    BusinessID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Review_Business_BusinessID",
                        column: x => x.BusinessID,
                        principalTable: "Business",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_AddressID",
                table: "Business",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Business_BusinessOwnerID",
                table: "Business",
                column: "BusinessOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Business_CategoryID",
                table: "Business",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Business_PaymentID",
                table: "Business",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Business_TimingsID",
                table: "Business",
                column: "TimingsID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_BusinessID",
                table: "Contacts",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_FilePath_BusinessID",
                table: "FilePath",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BusinessID",
                table: "Review",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_FridayID",
                table: "Timings",
                column: "FridayID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_MondayID",
                table: "Timings",
                column: "MondayID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_SaturdayID",
                table: "Timings",
                column: "SaturdayID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_SundayID",
                table: "Timings",
                column: "SundayID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_ThursdayID",
                table: "Timings",
                column: "ThursdayID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_TuesdayID",
                table: "Timings",
                column: "TuesdayID");

            migrationBuilder.CreateIndex(
                name: "IX_Timings_WednesdayID",
                table: "Timings",
                column: "WednesdayID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "FilePath");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Timings");

            migrationBuilder.DropTable(
                name: "DayToTimeMapping");
        }
    }
}
