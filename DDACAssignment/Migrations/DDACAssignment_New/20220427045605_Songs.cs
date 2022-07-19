using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDACAssignment.Migrations.DDACAssignment_New
{
    public partial class Songs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordingSession",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionID = table.Column<int>(nullable: false),
                    SongName = table.Column<string>(maxLength: 100, nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    ProducerName = table.Column<string>(maxLength: 100, nullable: false),
                    ComposerName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordingSession", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(maxLength: 100, nullable: false),
                    SongUploadDate = table.Column<DateTime>(nullable: false),
                    SongGenre = table.Column<string>(nullable: false),
                    ProducerName = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordingSession");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
