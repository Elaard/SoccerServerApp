﻿// <auto-generated />
using System;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210922213310_AddIndexAndSetRelationshipBetweenBasedTables")]
    partial class AddIndexAndSetRelationshipBetweenBasedTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infrastructure.EFCore.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FirstName")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AwayTeamGoals")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HomeTeamGoals")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int?>("WinnterTeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<string>("Colors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PageWWW")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearOfFundation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.TeamStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AwayDraws")
                        .HasColumnType("int");

                    b.Property<int?>("AwayGoalsConceded")
                        .HasColumnType("int");

                    b.Property<int?>("AwayGoalsScored")
                        .HasColumnType("int");

                    b.Property<int?>("AwayLosts")
                        .HasColumnType("int");

                    b.Property<int?>("AwayWins")
                        .HasColumnType("int");

                    b.Property<int?>("DirectDraws")
                        .HasColumnType("int");

                    b.Property<int?>("DirectGoalsConceded")
                        .HasColumnType("int");

                    b.Property<int?>("DirectGoalsScored")
                        .HasColumnType("int");

                    b.Property<int?>("DirectLosts")
                        .HasColumnType("int");

                    b.Property<int?>("DirectMatches")
                        .HasColumnType("int");

                    b.Property<int?>("DirectMatchesPoints")
                        .HasColumnType("int");

                    b.Property<int?>("DirectWins")
                        .HasColumnType("int");

                    b.Property<int?>("HomeDraws")
                        .HasColumnType("int");

                    b.Property<int?>("HomeGoalsConceded")
                        .HasColumnType("int");

                    b.Property<int?>("HomeGoalsScored")
                        .HasColumnType("int");

                    b.Property<int?>("HomeLosts")
                        .HasColumnType("int");

                    b.Property<int?>("HomeWins")
                        .HasColumnType("int");

                    b.Property<int?>("Matches")
                        .HasColumnType("int");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("TotalDraws")
                        .HasColumnType("int");

                    b.Property<int?>("TotalGoalsConceded")
                        .HasColumnType("int");

                    b.Property<int?>("TotalGoalsScored")
                        .HasColumnType("int");

                    b.Property<int?>("TotalLosts")
                        .HasColumnType("int");

                    b.Property<int?>("TotalWins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("TeamsStatistics");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.Match", b =>
                {
                    b.HasOne("Infrastructure.EFCore.Models.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Infrastructure.EFCore.Models.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.Team", b =>
                {
                    b.HasOne("Infrastructure.EFCore.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.TeamStatistics", b =>
                {
                    b.HasOne("Infrastructure.EFCore.Models.Team", "Team")
                        .WithOne("TeamStatistics")
                        .HasForeignKey("Infrastructure.EFCore.Models.TeamStatistics", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Infrastructure.EFCore.Models.Team", b =>
                {
                    b.Navigation("AwayMatches");

                    b.Navigation("HomeMatches");

                    b.Navigation("TeamStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
