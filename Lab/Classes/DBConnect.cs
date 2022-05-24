using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Classes
{
    class DBConnect
    {
        private static Models.LabEntities Connection;
        public static Models.LabEntities GetContext()
        {
            if (Connection == null)
            {
                Connection = new Models.LabEntities();
            }
            return Connection;
        }
    }
}
