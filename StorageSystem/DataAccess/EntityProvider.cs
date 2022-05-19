using StorageSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess
{
    public static class EntityProvider
    {


        public static StorageEntities CreateEntities()
        {

            return new StorageEntities();


        }



    }
}
