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
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository(VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Serie serie, int[] actoresIds)
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

            else
            {
                base.Insert(serie);
            }

            _context.Series.Add(serie);
        }

        public void UpdateComplete(Serie serie, int[] actoresSeleccionados)
        {
            // Localizar la entidad en el contexto y poder modificar la del contexto.
            _context.Series.Attach(serie);

            // Avisar que la entidad esta siendo modificada.
            _context.Entry(serie).State = System.Data.Entity.EntityState.Modified;

            // Instrucción para recargar una colección de mi entidad.
            _context.Entry(serie).Collection(s => s.Actores).Load();

            // Limpiar la lista
            serie.Actores.Clear();
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
        }
    }
}
