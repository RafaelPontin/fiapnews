﻿// <auto-generated />
using System;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestrutura.Migrations
{
    [DbContext(typeof(FiapNewsContext))]
    [Migration("20231019143029_inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutorNoticia", b =>
                {
                    b.Property<Guid>("AutoresId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NoticiasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AutoresId", "NoticiasId");

                    b.HasIndex("NoticiasId");

                    b.ToTable("AutorNoticia");
                });

            modelBuilder.Entity("AutorRedeSocial", b =>
                {
                    b.Property<Guid>("AutoresId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RedesSociaisId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AutoresId", "RedesSociaisId");

                    b.HasIndex("RedesSociaisId");

                    b.ToTable("AutorRedeSocial");
                });

            modelBuilder.Entity("CategoriaNoticia", b =>
                {
                    b.Property<Guid>("CategoriasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NoticiasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriasId", "NoticiasId");

                    b.HasIndex("NoticiasId");

                    b.ToTable("CategoriaNoticia");
                });

            modelBuilder.Entity("Dominio.Entidades.Assinatura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Preco")
                        .HasPrecision(18, 2)
                        .HasColumnType("float(18)");

                    b.Property<int>("TipoAssinatura")
                        .HasColumnType("int");

                    b.Property<int>("TipoPlano")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Assinatura", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Comentario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssinanteId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataValidacao")
                        .HasColumnType("datetime");

                    b.Property<int>("EstadoValidacao")
                        .HasColumnType("int");

                    b.Property<Guid?>("ModeradorResponsavelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MotivoRejeicao")
                        .HasColumnType("varchar(500)");

                    b.Property<Guid?>("NoticiaId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("AssinanteId");

                    b.HasIndex("ModeradorResponsavelId");

                    b.HasIndex("NoticiaId");

                    b.ToTable("Comentario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Noticia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativa")
                        .HasColumnType("bit");

                    b.Property<string>("Conteudo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ExclusivoParaAssinantes")
                        .HasColumnType("bit");

                    b.Property<string>("Lead")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regiao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubTitulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Noticia", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Dominio.ObjetosDeValor.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Dominio.ObjetosDeValor.RedeSocial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RedesSociais", (string)null);
                });

            modelBuilder.Entity("Dominio.ObjetosDeValor.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("NoticiaNoticia", b =>
                {
                    b.Property<Guid>("NoticiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NoticiasRelacionadasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NoticiaId", "NoticiasRelacionadasId");

                    b.HasIndex("NoticiasRelacionadasId");

                    b.ToTable("NoticiaNoticia");
                });

            modelBuilder.Entity("NoticiaTag", b =>
                {
                    b.Property<Guid>("NoticiasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NoticiasId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("NoticiaTag");
                });

            modelBuilder.Entity("Dominio.Entidades.Administrador", b =>
                {
                    b.HasBaseType("Dominio.Entidades.Usuario");

                    b.ToTable("Administrador", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Assinante", b =>
                {
                    b.HasBaseType("Dominio.Entidades.Usuario");

                    b.Property<Guid?>("AssinaturaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("AssinaturaId");

                    b.ToTable("Assinante", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Autor", b =>
                {
                    b.HasBaseType("Dominio.Entidades.Usuario");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Autor", (string)null);
                });

            modelBuilder.Entity("AutorNoticia", b =>
                {
                    b.HasOne("Dominio.Entidades.Autor", null)
                        .WithMany()
                        .HasForeignKey("AutoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Noticia", null)
                        .WithMany()
                        .HasForeignKey("NoticiasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AutorRedeSocial", b =>
                {
                    b.HasOne("Dominio.Entidades.Autor", null)
                        .WithMany()
                        .HasForeignKey("AutoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.ObjetosDeValor.RedeSocial", null)
                        .WithMany()
                        .HasForeignKey("RedesSociaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoriaNoticia", b =>
                {
                    b.HasOne("Dominio.ObjetosDeValor.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Noticia", null)
                        .WithMany()
                        .HasForeignKey("NoticiasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.Comentario", b =>
                {
                    b.HasOne("Dominio.Entidades.Assinante", "Assinante")
                        .WithMany("Comentarios")
                        .HasForeignKey("AssinanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Administrador", "ModeradorResponsavel")
                        .WithMany()
                        .HasForeignKey("ModeradorResponsavelId");

                    b.HasOne("Dominio.Entidades.Noticia", "Noticia")
                        .WithMany("Comentarios")
                        .HasForeignKey("NoticiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assinante");

                    b.Navigation("ModeradorResponsavel");

                    b.Navigation("Noticia");
                });

            modelBuilder.Entity("Dominio.Entidades.Usuario", b =>
                {
                    b.OwnsOne("Dominio.ObjetosDeValor.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("EnderecoEmail")
                                .IsRequired()
                                .HasColumnType("varchar(max)")
                                .HasColumnName("Email");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Email");
                });

            modelBuilder.Entity("NoticiaNoticia", b =>
                {
                    b.HasOne("Dominio.Entidades.Noticia", null)
                        .WithMany()
                        .HasForeignKey("NoticiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Noticia", null)
                        .WithMany()
                        .HasForeignKey("NoticiasRelacionadasId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NoticiaTag", b =>
                {
                    b.HasOne("Dominio.Entidades.Noticia", null)
                        .WithMany()
                        .HasForeignKey("NoticiasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.ObjetosDeValor.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.Administrador", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario", null)
                        .WithOne()
                        .HasForeignKey("Dominio.Entidades.Administrador", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.Assinante", b =>
                {
                    b.HasOne("Dominio.Entidades.Assinatura", "Assinatura")
                        .WithMany()
                        .HasForeignKey("AssinaturaId");

                    b.HasOne("Dominio.Entidades.Usuario", null)
                        .WithOne()
                        .HasForeignKey("Dominio.Entidades.Assinante", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assinatura");
                });

            modelBuilder.Entity("Dominio.Entidades.Autor", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario", null)
                        .WithOne()
                        .HasForeignKey("Dominio.Entidades.Autor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.Noticia", b =>
                {
                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("Dominio.Entidades.Assinante", b =>
                {
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
