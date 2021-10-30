using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SIPI_web.Models
{
    public partial class SIPI_dbContext : DbContext
    {
        public SIPI_dbContext()
        {
        }

        public SIPI_dbContext(DbContextOptions<SIPI_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<tbl_carrera> tbl_carreras { get; set; }
        public virtual DbSet<tbl_ciudad> tbl_ciudads { get; set; }
        public virtual DbSet<tbl_consultor> tbl_consultors { get; set; }
        public virtual DbSet<tbl_empresa> tbl_empresas { get; set; }
        public virtual DbSet<tbl_equipo> tbl_equipos { get; set; }
        public virtual DbSet<tbl_estado> tbl_estados { get; set; }
        public virtual DbSet<tbl_estudiante> tbl_estudiantes { get; set; }
        public virtual DbSet<tbl_estudianteCarrera> tbl_estudianteCarreras { get; set; }
        public virtual DbSet<tbl_estudianteEstatus> tbl_estudianteEstatuses { get; set; }
        public virtual DbSet<tbl_informeAcademicoEstatus> tbl_informeAcademicoEstatuses { get; set; }
        public virtual DbSet<tbl_inscrito> tbl_inscritos { get; set; }
        public virtual DbSet<tbl_integrante> tbl_integrantes { get; set; }
        public virtual DbSet<tbl_metodologiaEstatus> tbl_metodologiaEstatuses { get; set; }
        public virtual DbSet<tbl_pai> tbl_pais { get; set; }
        public virtual DbSet<tbl_palabraClaveTrabajo> tbl_palabraClaveTrabajos { get; set; }
        public virtual DbSet<tbl_palabrasClave> tbl_palabrasClaves { get; set; }
        public virtual DbSet<tbl_pasantiaEstatus> tbl_pasantiaEstatuses { get; set; }
        public virtual DbSet<tbl_persona> tbl_personas { get; set; }
        public virtual DbSet<tbl_rolesTeg> tbl_rolesTegs { get; set; }
        public virtual DbSet<tbl_sede> tbl_sedes { get; set; }
        public virtual DbSet<tbl_teg> tbl_tegs { get; set; }
        public virtual DbSet<tbl_tipoTrabajo> tbl_tipoTrabajos { get; set; }
        public virtual DbSet<tbl_trabajo> tbl_trabajos { get; set; }
        public virtual DbSet<tbl_usuario> tbl_usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUserRoles_tbl_usuario");
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<tbl_carrera>(entity =>
            {
                entity.HasKey(e => e.id_carrera)
                    .HasName("PK_tbl_adminCarrera");
            });

            modelBuilder.Entity<tbl_ciudad>(entity =>
            {
                entity.HasOne(d => d.id_estadoNavigation)
                    .WithMany(p => p.tbl_ciudads)
                    .HasForeignKey(d => d.id_estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estado tiene ciudades");
            });

            modelBuilder.Entity<tbl_consultor>(entity =>
            {
                entity.HasOne(d => d.id_consultorNavigation)
                    .WithOne(p => p.tbl_consultor)
                    .HasForeignKey<tbl_consultor>(d => d.id_consultor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un consultor es una persona");
            });

            modelBuilder.Entity<tbl_empresa>(entity =>
            {
                entity.HasOne(d => d.id_empresaNavigation)
                    .WithOne(p => p.tbl_empresa)
                    .HasForeignKey<tbl_empresa>(d => d.id_empresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Una empresa es un usuario");
            });

            modelBuilder.Entity<tbl_estado>(entity =>
            {
                entity.Property(e => e.id_estado).ValueGeneratedNever();

                entity.HasOne(d => d.id_paisNavigation)
                    .WithMany(p => p.tbl_estados)
                    .HasForeignKey(d => d.id_pais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un pais tiene estados");
            });

            modelBuilder.Entity<tbl_estudiante>(entity =>
            {
                entity.HasOne(d => d.id_equipoNavigation)
                    .WithMany(p => p.tbl_estudiantes)
                    .HasForeignKey(d => d.id_equipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante perteence a un equipo");

                entity.HasOne(d => d.id_estudianteNavigation)
                    .WithOne(p => p.tbl_estudiante)
                    .HasForeignKey<tbl_estudiante>(d => d.id_estudiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante es una persona");

                entity.HasOne(d => d.id_estudianteEstatusNavigation)
                    .WithMany(p => p.tbl_estudiantes)
                    .HasForeignKey(d => d.id_estudianteEstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante tiene un estatus");

                entity.HasOne(d => d.id_informeAcademicoEstatusNavigation)
                    .WithMany(p => p.tbl_estudiantes)
                    .HasForeignKey(d => d.id_informeAcademicoEstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante tiene un status en el Informe Academico");

                entity.HasOne(d => d.id_metodologiaEstatusNavigation)
                    .WithMany(p => p.tbl_estudiantes)
                    .HasForeignKey(d => d.id_metodologiaEstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante tiene un status en Metodologia");

                entity.HasOne(d => d.id_pasantiaEstatusNavigation)
                    .WithMany(p => p.tbl_estudiantes)
                    .HasForeignKey(d => d.id_pasantiaEstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante tiene un status en su pasantia");

                entity.HasOne(d => d.id_sedeNavigation)
                    .WithMany(p => p.tbl_estudiantes)
                    .HasForeignKey(d => d.id_sede)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante va a una sede");
            });

            modelBuilder.Entity<tbl_estudianteCarrera>(entity =>
            {
                entity.HasOne(d => d.id_carreraNavigation)
                    .WithMany(p => p.tbl_estudianteCarreras)
                    .HasForeignKey(d => d.id_carrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mas de una carrera puede ser cursada por un estudiante");

                entity.HasOne(d => d.id_estudianteNavigation)
                    .WithMany(p => p.tbl_estudianteCarreras)
                    .HasForeignKey(d => d.id_estudiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un estudiante cursa una o mas carreras");
            });

            modelBuilder.Entity<tbl_inscrito>(entity =>
            {
                entity.Property(e => e.id_inscrito).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<tbl_integrante>(entity =>
            {
                entity.HasKey(e => e.id_integrantes)
                    .HasName("PK_tbl_tegista_1");

                entity.HasOne(d => d.id_estudianteNavigation)
                    .WithMany(p => p.tbl_integrantes)
                    .HasForeignKey(d => d.id_estudiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un TEGISTA es un estudiante");

                entity.HasOne(d => d.id_trabajoNavigation)
                    .WithMany(p => p.tbl_integrantes)
                    .HasForeignKey(d => d.id_trabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Uno mas mas TEGISTAS realizan una TEG");
            });

            modelBuilder.Entity<tbl_palabraClaveTrabajo>(entity =>
            {
                entity.HasOne(d => d.id_palabraClaveNavigation)
                    .WithMany(p => p.tbl_palabraClaveTrabajos)
                    .HasForeignKey(d => d.id_palabraClave)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Hay palabras clave en un trabajo");

                entity.HasOne(d => d.id_trabajoNavigation)
                    .WithMany(p => p.tbl_palabraClaveTrabajos)
                    .HasForeignKey(d => d.id_trabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un trabajo tiene palabras claves");
            });

            modelBuilder.Entity<tbl_palabrasClave>(entity =>
            {
                entity.Property(e => e.id_palabraClave).ValueGeneratedNever();
            });

            modelBuilder.Entity<tbl_persona>(entity =>
            {
                entity.HasOne(d => d.id_personaNavigation)
                    .WithOne(p => p.tbl_persona)
                    .HasForeignKey<tbl_persona>(d => d.id_persona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Una persona es un usuario");
            });

            modelBuilder.Entity<tbl_teg>(entity =>
            {
                entity.HasKey(e => e.id_teg)
                    .HasName("PK_tbl_teg_1");

                entity.Property(e => e.id_teg).ValueGeneratedNever();

                entity.HasOne(d => d.id_consultorAcademicoNavigation)
                    .WithMany(p => p.tbl_tegid_consultorAcademicoNavigations)
                    .HasForeignKey(d => d.id_consultorAcademico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Una TEG tiene un consultor Academico");

                entity.HasOne(d => d.id_consultorMetodologiaNavigation)
                    .WithMany(p => p.tbl_tegid_consultorMetodologiaNavigations)
                    .HasForeignKey(d => d.id_consultorMetodologia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Una TEG tiene un conultor Metodologico");

                entity.HasOne(d => d.id_tegNavigation)
                    .WithOne(p => p.tbl_teg)
                    .HasForeignKey<tbl_teg>(d => d.id_teg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un TEG es un trabajo de Investigacion");
            });

            modelBuilder.Entity<tbl_trabajo>(entity =>
            {
                entity.HasKey(e => e.id_trabajo)
                    .HasName("PK_tbl_teg");

                entity.HasOne(d => d.id_tipoTrabajoNavigation)
                    .WithMany(p => p.tbl_trabajos)
                    .HasForeignKey(d => d.id_tipoTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un Trabajo es de un Tipo");
            });

            modelBuilder.Entity<tbl_usuario>(entity =>
            {
                entity.HasOne(d => d.id_usuarioNavigation)
                    .WithOne(p => p.tbl_usuario)
                    .HasForeignKey<tbl_usuario>(d => d.id_usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un usuario es miembro del Portal Portal");

                entity.HasOne(d => d.usuario_ciudadNacimientoNavigation)
                    .WithMany(p => p.tbl_usuariousuario_ciudadNacimientoNavigations)
                    .HasForeignKey(d => d.usuario_ciudadNacimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Un Usuario nacio en una Ciudad");

                entity.HasOne(d => d.usuario_ciudadUbicacionNavigation)
                    .WithMany(p => p.tbl_usuariousuario_ciudadUbicacionNavigations)
                    .HasForeignKey(d => d.usuario_ciudadUbicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("[Un usuario se encuentra en una Ciudad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
