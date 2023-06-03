using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTek86.model
{
    class Service
    {

        public Service(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public int id { get; }
        public string nom { get; set; }
    }
}
