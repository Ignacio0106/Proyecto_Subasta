using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Subasta.Infraestructure.Models;

namespace Subasta.Infraestructure.Data;

public partial class SubastaContext : DbContext
{
    public SubastaContext(DbContextOptions<SubastaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Condicion> Condicion { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<EstadoPago> EstadoPago { get; set; }

    public virtual DbSet<EstadoSubasta> EstadoSubasta { get; set; }

    public virtual DbSet<ImagenObjeto> ImagenObjeto { get; set; }

    public virtual DbSet<Notificacion> Notificacion { get; set; }

    public virtual DbSet<Objeto> Objeto { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Puja> Puja { get; set; }

    public virtual DbSet<ResultadoSubasta> ResultadoSubasta { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Subasta.Infraestructure.Models.Subasta> Subasta { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A109A6C2FB8");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Condicion>(entity =>
        {
            entity.HasKey(e => e.IdCondicion).HasName("PK__Condicio__7BCB6514EB516C39");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1FC4BC730");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoPago>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPago).HasName("PK__EstadoPa__540F03E96E8ABB78");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoSubasta>(entity =>
        {
            entity.HasKey(e => e.IdEstadoSubasta).HasName("PK__EstadoSu__4F0A9413315E8639");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImagenObjeto>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__ImagenOb__B42D8F2A627FDB2E");

            entity.HasOne(d => d.IdObjetoNavigation).WithMany(p => p.ImagenObjeto)
                .HasForeignKey(d => d.IdObjeto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenObj__IdObj__52593CB8");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__Notifica__F6CA0A85E7AA8DCA");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notificacion)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__IdUsu__6A30C649");
        });

        modelBuilder.Entity<Objeto>(entity =>
        {
            entity.HasKey(e => e.IdObjeto).HasName("PK__Objeto__38FB77BF14DC9DEA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCondicionNavigation).WithMany(p => p.Objeto)
                .HasForeignKey(d => d.IdCondicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Objeto__IdCondic__4BAC3F29");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Objeto)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Objeto__IdEstado__4AB81AF0");

            entity.HasOne(d => d.IdUsuarioVendedorNavigation).WithMany(p => p.Objeto)
                .HasForeignKey(d => d.IdUsuarioVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Objeto__IdUsuari__49C3F6B7");

            entity.HasMany(d => d.IdCategoria).WithMany(p => p.IdObjeto)
                .UsingEntity<Dictionary<string, object>>(
                    "ObjetoCategoria",
                    r => r.HasOne<Categoria>().WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ObjetoCat__IdCat__4F7CD00D"),
                    l => l.HasOne<Objeto>().WithMany()
                        .HasForeignKey("IdObjeto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ObjetoCat__IdObj__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("IdObjeto", "IdCategoria").HasName("PK__ObjetoCa__22C7751EAD6C4478");
                    });
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__FC851A3A73A97409");

            entity.HasIndex(e => e.IdSubasta, "UQ__Pago__AA418F725D7B3BD6").IsUnique();

            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEstadoPagoNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdEstadoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__IdEstadoPa__6477ECF3");

            entity.HasOne(d => d.IdSubastaNavigation).WithOne(p => p.Pago)
                .HasForeignKey<Pago>(d => d.IdSubasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__IdSubasta__656C112C");
        });

        modelBuilder.Entity<Puja>(entity =>
        {
            entity.HasKey(e => e.IdPuja).HasName("PK__Puja__F986B2D452DC1DC1");

            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoOfertado).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdSubastaNavigation).WithMany(p => p.Puja)
                .HasForeignKey(d => d.IdSubasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Puja__IdSubasta__5BE2A6F2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Puja)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Puja__IdUsuario__5AEE82B9");
        });

        modelBuilder.Entity<ResultadoSubasta>(entity =>
        {
            entity.HasKey(e => e.IdResultado).HasName("PK__Resultad__DAF71D0B2A1E246B");

            entity.HasIndex(e => e.IdSubasta, "UQ__Resultad__AA418F724AC21F6B").IsUnique();

            entity.Property(e => e.FechaCierre).HasColumnType("datetime");
            entity.Property(e => e.MontoFinal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdSubastaNavigation).WithOne(p => p.ResultadoSubasta)
                .HasForeignKey<ResultadoSubasta>(d => d.IdSubasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__IdSub__5FB337D6");

            entity.HasOne(d => d.IdUsuarioGanadorNavigation).WithMany(p => p.ResultadoSubasta)
                .HasForeignKey(d => d.IdUsuarioGanador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__IdUsu__60A75C0F");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C601769D8");

            entity.Property(e => e.NombreRol)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subasta.Infraestructure.Models.Subasta>(entity =>
        {
            entity.HasKey(e => e.IdSubasta).HasName("PK__Subasta__AA418F73851CAD69");

            entity.Property(e => e.FechaHoraCierre).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraInicio).HasColumnType("datetime");
            entity.Property(e => e.IncrementoMinimo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioBase).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEstadoSubastaNavigation).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.IdEstadoSubasta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subasta__IdEstad__5535A963");

            entity.HasOne(d => d.IdObjetoNavigation).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.IdObjeto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subasta__IdObjet__571DF1D5");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.Subasta)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subasta__IdUsuar__5629CD9C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97408FC600");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuario__531402F3002BECF1").IsUnique();

            entity.Property(e => e.Contrasenna)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdEstad__45F365D3");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
