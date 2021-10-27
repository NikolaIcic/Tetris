using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    class Object
    {
        #region atributs
        private int _x;
        private int _y;
        private int _State;
        private int _Rotate;
        private List<Field> _Fields = new List<Field>();
        #endregion

        #region properties
        public List<Field> Fields
        {
            get { return _Fields; }
            set { _Fields = value; }
        }
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
        public int State
        {
            get { return _State; }
            set { _State = value; }
            // 0 - Square
            // 1 - Line
            // 2 - Field
            // 3 - Half
            // 4 - Cross
            // 5 - Magnet
        }
        public int rotation
        {
            get { return _Rotate; }
            set { _Rotate = value; }
            // 0 -Facing down
            // 1 - Facing left
            // 2 - Facing up
            // 3 - Facing right
        }
        #endregion

        #region methods
        public Object()
        {
           
        }
        public Object(int xCord, int yCord, int state)
        {
            State = state;
            x = xCord;
            y = yCord;
            _Rotate = 0;
            switch (State)
            {
                case 0:
                    _Fields.Add(new Field(x,y,Brushes.Green));
                    _Fields.Add(new Field(x, y+1, Brushes.Green));
                    _Fields.Add(new Field(x - 1, y, Brushes.Green));
                    _Fields.Add(new Field(x - 1, y+1, Brushes.Green));
                    break;
                case 1:
                    _Fields.Add(new Field(x, y, Brushes.Blue));
                    _Fields.Add(new Field(x, y + 1, Brushes.Blue));
                    _Fields.Add(new Field(x, y + 2, Brushes.Blue));
                    break;
                case 2:
                    _Fields.Add(new Field(x, y, Brushes.Yellow));
                    break;
                case 4:
                    _Fields.Add(new Field(x, y, Brushes.Red));
                    _Fields.Add(new Field(x, y + 1, Brushes.Red));
                    _Fields.Add(new Field(x, y + 2, Brushes.Red));
                    _Fields.Add(new Field(x - 1, y + 1, Brushes.Red));
                    _Fields.Add(new Field(x + 1, y + 1, Brushes.Red));
                    break;
                case 3:
                    _Fields.Add(new Field(x, y, Brushes.Purple));
                    _Fields.Add(new Field(x, y + 1, Brushes.Purple));
                    _Fields.Add(new Field(x - 1, y + 1, Brushes.Purple));
                    _Fields.Add(new Field(x + 1, y + 1, Brushes.Purple));
                    break;
                case 5:
                    _Fields.Add(new Field(x, y + 1, Brushes.Orange));
                    _Fields.Add(new Field(x+1, y + 1, Brushes.Orange));
                    _Fields.Add(new Field(x - 1, y + 1, Brushes.Orange));
                    _Fields.Add(new Field(x + 2, y + 1, Brushes.Orange));
                    _Fields.Add(new Field(x - 1, y, Brushes.Orange));
                    _Fields.Add(new Field(x + 2, y, Brushes.Orange));
                    break;
            }
        }
        public void RemoveField(Field f)
        {
            _Fields.Remove(f);
        }
        public bool MoveDown()
        {
            bool posible = true;
            foreach (Field f in _Fields)
            {
                if (f.y + 1 == Settings.Instance.Rows)
                    posible = false;
                foreach(Object o in ObjectList.Instance.ObjectsValues)
                {
                    foreach (Field ff in o.Fields)
                    {
                        if (f.y + 1 == ff.y && f.x == ff.x)
                            posible = false;
                    }
                }
            }
            if (posible)
            {
                foreach (Field f in _Fields)
                {
                    f.y++;
                }
            }
            return posible;
        }
        public bool MoveDownStatic()
        {
            bool posible = true;
            foreach (Field f in _Fields)
            {
                if (f.y + 1 == Settings.Instance.Rows)
                    posible = false;
                foreach (Object o in ObjectList.Instance.ObjectsValues)
                {
                    if(o != this)
                    foreach (Field ff in o.Fields)
                    {
                        if (f.y + 1 == ff.y && f.x == ff.x)
                            posible = false;
                    }
                }
            }
            if (posible)
            {
                foreach (Field f in _Fields)
                {
                    f.y++;
                }
            }
            return posible;
        }
        public void MoveLeft()
        {
            bool posible = true;
            foreach (Field f in _Fields)
            {
                if (f.x == 0)
                    posible = false;
                foreach (Object o in ObjectList.Instance.ObjectsValues)
                {
                    foreach (Field ff in o.Fields)
                    {
                        if (f.x - 1 == ff.x && f.y == ff.y)
                            posible = false;
                    }
                }
            }
            if (posible)
            {
                foreach (Field f in _Fields)
                {
                    f.x--;
                }
            }
        }
        public void MoveRight()
        {
            bool posible = true;
            foreach (Field f in _Fields)
            {
                if (f.x+1 == Settings.Instance.Colums)
                    posible = false;
                foreach (Object o in ObjectList.Instance.ObjectsValues)
                {
                    foreach (Field ff in o.Fields)
                    {
                        if (f.x + 1 == ff.x && f.y == ff.y)
                            posible = false;
                    }
                }
            }
            if (posible)
            {
                foreach (Field f in _Fields)
                {
                    f.x++;
                }
            }
        }
        public void Rotate()
        {
            bool posible = true;
            List<Field> fSave = this.Fields;
            switch (State)
            {
                case 1:
                    if (rotation == 0 && Fields[0].x > 0 && Fields[0].x < Settings.Instance.Colums - 1)
                    {
                        
                        if (posible)
                        {
                            Fields[0].x--;
                            Fields[0].y++;
                            Fields[2].x++;
                            Fields[2].y--;
                            rotation = 1;
                        }
                    }
                    else if (rotation == 1 && Fields[0].y < Settings.Instance.Rows - 1)
                    {
                        if (posible)
                        {
                            Fields[0].x++;
                            Fields[0].y--;
                            Fields[2].x--;
                            Fields[2].y++;
                            rotation = 0;
                        }
                    }
                    break;
                case 3:
                    switch (rotation)
                    {
                        case 0:
                            Fields[3].x--;
                            Fields[3].y++;
                            rotation++;
                            break;
                        case 1:
                            Fields[0].x++;
                            Fields[0].y++;
                            rotation++;
                            break;
                        case 2:
                            Fields[2].x++;
                            Fields[2].y--;
                            rotation++;
                            break;
                        case 3:
                            Fields[2].x--;
                            Fields[2].y++;
                            Fields[0].x--;
                            Fields[0].y--;
                            Fields[3].x++;
                            Fields[3].y--;
                            rotation = 0;
                            break;
                    }
                    break;
                case 5:
                    switch (rotation)
                    {
                        case 0:
                            Fields[0].x--;
                            Fields[0].y++;
                            Fields[1].x-=2;
                            Fields[1].y += 2;
                            Fields[3].x -= 2;
                            Fields[3].y += 2;
                            Fields[5].x-=2;
                            rotation++;
                            break;
                        case 1:
                            Fields[4].x++;
                            Fields[4].y+=2;
                            Fields[5].x++;
                            Fields[5].y += 2;
                            Fields[3].x+=2;
                            Fields[3].y--;
                            Fields[2].x+=3;
                            Fields[2].y+=2;
                            rotation++;
                            break;
                        case 2:
                            Fields[1].x+=2;
                            Fields[0].x += 3;
                            Fields[0].y-=2;
                            Fields[5].y -= 2;
                            Fields[4].x += 2;
                            Fields[4].y -= 1;
                            rotation++;
                            break;
                        case 3:
                            Fields[0].x-=2;
                            Fields[0].y++;
                            Fields[1].y-=2;
                            Fields[2].x-=3;
                            Fields[2].y -= 2;
                            Fields[3].y--;
                            Fields[5].x++;
                            Fields[4].x-=3;
                            Fields[4].y--;
                            rotation = 0;
                            break;
                    }
                    break;
            }

            foreach (Object o in ObjectList.Instance.ObjectsValues)
            {
                foreach (Field f in o.Fields)
                {
                    foreach(Field ff in Fields)
                    {
                        if (ff.x == f.x && ff.y == f.y && ff != f)
                            posible = false;
                    }
                    
                }
            }
            if (!posible)
                Fields = fSave;
        }
        #endregion
    }
}
