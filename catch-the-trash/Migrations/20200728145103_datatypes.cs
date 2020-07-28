using Microsoft.EntityFrameworkCore.Migrations;

namespace catch_the_trash.Migrations
{
    public partial class datatypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Report",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Report",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                table: "Report",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Report",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
