using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DBContext.Migrations
{
    /// <inheritdoc />
    public partial class ReAddMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NameAR = table.Column<string>(type: "longtext", nullable: false),
                    NameEN = table.Column<string>(type: "longtext", nullable: false),
                    JobTitleAR = table.Column<string>(type: "longtext", nullable: false),
                    JobTitleEN = table.Column<string>(type: "longtext", nullable: false),
                    FacebookLink = table.Column<string>(type: "longtext", nullable: false),
                    InstagramLink = table.Column<string>(type: "longtext", nullable: false),
                    SkypeLink = table.Column<string>(type: "longtext", nullable: false),
                    TwitterLink = table.Column<string>(type: "longtext", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
