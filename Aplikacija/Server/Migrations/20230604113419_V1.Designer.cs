﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace MojGradproject.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230604113419_V1")]
    partial class V1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Dogadjaji", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<double>("CenaKarte")
                        .HasColumnType("float");

                    b.Property<DateTime>("DatumVreme")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrganizatorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrganizatorID");

                    b.ToTable("Dogadjaji");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Models.Glumci", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("FilmoviID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PredstaveID")
                        .HasColumnType("int");

                    b.Property<string>("Prezume")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FilmoviID");

                    b.HasIndex("PredstaveID");

                    b.ToTable("Glumci");
                });

            modelBuilder.Entity("Models.Gosti", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KoncertiID")
                        .HasColumnType("int");

                    b.Property<string>("Prezume")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("KoncertiID");

                    b.ToTable("Gosti");
                });

            modelBuilder.Entity("Models.Korisnici", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipKorisnika")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Models.Filmovi", b =>
                {
                    b.HasBaseType("Models.Dogadjaji");

                    b.Property<int>("BrMesta")
                        .HasColumnType("int");

                    b.Property<int?>("KorisniciID")
                        .HasColumnType("int");

                    b.Property<string>("KratakOpis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VremeTrajanjaUMin")
                        .HasColumnType("int");

                    b.Property<string>("Zanr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("KorisniciID");

                    b.ToTable("Filmovi");
                });

            modelBuilder.Entity("Models.Koncerti", b =>
                {
                    b.HasBaseType("Models.Dogadjaji");

                    b.Property<int>("BrMesta")
                        .HasColumnType("int");

                    b.Property<string>("Izvodjac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KorisniciID")
                        .HasColumnType("int");

                    b.HasIndex("KorisniciID");

                    b.ToTable("Koncerti");
                });

            modelBuilder.Entity("Models.OstaliDogadjaji", b =>
                {
                    b.HasBaseType("Models.Dogadjaji");

                    b.Property<int?>("KorisniciID")
                        .HasColumnType("int");

                    b.Property<string>("KratakOpis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TrajeDo")
                        .HasColumnType("datetime2");

                    b.HasIndex("KorisniciID");

                    b.ToTable("OstaliDogadjaji");
                });

            modelBuilder.Entity("Models.Predstave", b =>
                {
                    b.HasBaseType("Models.Dogadjaji");

                    b.Property<int>("BrMesta")
                        .HasColumnType("int");

                    b.Property<int?>("KorisniciID")
                        .HasColumnType("int");

                    b.Property<string>("KratakOpis")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("KorisniciID");

                    b.ToTable("Predstave");
                });

            modelBuilder.Entity("Models.Utakmice", b =>
                {
                    b.HasBaseType("Models.Dogadjaji");

                    b.Property<int>("BrMesta")
                        .HasColumnType("int");

                    b.Property<string>("Klub1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Klub2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KorisniciID")
                        .HasColumnType("int");

                    b.Property<string>("Sport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("KorisniciID");

                    b.ToTable("Utakmice");
                });

            modelBuilder.Entity("Models.Dogadjaji", b =>
                {
                    b.HasOne("Models.Korisnici", "Organizator")
                        .WithMany("Dogadjaji")
                        .HasForeignKey("OrganizatorID");

                    b.Navigation("Organizator");
                });

            modelBuilder.Entity("Models.Glumci", b =>
                {
                    b.HasOne("Models.Filmovi", null)
                        .WithMany("Glumci")
                        .HasForeignKey("FilmoviID");

                    b.HasOne("Models.Predstave", null)
                        .WithMany("Glumci")
                        .HasForeignKey("PredstaveID");
                });

            modelBuilder.Entity("Models.Gosti", b =>
                {
                    b.HasOne("Models.Koncerti", null)
                        .WithMany("Gosti")
                        .HasForeignKey("KoncertiID");
                });

            modelBuilder.Entity("Models.Filmovi", b =>
                {
                    b.HasOne("Models.Dogadjaji", null)
                        .WithOne()
                        .HasForeignKey("Models.Filmovi", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Korisnici", null)
                        .WithMany("OrganizovaniFilmovi")
                        .HasForeignKey("KorisniciID");
                });

            modelBuilder.Entity("Models.Koncerti", b =>
                {
                    b.HasOne("Models.Dogadjaji", null)
                        .WithOne()
                        .HasForeignKey("Models.Koncerti", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Korisnici", null)
                        .WithMany("OrganizovaniKoncerti")
                        .HasForeignKey("KorisniciID");
                });

            modelBuilder.Entity("Models.OstaliDogadjaji", b =>
                {
                    b.HasOne("Models.Dogadjaji", null)
                        .WithOne()
                        .HasForeignKey("Models.OstaliDogadjaji", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Korisnici", null)
                        .WithMany("OrganizovaniOstaliDogadjaji")
                        .HasForeignKey("KorisniciID");
                });

            modelBuilder.Entity("Models.Predstave", b =>
                {
                    b.HasOne("Models.Dogadjaji", null)
                        .WithOne()
                        .HasForeignKey("Models.Predstave", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Korisnici", null)
                        .WithMany("OrganizovanePredstave")
                        .HasForeignKey("KorisniciID");
                });

            modelBuilder.Entity("Models.Utakmice", b =>
                {
                    b.HasOne("Models.Dogadjaji", null)
                        .WithOne()
                        .HasForeignKey("Models.Utakmice", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Korisnici", null)
                        .WithMany("OrganizovaneUtakmice")
                        .HasForeignKey("KorisniciID");
                });

            modelBuilder.Entity("Models.Korisnici", b =>
                {
                    b.Navigation("Dogadjaji");

                    b.Navigation("OrganizovanePredstave");

                    b.Navigation("OrganizovaneUtakmice");

                    b.Navigation("OrganizovaniFilmovi");

                    b.Navigation("OrganizovaniKoncerti");

                    b.Navigation("OrganizovaniOstaliDogadjaji");
                });

            modelBuilder.Entity("Models.Filmovi", b =>
                {
                    b.Navigation("Glumci");
                });

            modelBuilder.Entity("Models.Koncerti", b =>
                {
                    b.Navigation("Gosti");
                });

            modelBuilder.Entity("Models.Predstave", b =>
                {
                    b.Navigation("Glumci");
                });
#pragma warning restore 612, 618
        }
    }
}
