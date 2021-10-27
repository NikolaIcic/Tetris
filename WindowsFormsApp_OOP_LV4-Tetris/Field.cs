using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    class Field
    {
        #region atributs
        private int _x;
        private int _y;
        private Brush _Color;
        #endregion

        #region properties
        public int x
        {
            get { return _x; }
            set { _x = value; }
        }
        public int y
        {
            get { return _y; }
            set { _y = value; }
        }
        public Brush Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        #endregion

        #region methods
        public Field()
        {
            Color = Brushes.Red;
        }
        public Field(int xCord, int yCord,Brush brush)
        {
            x = xCord;
            y = yCord;
            Color = brush;
        }
        
        #endregion
    }
}
