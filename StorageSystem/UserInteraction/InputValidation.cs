using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageSystem.UserInteraction
{
    public static class InputValidation
    {


        public static bool IsSpaceKey(Key key)
        {

            if (key == Key.Space)
                return true;
            else
                return false;

        }


        public static bool IsDigitKey(Key key)
        {

            var keyChar = (char)KeyInterop.VirtualKeyFromKey(key);

            if (char.IsDigit(keyChar))
                return true;
            else
                return false;


        }

        public static bool ValidatePrice(Key key, string text)
        {

            if (IsSpaceKey(key))
            {                
                return false;
            }

            if (!IsDigitKey(key) && key != Key.Back && key != Key.OemComma)
            {                
                return false;
            }

            if (key == Key.OemComma)
            {

                if (text.Length == 0 || text.Contains(","))
                {                    
                    return false;
                }

            }

            return true;

        }

    }
}
