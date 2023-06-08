using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.dal;
using MediaTek86.model;

namespace MediaTek86.controller
{
    public class MotifController
    {
        private readonly MotifAccess motifAccess;

        public MotifController()
        {
            motifAccess = new MotifAccess();
        }

        public List<Motif> GetMotifs()
        {
            return motifAccess.GetMotifs();
        }
    }
}
