﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using books;

namespace Migrations
{
    [DbContext(typeof(CurrentContext))]
    [Migration("20181226154908_initial_create")]
    partial class initial_create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Description");

                    b.Property<string>("ISBN10");

                    b.Property<string>("Image");

                    b.Property<int>("Pages");

                    b.Property<double>("Price");

                    b.Property<short>("Rating");

                    b.Property<string>("Title");

                    b.Property<int>("Votes");

                    b.HasKey("Id");

                    b.ToTable("books");
                });

            modelBuilder.Entity("books.Item", b =>
                {
                    b.Property<Guid>("Client")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Client");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("books.payment_log", b =>
                {
                    b.Property<Guid>("Client")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Item");

                    b.Property<int>("Quantity");

                    b.Property<DateTime>("_When");

                    b.HasKey("Client");

                    b.ToTable("payment_history");
                });
#pragma warning restore 612, 618
        }
    }
}
