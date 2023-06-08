using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTek86.model
{
    public class Personnel
    {
        public Personnel(int id, Service service, string nom, string prenom, string tel, string mail)
        {
            this.id = id;
            this.service = service;
            this.nom = nom;
            this.prenom = prenom;
            this.tel = tel;
            this.mail = mail;
        }

        public int id { get;  }
        public Service service { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
    }
}
