﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetriNetEngine.Infrastructure;

#nullable disable

namespace PetriNetEngine.Migrations
{
    [DbContext(typeof(PetriNetContext))]
    partial class PetriNetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SEA_Models.PetriNet.Arc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("PetriNetId")
                        .HasColumnType("integer");

                    b.Property<int>("SourceNode")
                        .HasColumnType("integer");

                    b.Property<int>("TargetNode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PetriNetId");

                    b.ToTable("Arcs");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.PetriNet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PetriNets");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("NumberOfTokens")
                        .HasColumnType("integer");

                    b.Property<int?>("PetriNetId")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PetriNetId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.Transition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PetriNetId")
                        .HasColumnType("integer");

                    b.Property<int>("TransitionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PetriNetId");

                    b.ToTable("Transitions");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.Arc", b =>
                {
                    b.HasOne("SEA_Models.PetriNet.PetriNet", "PetriNet")
                        .WithMany("Arcs")
                        .HasForeignKey("PetriNetId");

                    b.Navigation("PetriNet");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.Place", b =>
                {
                    b.HasOne("SEA_Models.PetriNet.PetriNet", "PetriNet")
                        .WithMany("Places")
                        .HasForeignKey("PetriNetId");

                    b.Navigation("PetriNet");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.Transition", b =>
                {
                    b.HasOne("SEA_Models.PetriNet.PetriNet", "PetriNet")
                        .WithMany("Transitions")
                        .HasForeignKey("PetriNetId");

                    b.Navigation("PetriNet");
                });

            modelBuilder.Entity("SEA_Models.PetriNet.PetriNet", b =>
                {
                    b.Navigation("Arcs");

                    b.Navigation("Places");

                    b.Navigation("Transitions");
                });
#pragma warning restore 612, 618
        }
    }
}
