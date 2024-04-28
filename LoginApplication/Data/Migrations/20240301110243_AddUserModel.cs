using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginApplication.Migrations
{
	/// <inheritdoc />
	public partial class AddUserModel : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
					Password = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
					IsAdmin = table.Column<bool>(type: "bit", nullable: false),
					IsLoggedIn = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
