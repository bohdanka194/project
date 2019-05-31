using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations
{
    public partial class initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string cmd = File.ReadAllText(Environment.CurrentDirectory + "\\Migrations\\sql\\create.sql");

            migrationBuilder.Sql(cmd, true);
        } 

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
