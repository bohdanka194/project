using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookstorebackend.Migrations
{
    public partial class initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string baseAddress = Environment.CurrentDirectory + "\\Migrations";

            string[] files = Directory.GetFiles(baseAddress + "\\sql");

            migrationBuilder.Sql(BuildCmd(files).ToString());

            string[] stored_procedures = Directory.GetFiles(baseAddress + "\\sp");
            foreach (var item in stored_procedures.Select(file => File.ReadAllText(file)))
            {
                migrationBuilder.Sql(item);
            } 
        }

        private static StringBuilder BuildCmd(string[] files)
        {
            StringBuilder cmtText = new StringBuilder();
            foreach (var item in files.Select(file => File.ReadAllText(file)))
            {
                cmtText.Append(item);
                cmtText.AppendLine();
            }

            return cmtText;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop procedure update_cart;
                                   drop procedure log_payment_details");
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "payment_history"); 
        }
    }
}
