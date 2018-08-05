using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Data
{
    public class VideoOnDemandContext : DbContext
    {
        public VideoOnDemandContext() : base("name=VideoOnDemandContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Episodio> Episodios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var usuarioEntity = modelBuilder.Entity<Usuario>();
            usuarioEntity.HasKey(x => x.Id);
            usuarioEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            usuarioEntity.Property(x => x.Nombre).HasMaxLength(200).IsRequired();
            usuarioEntity.Property(x => x.IdentityId).HasMaxLength(128).IsRequired();

		 #region MapeoGenero
            var genero = modelBuilder.Entity<Genero>();
            genero.HasKey(i => i.GeneroId);
            genero.Property(i => i.GeneroId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            genero.Property(i => i.Nombre).HasMaxLength(100).IsRequired();
            genero.Property(i => i.Descripcion).HasMaxLength(500).IsOptional();

            #endregion

            #region MapeoMedia
            var media = modelBuilder.Entity<Media>();
            media.HasKey(i => i.MediaId);
            media.Property(i => i.MediaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            media.Property(i => i.Descripcion).HasMaxLength(500).IsOptional();

            media.HasMany<Genero>(g => g.Generos).WithMany(m => m.Medias).Map(gm =>
            {
                gm.MapLeftKey("MediaId");
                gm.MapRightKey("GeneroId");
                gm.ToTable("Media-Genero");
            });


            media.HasMany<Persona>(a => a.Actores).WithMany(m => m.Medias).Map(am =>
            {
                am.MapLeftKey("MediaId");
                am.MapRightKey("ActoresId");
                am.ToTable("Media-Actor");
            });
            #endregion

            #region MapeoOpinion
            var opinion = modelBuilder.Entity<Opinion>();
            opinion.HasKey(o => o.Id);
            opinion.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            opinion.Property(o => o.Puntuacion).IsOptional();
            opinion.Property(o => o.Descripcion).IsOptional();
            #endregion

            #region MapeoFavorito
            var favorito = modelBuilder.Entity<Favorito>();
            favorito.HasKey(m => m.id);
            favorito.Property(m => m.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            favorito.Property(m => m.FechaAgregado).IsRequired();
            favorito.Property(m => m.mediaId).IsRequired();
            favorito.HasRequired(m => m.media).WithMany().HasForeignKey(m => m.mediaId);
            favorito.Property(m => m.usuarioId).IsRequired();
            favorito.HasRequired(m => m.usuario).WithMany().HasForeignKey(m => m.usuarioId);
            #endregion

            #region MapeoSerie
            //La relacion 1-n con Episodio se mapeo en el mapeo de Episodio
            var serie = modelBuilder.Entity<Serie>();
            serie.HasKey(s => s.MediaId);
            serie.ToTable("Series"); //Mapea la herencia desde Media, crea una tabla sola para Series
            #endregion

            #region MapeoEpisodio
            //La relacion 1-n con Serie no se mapea aqui, en cambio se deja a EF hacerlo por las convenciones mediante el atributo
            //Serie dentro de Episodio
            var episodio = modelBuilder.Entity<Episodio>();
            episodio.HasKey(e => e.MediaId);
            episodio.HasRequired(e => e.Serie)
                .WithMany()
                .HasForeignKey(e => e.SerieId);

            episodio.ToTable("Episodios"); //Mapea la herencia desde Media, crea una tabla sola para Episodios

            #endregion

            #region MapeoMediaOnPlay
            var mediaOnPlay = modelBuilder.Entity<MediaOnPlay>();
            mediaOnPlay.HasKey(m => m.Id);
            mediaOnPlay.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mediaOnPlay.Property(m => m.Milisegundo).IsOptional();

            //Uno a muchos con Media
            mediaOnPlay.HasRequired(m => m.Media).WithMany().HasForeignKey(m => m.MediaId);
            //Uno a muchos con Usuario
            mediaOnPlay.HasRequired(m => m.Usuario).WithMany().HasForeignKey(m => m.UsuarioId);
            #endregion
            
        }
    }
}
