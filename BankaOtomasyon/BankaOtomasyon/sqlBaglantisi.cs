using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaOtomasyon
{
    internal class sqlBaglantisi
    {
        public OleDbConnection baglanti()
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\huseyinkaradana\source\repos\BankaOtomasyon\AlperBank.accdb");
            conn.Open();
            return conn;
        }
    }
}
