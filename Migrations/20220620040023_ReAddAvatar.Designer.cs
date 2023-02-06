﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatThreeRole.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20220620040023_ReAddAvatar")]
    partial class ReAddAvatar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ChatThreeRole.Models.AccountModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Email")
                        .HasName("tbl_account_key");

                    b.ToTable("tbl_account", (string)null);
                });

            modelBuilder.Entity("ChatThreeRole.Models.GroupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageOfGroup")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_group", (string)null);
                });

            modelBuilder.Entity("ChatThreeRole.Models.MessageModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<string>("Messsage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupID");

                    b.ToTable("tbl_message", (string)null);
                });

            modelBuilder.Entity("ChatThreeRole.Models.MessageModel", b =>
                {
                    b.HasOne("ChatThreeRole.Models.GroupModel", "Group")
                        .WithMany("MessageModels")
                        .HasForeignKey("GroupID")
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ChatThreeRole.Models.GroupModel", b =>
                {
                    b.Navigation("MessageModels");
                });
#pragma warning restore 612, 618
        }
    }
}
