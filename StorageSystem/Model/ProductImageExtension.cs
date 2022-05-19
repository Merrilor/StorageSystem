using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Model
{
    public partial class ProductImage
    {

        public string FullImagePath
        {

            get
            {
                return Directory.GetCurrentDirectory() + ImagePath;

            }

        }


        public string ImageName
        {

            get
            {
                return ImagePath.Split('/').Last();


            }

        }


    }
}
