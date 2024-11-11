﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVerifiedToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Reviews");
        }
    }
}
