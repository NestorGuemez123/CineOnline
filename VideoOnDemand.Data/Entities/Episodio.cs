using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Episodio : Media
    {
        public int? Temporada { get; set; }

        #region Relaciones
        public int? SerieId { get; set; }
        public Serie Serie { get; set; }
        #endregion
    }
}
