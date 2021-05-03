﻿// <auto-generated />
using System;
using Groups.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Groups.Database.Migrations
{
    [DbContext(typeof(GroupsDbContext))]
    [Migration("20210503221637_RemoveManyCorrectAnswersColumn")]
    partial class RemoveManyCorrectAnswersColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Groups.Domain.Aggregates.GroupAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GroupAggregate");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.QuestionAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ClosedQuestion")
                        .HasColumnType("bit");

                    b.Property<Guid?>("TestAggregateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TestAggregateId");

                    b.ToTable("QuestionAggregate");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.TestAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PassedFrom")
                        .HasColumnType("int");

                    b.Property<bool>("RequirePhoto")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("TestAggregate");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.TestResultAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestResultAggregate");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.GroupAggregate", b =>
                {
                    b.OwnsMany("Groups.Domain.ValueObjects.Participient", "Participients", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("GroupId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Role")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Role");

                            b1.HasKey("Id");

                            b1.HasIndex("GroupId");

                            b1.ToTable("Participient");

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.Navigation("Participients");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.QuestionAggregate", b =>
                {
                    b.HasOne("Groups.Domain.Aggregates.TestAggregate", null)
                        .WithMany("Questions")
                        .HasForeignKey("TestAggregateId");

                    b.OwnsOne("Core.Domain.ValueObjects.Photo", "Photo", b1 =>
                        {
                            b1.Property<Guid>("QuestionAggregateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte[]>("Image")
                                .HasColumnType("varbinary(max)")
                                .HasColumnName("Photo");

                            b1.HasKey("QuestionAggregateId");

                            b1.ToTable("QuestionAggregate");

                            b1.WithOwner()
                                .HasForeignKey("QuestionAggregateId");
                        });

                    b.OwnsMany("Groups.Domain.ValueObjects.Answer", "Answers", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<bool>("Correct")
                                .HasColumnType("bit");

                            b1.HasKey("QuestionId", "Value", "Correct");

                            b1.ToTable("Answer");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");
                        });

                    b.Navigation("Answers");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.TestAggregate", b =>
                {
                    b.HasOne("Groups.Domain.Aggregates.GroupAggregate", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.TestResultAggregate", b =>
                {
                    b.HasOne("Groups.Domain.Aggregates.TestAggregate", "Test")
                        .WithMany()
                        .HasForeignKey("TestId");

                    b.OwnsOne("Groups.Domain.ValueObjects.Participient", "Student", b1 =>
                        {
                            b1.Property<Guid>("TestResultAggregateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TestResultAggregateId");

                            b1.ToTable("TestResultAggregate");

                            b1.WithOwner()
                                .HasForeignKey("TestResultAggregateId");
                        });

                    b.OwnsMany("Groups.Domain.ValueObjects.StudentAnswer", "StudentAnswers", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("PointsForAnswer")
                                .HasColumnType("int");

                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("ReceivedAnswers")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("TestResultAggregateId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("QuestionId");

                            b1.HasIndex("TestResultAggregateId");

                            b1.ToTable("StudentAnswer");

                            b1.HasOne("Groups.Domain.Aggregates.QuestionAggregate", "Question")
                                .WithMany()
                                .HasForeignKey("QuestionId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TestResultAggregateId");

                            b1.Navigation("Question");
                        });

                    b.Navigation("Student")
                        .IsRequired();

                    b.Navigation("StudentAnswers");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Groups.Domain.Aggregates.TestAggregate", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
