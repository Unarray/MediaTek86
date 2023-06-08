using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTek86.model;

namespace MediaTek86.dal
{
    public class MotifAccess
    {
        private readonly Access access = null;

        public MotifAccess()
        {
            access = Access.GetInstance();
        }

        public List<Motif> GetMotifs()
        {
            List<Motif> motifs = new List<Motif>();

            if (access == null) return motifs;

            string req = "SELECT IDMOTIF, LIBELLE FROM motif;";

            try
            {
                List<Dictionary<string, object>> records = access.Manager.ReqSelect(req);

                foreach (Dictionary<string, object> row in records)
                {

                    motifs.Add(new Motif(
                        (int)row["IDMOTIF"],
                        (string)row["LIBELLE"]
                    ));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            return motifs;
        }
    }
}
