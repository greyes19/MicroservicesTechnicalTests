using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movement.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovementHeaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                    OriginDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovementHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementDetails_MovementHeaders_MovementHeaderId",
                        column: x => x.MovementHeaderId,
                        principalTable: "MovementHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovementDetails_MovementHeaderId",
                table: "MovementDetails",
                column: "MovementHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovementDetails");

            migrationBuilder.DropTable(
                name: "MovementHeaders");
        }
    }
}
