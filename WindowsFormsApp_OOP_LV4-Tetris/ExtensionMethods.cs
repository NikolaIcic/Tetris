using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    public static class ExtensionMethods
    {
        public static bool Empty(this string str)
        {
            if (str == "" || str == null)
                return true;
            else
                return false;
        }
    }
}
