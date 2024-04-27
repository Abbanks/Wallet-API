using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES ('1', 'noob', 'NOOB', '8A9D4974-C82A-4794-8708-B667E5F5BAAC'),  
                           ('2', 'elite', 'ELITE', '95F388A5-2614-4FE9-A4BC-93453236D016'),
                           ('3', 'admin', 'ADMIN', 'D1A3D3A4-3A3D-4A3D-8A3D-3A3D3A3D3A3D');
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
