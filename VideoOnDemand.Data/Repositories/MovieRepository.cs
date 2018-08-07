using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MovieRepository : BaseRepository<Movie>
    {
        public MovieRepository(VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Movie movie, int[] generosId, int[] actoresIds)
        {
            if (actoresIds != null)
            {
                // Consulto los topicos de mi BD a partir de los Ids
                var actores = from a in _context.Personas
                              where actoresIds.Contains(a.Id.Value)
                              select a;

                // Agrego los topics consultados a mi curso
                movie.Actores = new List<Persona>();
                foreach (var a in actores)
                    movie.Actores.Add(a);
            }

            if (generosId != null)
            {
                //Consulto los topics de mi BD a partir de los IDS
                var generos = from t in _context.Generos
                              where generosId.Contains(t.GeneroId.Value)
                              select t;

                //Agrego los topics consultados a mi curso
                movie.Generos = new List<Genero>();
                foreach (var t in generos)
                     movie.Generos.Add(t);
                }
            if (generosId == null && actoresIds == null)
            {
                base.Insert(movie);
            }
            //Agregar al contexto el curso
            _context.Medias.Add(movie);
            }

        public void UpdateComplete(Movie movie, int[] actoresSeleccionados, int[] generosSeleccionados)
        {
            // Localizar la entidad en el contexto y poder modificar la del contexto.
            _context.Movies.Attach(movie);

            // Avisar que la entidad esta siendo modificada.
            _context.Entry(movie).State = System.Data.Entity.EntityState.Modified;

            // Instrucción para recargar una colección de mi entidad.
            _context.Entry(movie).Collection(s => s.Actores).Load();
            _context.Entry(movie).Collection(s => s.Generos).Load();

            // Limpiar la lista
            movie.Actores.Clear();
            movie.Generos.Clear();
            if (actoresSeleccionados != null)
            {
                // Vuelve a crear las relaciones con los topics.
                var actores = from a in _context.Personas
                              where actoresSeleccionados.Contains((int)a.Id)
                              select a;
                movie.Actores = new List<Persona>();
                foreach (var a in actores)
                    movie.Actores.Add(a);
            }

            if (generosSeleccionados != null)
            {
                // Vuelve a crear las relaciones con los topics.
                var generos = from g in _context.Generos
                              where generosSeleccionados.Contains((int)g.GeneroId)
                              select g;
                movie.Generos = new List<Genero>();
                foreach (var g in generos)
                    movie.Generos.Add(g);
            }

        }
    }
}

