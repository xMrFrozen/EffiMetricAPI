using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EffiMetricAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRankAndTaskLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "WorkTasks",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "WorkTasks");
        }
    }
}
