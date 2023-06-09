﻿// <auto-generated />
using System;
using ApiResource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiResource.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiResource.Model.AccountBalance", b =>
                {
                    b.Property<string>("AccountBalanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("BankCustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountBalanceId");

                    b.HasIndex("BankCustomerId");

                    b.ToTable("AccountBalances");
                });

            modelBuilder.Entity("ApiResource.Model.BankCustomer", b =>
                {
                    b.Property<string>("BankCustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<double>("CustomerBalance")
                        .HasColumnType("float");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BankCustomerId");

                    b.ToTable("BankCustomers");
                });

            modelBuilder.Entity("ApiResource.Model.Credit", b =>
                {
                    b.Property<string>("CreditId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("BalanceAccountBalanceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("BalanceBeforeCredit")
                        .HasColumnType("float");

                    b.Property<string>("BankCustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreditedAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DebitedAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CreditId");

                    b.HasIndex("BalanceAccountBalanceId");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("ApiResource.Model.Debit", b =>
                {
                    b.Property<string>("DebitId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("BalanceAccountBalanceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("BalanceBeforeDebit")
                        .HasColumnType("float");

                    b.Property<string>("BankCustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreditedAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DebitedAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DebitId");

                    b.HasIndex("BalanceAccountBalanceId");

                    b.ToTable("Debits");
                });

            modelBuilder.Entity("ApiResource.Model.AccountBalance", b =>
                {
                    b.HasOne("ApiResource.Model.BankCustomer", null)
                        .WithMany("AccountBalanceHistory")
                        .HasForeignKey("BankCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiResource.Model.Credit", b =>
                {
                    b.HasOne("ApiResource.Model.AccountBalance", "Balance")
                        .WithMany()
                        .HasForeignKey("BalanceAccountBalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("ApiResource.Model.Debit", b =>
                {
                    b.HasOne("ApiResource.Model.AccountBalance", "Balance")
                        .WithMany()
                        .HasForeignKey("BalanceAccountBalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("ApiResource.Model.BankCustomer", b =>
                {
                    b.Navigation("AccountBalanceHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
