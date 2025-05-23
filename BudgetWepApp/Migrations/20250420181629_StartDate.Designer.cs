﻿// <auto-generated />
using System;
using BudgetWepApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetWepApp.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20250420181629_StartDate")]
    partial class StartDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BudgetWepApp.Models.Goal", b =>
                {
                    b.Property<int>("GoalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GoalID"));

                    b.Property<DateTime>("DateAddded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("GoalID");

                    b.HasIndex("UserID");

                    b.ToTable("Goals");

                    b.HasData(
                        new
                        {
                            GoalID = 1,
                            DateAddded = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "This is the first goal.",
                            Name = "Goal 1",
                            UserID = 1
                        },
                        new
                        {
                            GoalID = 2,
                            DateAddded = new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "This is the second goal.",
                            Name = "Goal 2",
                            UserID = 2
                        });
                });

            modelBuilder.Entity("BudgetWepApp.Models.Income", b =>
                {
                    b.Property<int>("IncomeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeID"));

                    b.Property<double>("IncomeAmmount")
                        .HasColumnType("float");

                    b.Property<int>("PayPeriodDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("IncomeID");

                    b.HasIndex("UserID");

                    b.ToTable("Incomes");

                    b.HasData(
                        new
                        {
                            IncomeID = 1,
                            IncomeAmmount = 100.0,
                            PayPeriodDays = 14,
                            StartingDate = new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 1
                        },
                        new
                        {
                            IncomeID = 2,
                            IncomeAmmount = 200.0,
                            PayPeriodDays = 7,
                            StartingDate = new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 2
                        });
                });

            modelBuilder.Entity("BudgetWepApp.Models.RecurringPayment", b =>
                {
                    b.Property<int>("RecurringPaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecurringPaymentId"));

                    b.Property<int>("PaymenFrequencyDays")
                        .HasColumnType("int");

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RecurringPaymentId");

                    b.HasIndex("UserID");

                    b.ToTable("recurringPayments");

                    b.HasData(
                        new
                        {
                            RecurringPaymentId = 1,
                            PaymenFrequencyDays = 14,
                            PaymentAmount = 100.0,
                            StartingDate = new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 1
                        },
                        new
                        {
                            RecurringPaymentId = 2,
                            PaymenFrequencyDays = 7,
                            PaymentAmount = 200.0,
                            StartingDate = new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 2
                        });
                });

            modelBuilder.Entity("BudgetWepApp.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionID"));

                    b.Property<double>("Ammount")
                        .HasColumnType("float");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("TransactionID");

                    b.HasIndex("UserID");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            TransactionID = 1,
                            Ammount = 100.0,
                            TimeStamp = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 1
                        },
                        new
                        {
                            TransactionID = 2,
                            Ammount = -50.0,
                            TimeStamp = new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 1
                        },
                        new
                        {
                            TransactionID = 3,
                            Ammount = 200.0,
                            TimeStamp = new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserID = 2
                        });
                });

            modelBuilder.Entity("BudgetWepApp.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime>("AccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("CurrentBalance")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            AccountCreationDate = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentBalance = 1000.0,
                            Email = "test1@email.com",
                            IsAdmin = true,
                            Password = "1234",
                            UserName = "Name1"
                        },
                        new
                        {
                            UserID = 2,
                            AccountCreationDate = new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentBalance = 2000.0,
                            Email = "test2@email.com",
                            IsAdmin = true,
                            Password = "1234",
                            UserName = "Name2"
                        });
                });

            modelBuilder.Entity("BudgetWepApp.Models.Goal", b =>
                {
                    b.HasOne("BudgetWepApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetWepApp.Models.Income", b =>
                {
                    b.HasOne("BudgetWepApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetWepApp.Models.RecurringPayment", b =>
                {
                    b.HasOne("BudgetWepApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetWepApp.Models.Transaction", b =>
                {
                    b.HasOne("BudgetWepApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
