using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTek86.dal
{
    public class ResponsableAccess
    {
        private readonly Access access = null;

        public ResponsableAccess()
        {
            access = Access.GetInstance();
        }

        public Boolean AuthIsValid(string login, string password)
        {
            if (access == null) return false;

            string req = "SELECT login, pwd FROM responsable WHERE login=@login AND pwd=SHA2(@pwd, 256);";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "@login", login},
                { "@pwd", password }
            };

            try
            {
                List<Dictionary<string, object>> records = access.Manager.ReqSelect(req, parameters);

                return (records.Count > 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Log.Error("DeveloppeurAccess.ControleAuthentification catch req={0} erreur={1}", req, e.Message);
                Environment.Exit(0);
            }

            return false;
        }
    }
}
