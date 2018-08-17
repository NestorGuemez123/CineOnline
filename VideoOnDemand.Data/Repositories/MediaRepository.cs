using AppFramework.Expressions;
using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MediaRepository : BaseRepository<Media>
    {
        public MediaRepository(VideoOnDemandContext context) : base(context)
        {

        }
        public ICollection<Media> QueryPageByNombreAndGeneroIncluding(string nombre, string genero, Expression<Func<Media, object>>[] includes, out int totalPages, out int totalRows, string order, int page = 0, int pageSize = 10)
        {
            Expression<Func<Media, bool>> where = s => true;
            where = where.And(s => s.EstadosMedia == EEstatusMedia.VISIBLE);

            if (!String.IsNullOrEmpty(genero))
                where = where.And(s => s.Generos.Select(g => g.Nombre).Contains(genero));
            if (!String.IsNullOrEmpty(nombre))
                where = where.And(s => s.Nombre.Contains(nombre));

            int paginas;
            int filas;

            ICollection<Media> movies = QueryPageIncluding(where, includes, out paginas, out filas, order, page, pageSize);

            totalPages = paginas;
            totalRows = filas;

            return movies;
        }
    }
}
