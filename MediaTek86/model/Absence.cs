using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTek86.model
{
    class Absence
    {
        public Absence(Personnel personnel, DateTime dateDebut, DateTime dateFin, Motif motif) {
            this.personnel = personnel;
            this.dateDebut = dateDebut;
            this.dateFin = dateFin;
            this.motif = motif;
        }

        public Personnel personnel { get; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public Motif motif { get; set; }
    }
}
