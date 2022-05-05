using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Model
{
    public partial class WarehouseUnit
    {


        public decimal TotalCost
        {

            get
            {
                return Product.DefaultPrice * Amount;


            }



        }


    }
}
