﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestPoint.DAL.Contexts;

#nullable disable

namespace TestPoint.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestPoint.Domain.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AdministratorId");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.ToTable("Administrator", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AnswerId");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("AnswerText");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsCorrect")
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasColumnName("IsCorrect");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.AnswerHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AnswerHistoryId");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("AnswerText");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TestCompletionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestCompletionId");

                    b.ToTable("AnswerHistory", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("QuestionId");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("QuestionText");

                    b.Property<byte>("QuestionType")
                        .HasColumnType("tinyint")
                        .HasColumnName("QuestionType");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.SystemLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LoginId");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("LoginType")
                        .HasColumnType("tinyint")
                        .HasColumnName("LoginType");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("PasswordHash");

                    b.Property<bool>("PasswordReseted")
                        .HasColumnType("bit")
                        .HasColumnName("PasswordReseted");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("RegistryDate");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username", "LoginType")
                        .IsUnique()
                        .HasDatabaseName("UQ_Login_Username_LoginType");

                    b.ToTable("Login", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TestId");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("Author");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Difficulty")
                        .HasColumnType("tinyint")
                        .HasColumnName("Difficulty");

                    b.Property<int>("EstimatedTime")
                        .HasColumnType("int")
                        .HasColumnName("EstimatedTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId", "Name")
                        .IsUnique()
                        .HasDatabaseName("UQ_Test_AuthorId_Name");

                    b.ToTable("Test", (string)null);

                    b.HasCheckConstraint("CK_Test_EstimatedTime", "EstimatedTime > 0");
                });

            modelBuilder.Entity("TestPoint.Domain.TestAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TestAssignmentId");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TestId");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TestId", "UserId")
                        .IsUnique()
                        .HasDatabaseName("UQ_TestAssignment_TestId_UserId");

                    b.ToTable("TestAssignment", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.TestCompletion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TestCompletionId");

                    b.Property<double>("CompletionTime")
                        .HasColumnType("float")
                        .HasColumnName("CompletionTime");

                    b.Property<int>("CorrectAnswersCount")
                        .HasColumnType("int")
                        .HasColumnName("CorrectAnswersCount");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Score")
                        .HasColumnType("float")
                        .HasColumnName("Score");

                    b.Property<Guid>("TestAssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TestAssignmentId")
                        .IsUnique();

                    b.ToTable("TestCompletion", (string)null);

                    b.HasCheckConstraint("CK_TestCompletion_CompletionTime", "CompletionTime > 0");

                    b.HasCheckConstraint("CK_TestCompletion_CorrectAnswersCount", "CorrectAnswersCount >= 0");

                    b.HasCheckConstraint("CK_TestCompletion_Score", "Score >= 0");
                });

            modelBuilder.Entity("TestPoint.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Avatar");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)")
                        .HasColumnName("Email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("LastName");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UQ_User_Email");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("TestPoint.Domain.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserGroupId");

                    b.Property<Guid>("AdministratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("Name", "AdministratorId")
                        .IsUnique()
                        .HasDatabaseName("UQ_UserGroup_Name_AdministratorId");

                    b.ToTable("UserGroup", (string)null);
                });

            modelBuilder.Entity("UserUserGroupBridge", b =>
                {
                    b.Property<Guid>("UserGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserGroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserUserGroupBridge");
                });

            modelBuilder.Entity("TestPoint.Domain.Administrator", b =>
                {
                    b.HasOne("TestPoint.Domain.SystemLogin", "Login")
                        .WithOne()
                        .HasForeignKey("TestPoint.Domain.Administrator", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("TestPoint.Domain.Answer", b =>
                {
                    b.HasOne("TestPoint.Domain.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestPoint.Domain.AnswerHistory", b =>
                {
                    b.HasOne("TestPoint.Domain.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TestPoint.Domain.TestCompletion", null)
                        .WithMany("Answers")
                        .HasForeignKey("TestCompletionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestPoint.Domain.Question", b =>
                {
                    b.HasOne("TestPoint.Domain.Test", null)
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestPoint.Domain.Test", b =>
                {
                    b.HasOne("TestPoint.Domain.Administrator", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestPoint.Domain.TestAssignment", b =>
                {
                    b.HasOne("TestPoint.Domain.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestPoint.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestPoint.Domain.TestCompletion", b =>
                {
                    b.HasOne("TestPoint.Domain.TestAssignment", null)
                        .WithOne("TestCompletion")
                        .HasForeignKey("TestPoint.Domain.TestCompletion", "TestAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestPoint.Domain.User", b =>
                {
                    b.HasOne("TestPoint.Domain.SystemLogin", "Login")
                        .WithOne()
                        .HasForeignKey("TestPoint.Domain.User", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("TestPoint.Domain.UserGroup", b =>
                {
                    b.HasOne("TestPoint.Domain.Administrator", null)
                        .WithMany()
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserUserGroupBridge", b =>
                {
                    b.HasOne("TestPoint.Domain.UserGroup", null)
                        .WithMany()
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestPoint.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("TestPoint.Domain.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("TestPoint.Domain.Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("TestPoint.Domain.TestAssignment", b =>
                {
                    b.Navigation("TestCompletion");
                });

            modelBuilder.Entity("TestPoint.Domain.TestCompletion", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
