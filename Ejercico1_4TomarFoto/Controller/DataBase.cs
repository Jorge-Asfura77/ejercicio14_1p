using Ejercico1_4TomarFoto.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ejercico1_4TomarFoto.Controller
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbase;
        public DataBase(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);

            dbase.CreateTableAsync<Models.Foto>();
        }

        public Task<int> SavePhoto(Foto foto)
        {
            if (foto.id != 0)
            {
                return dbase.UpdateAsync(foto);
            }
            else
            {
                return dbase.InsertAsync(foto);
            }

        }

        public Task<List<Foto>> getListFoto()
        {
            return dbase.Table<Foto>().ToListAsync();
        }

    }
}
