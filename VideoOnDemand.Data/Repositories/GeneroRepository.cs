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
    public class GeneroRepository : BaseRepository<Genero>
    {
        public GeneroRepository(VideoOnDemandContext context) : base(context)
        {

        }

        public void GenerosActivos()
        {
            var generosActivos = from ga in _context.Generos
                                 where ga.Activo == true
                                 select ga;
        }
    }
}
