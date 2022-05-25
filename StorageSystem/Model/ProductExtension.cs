using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Model
{
    public partial class Product
    {

        public string FirstImagePath
        {
            get
            {
                var imagepath = ProductImage.FirstOrDefault()?.ImagePath;

                if (imagepath is null)
                    return "/Image/productPlaceholder.png";

                var path = Directory.GetCurrentDirectory() + imagepath;

                return path;


            }

        }

        public string CategoryNames
        {

            get                      
            {
                return string.Join(",", ProductCategory.Select(pc => pc.Category.Name));                                  
            } 

        }


    }
}
