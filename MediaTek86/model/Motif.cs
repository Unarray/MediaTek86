using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTek86.model
{
    public class Motif
    {
        public Motif(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }

        public int id { get; }
        public string libelle { get; set; }
    }
}
