﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20210926134826_Add-Tb_Online")]
    partial class AddTb_Online
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("QianShiChat.Server.Models.Entities.Online", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasComment("在线表主键");

                    b.Property<string>("ConnectionId")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasComment("用户编号");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("online");
                });

            modelBuilder.Entity("QianShiChat.Server.Models.Entities.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasComment("用户表主键");

                    b.Property<string>("Avatar")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasComment("用户头像");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasComment("用户昵称");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasComment("登录密码");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasComment("用户登录名");

                    b.HasKey("Id");

                    b.ToTable("userinfo");
                });

            modelBuilder.Entity("QianShiChat.Server.Models.Entities.Online", b =>
                {
                    b.HasOne("QianShiChat.Server.Models.Entities.UserInfo", "UserInfo")
                        .WithMany("Onlines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("QianShiChat.Server.Models.Entities.UserInfo", b =>
                {
                    b.Navigation("Onlines");
                });
#pragma warning restore 612, 618
        }
    }
}
