using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DBContext.Migrations
{
    /// <inheritdoc />
    public partial class ourServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OurServices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TitleEn = table.Column<string>(type: "longtext", nullable: false),
                    TitleAr = table.Column<string>(type: "longtext", nullable: false),
                    BodyAr = table.Column<string>(type: "longtext", nullable: false),
                    BodyEn = table.Column<string>(type: "longtext", nullable: false),
                    Icon = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServices", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurServices");
        }
    }
}
