using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FetPamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    experience = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    volunteer_details = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volunteers", x => x.id);
                    table.CheckConstraint("CK_volunteers_email", "\"email\" ~ '^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$'");
                    table.CheckConstraint("CK_volunteers_experience", "\"experience\" >= 0");
                    table.CheckConstraint("CK_volunteers_phone_number", "\"phone_number\" ~ '^\\+?\\d{1,15}$'");
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_breeds", x => x.id);
                    table.ForeignKey(
                        name: "FK_breeds_species_species_id",
                        column: x => x.species_id,
                        principalTable: "species",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    treatment_status = table.Column<string>(type: "text", nullable: false),
                    weight = table.Column<decimal>(type: "numeric", nullable: false),
                    height = table.Column<decimal>(type: "numeric", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: false),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    help_status = table.Column<string>(type: "text", nullable: false),
                    address_apartment_number = table.Column<int>(type: "integer", nullable: true),
                    address_building = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address_street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    info_breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    info_color = table.Column<string>(type: "text", nullable: false),
                    info_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_info = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.id);
                    table.CheckConstraint("address_apartment_number", "\"address_apartment_number\" IS NULL OR \"address_apartment_number\" > 0");
                    table.CheckConstraint("CK_pets_address_building", "\"address_building\" ~ '^[a-zA-ZА-Яа-яЁё0-9\\-]+$'");
                    table.CheckConstraint("CK_pets_address_city", "\"address_city\" ~ '^[a-zA-Zа-яА-ЯёЁ\\s\\-]+$'");
                    table.CheckConstraint("CK_pets_address_street", "\"address_street\" ~ '^[a-zA-ZА-Яа-яЁё0-9\\-\\. ]+$'");
                    table.CheckConstraint("CK_pets_dob", "\"dob\" <= CURRENT_DATE");
                    table.CheckConstraint("CK_pets_height", "\"height\" > 0 AND \"height\" <= 10000");
                    table.CheckConstraint("CK_pets_phone_number", "\"phone_number\" ~ '^\\+?\\d{1,15}$'");
                    table.CheckConstraint("CK_pets_weight", "\"weight\" > 0 AND \"weight\" <= 100");
                    table.ForeignKey(
                        name: "FK_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_breeds_species_id",
                table: "breeds",
                column: "species_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
