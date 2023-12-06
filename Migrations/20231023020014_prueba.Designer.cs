﻿// <auto-generated />
using ApiColegioPagos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiColegioPagos.Migrations
{
    [DbContext(typeof(ApiColegioPagosDbContext))]
    [Migration("20231023020014_prueba")]
    partial class prueba
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiColegioPagos.Models.Estudiante", b =>
                {
                    b.Property<int>("Est_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Est_id"));

                    b.Property<bool>("Est_activo")
                        .HasColumnType("bit");

                    b.Property<string>("Est_cedula")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Est_direccion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Est_nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Pen_id")
                        .HasColumnType("int");

                    b.HasKey("Est_id");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("ApiColegioPagos.Models.Global", b =>
                {
                    b.Property<int>("Glo_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Glo_id"));

                    b.Property<string>("Glo_nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Glo_valor")
                        .HasColumnType("int");

                    b.HasKey("Glo_id");

                    b.ToTable("Globals");
                });

            modelBuilder.Entity("ApiColegioPagos.Models.Pago", b =>
                {
                    b.Property<int>("Pag_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pag_id"));

                    b.Property<int>("Estudiante")
                        .HasColumnType("int");

                    b.Property<int>("Pag_cuota")
                        .HasColumnType("int");

                    b.Property<int>("Pen_id")
                        .HasColumnType("int");

                    b.HasKey("Pag_id");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("ApiColegioPagos.Models.Pen_id", b =>
                {
                    b.Property<int>("Pen_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pen_id"));

                    b.Property<string>("Pen_nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Pen_valor")
                        .HasColumnType("real");

                    b.HasKey("Pen_id");

                    b.ToTable("Pensiones");
                });
#pragma warning restore 612, 618
        }
    }
}
