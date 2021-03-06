﻿// <auto-generated />
using System;
using Chat.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chat.Database.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20210613093059_ChatUserIdToUserEmail")]
    partial class ChatUserIdToUserEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chat.Domain.Aggregates.GroupChatAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("GroupChatAggregate");
                });

            modelBuilder.Entity("Chat.Domain.Aggregates.PrivateChatAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("User1Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User2Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrivateChatAggregate");
                });

            modelBuilder.Entity("Chat.Domain.Aggregates.GroupChatAggregate", b =>
                {
                    b.OwnsMany("Chat.Domain.ValueObjects.Message", "Messages", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ChatGroupId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Text")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserEmail")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("ChatGroupId");

                            b1.ToTable("GroupChatMessages");

                            b1.WithOwner()
                                .HasForeignKey("ChatGroupId");
                        });

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Chat.Domain.Aggregates.PrivateChatAggregate", b =>
                {
                    b.OwnsMany("Chat.Domain.ValueObjects.Message", "Messages", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("PrivateChatId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Text")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserEmail")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("PrivateChatId");

                            b1.ToTable("PrivateChatMessages");

                            b1.WithOwner()
                                .HasForeignKey("PrivateChatId");
                        });

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
