using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Model
{
    public partial class User
    {

        public string FullPhotoPath
        {

            get
            {

                if(Photo is null)
                {
                    return "/Image/placeholderUser.jpg";
                }

                return Directory.GetCurrentDirectory() + Photo;

            }


        }



    }
}
