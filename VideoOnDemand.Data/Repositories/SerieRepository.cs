using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using AppFramework.Expressions;
using System.Linq.Expressions;

namespace VideoOnDemand.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository(VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Serie serie, int[] actoresIds, int[] generosIds)
        {
            if (actoresIds != null)
            {
                // Consulto los topicos de mi BD a partir de los Ids
                var actores = from a in _context.Personas
                              where actoresIds.Contains(a.Id.Value)
                              select a;

                // Agrego los topics consultados a mi curso
                serie.Actores = new List<Persona>();
                foreach (var a in actores)
                    serie.Actores.Add(a);
            }

            if (generosIds != null)
            {
                // Consulto los topicos de mi BD a partir de los Ids
                var generos = from g in _context.Generos
                              where generosIds.Contains(g.GeneroId.Value)
                              select g;

                // Agrego los topics consultados a mi curso
                serie.Generos = new List<Genero>();
                foreach (var g in generos)
                    serie.Generos.Add(g);
            }

            if (generosIds == null && actoresIds == null)
            {
                base.Insert(serie);
            }

            _context.Series.Add(serie);
        }

        public void UpdateComplete(Serie serie, int[] actoresSeleccionados, int[] generosSeleccionados)
        {
            // Localizar la entidad en el contexto y poder modificar la del contexto.
            _context.Series.Attach(serie);

            // Avisar que la entidad esta siendo modificada.
            _context.Entry(serie).State = System.Data.Entity.EntityState.Modified;

            // Instrucción para recargar una colección de mi entidad.
            _context.Entry(serie).Collection(s => s.Actores).Load();
            _context.Entry(serie).Collection(s => s.Generos).Load();

            // Limpiar la lista
            serie.Actores.Clear();
            serie.Generos.Clear();
            if (actoresSeleccionados != null)
            {
                // Vuelve a crear las relaciones con los topics.
                var actores = from a in _context.Personas
                              where actoresSeleccionados.Contains((int)a.Id)
                              select a;
                serie.Actores = new List<Persona>();
                foreach (var a in actores)
                    serie.Actores.Add(a);
            }

            if (generosSeleccionados != null)
            {
                // Vuelve a crear las relaciones con los topics.
                var generos = from g in _context.Generos
                              where generosSeleccionados.Contains((int)g.GeneroId)
                              select g;
                serie.Generos = new List<Genero>();
                foreach (var g in generos)
                    serie.Generos.Add(g);
            }

        }


        public ICollection<Serie> QueryPageByNombreAndGeneroIncluding(string nombre, string genero, Expression<Func<Serie, object>>[] includes, out int totalPages, out int totalRows, string order, int page = 0, int pageSize = 10)
        {
            Expression<Func<Serie, bool>> where = s => true;
            where = where.And(s=> s.EstadosMedia == EEstatusMedia.VISIBLE);

            if (!String.IsNullOrEmpty(genero))
                where = where.And( s => s.Generos.Select( g => g.Nombre).Contains(genero));
            if (!String.IsNullOrEmpty(nombre))
                where = where.And( s => s.Nombre.Contains(nombre));

            int paginas;
            int filas;
         
            ICollection<Serie> series = QueryPageIncluding(where, includes, out paginas, out filas, order, page, pageSize);

            totalPages = paginas;
            totalRows = filas;

            return series;
        }

    }

}
