using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week10_1_Singleton
{
    class DatabaseConnection
    {
        private static readonly DatabaseConnection _instance = new DatabaseConnection();

        private DatabaseConnection()
        {
        }

        public static DatabaseConnection GetInstance()
        {
            return _instance;
        }
    }
}
