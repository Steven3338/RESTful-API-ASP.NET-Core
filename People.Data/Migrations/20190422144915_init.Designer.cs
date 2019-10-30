﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeopleInformation.Data;

namespace PeopleInformation.Data.Migrations
{
    [DbContext(typeof(PeopleInformationContext))]
    [Migration("20190422144915_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PeopleInformation.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("County");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("MoveInDate");

                    b.Property<string>("MoveOutDate");

                    b.Property<int?>("ServiceProviderEmployeeId");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceProviderEmployeeId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeOfInitialMessage");

                    b.Property<DateTime>("LastModified");

                    b.Property<bool>("Resolved");

                    b.Property<string>("Subject");

                    b.Property<DateTime>("TimeToResolution");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Employeer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Email");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Employeers");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId");

                    b.Property<DateTime>("DateTimeOfMessage");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("MessageText");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("PeopleInformation.Domain.ServiceProviderEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int?>("EmailsId");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastName");

                    b.Property<bool>("LogIn");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("EmailsId");

                    b.ToTable("ServiceProviderEmployees");
                });

            modelBuilder.Entity("PeopleInformation.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PeopleInformation.Domain.UserEmployeer", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("EmployeerId");

                    b.Property<string>("EmployeerId1");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("UserId", "EmployeerId");

                    b.HasIndex("EmployeerId1");

                    b.ToTable("UserEmployeers");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Address", b =>
                {
                    b.HasOne("PeopleInformation.Domain.User", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PeopleInformation.Domain.ServiceProviderEmployee")
                        .WithMany("Addresses")
                        .HasForeignKey("ServiceProviderEmployeeId");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Case", b =>
                {
                    b.HasOne("PeopleInformation.Domain.User", "User")
                        .WithMany("Cases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PeopleInformation.Domain.Email", b =>
                {
                    b.HasOne("PeopleInformation.Domain.User", "User")
                        .WithMany("Emails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PeopleInformation.Domain.Employeer", b =>
                {
                    b.HasOne("PeopleInformation.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("PeopleInformation.Domain.Message", b =>
                {
                    b.HasOne("PeopleInformation.Domain.Case", "Case")
                        .WithMany("Messages")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PeopleInformation.Domain.ServiceProviderEmployee", b =>
                {
                    b.HasOne("PeopleInformation.Domain.Email", "Emails")
                        .WithMany()
                        .HasForeignKey("EmailsId");
                });

            modelBuilder.Entity("PeopleInformation.Domain.UserEmployeer", b =>
                {
                    b.HasOne("PeopleInformation.Domain.Employeer", "Employeer")
                        .WithMany("UserEmployeers")
                        .HasForeignKey("EmployeerId1");

                    b.HasOne("PeopleInformation.Domain.User", "User")
                        .WithMany("UserEmployeers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
