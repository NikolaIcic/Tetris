using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    class ObjectList
    {
        #region atributs
        private List<Object> Objects;
        private static ObjectList _Instance = null;
        #endregion

        #region properties
        public static ObjectList Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ObjectList();
                return _Instance;
            }
        }
        public List<Object> ObjectsValues
        {
            get
            {
                return Objects;
            }
        }
        #endregion

        #region methods
        private ObjectList()
        {
            Objects = new List<Object>();
        }
        public void add(Object o)
        {
            Objects.Add(o);
        }
        public void deleteAll()
        {
            Objects.Clear();
        }
        public void CheckRows()
        {
            for (int i = 0; i < Settings.Instance.Rows; i++)
            {
                int posible = 0;
                foreach (Object o in Objects)
                    foreach (Field f in o.Fields)
                    {
                        if (f.y == i)
                            posible++;
                    }
                if (posible == Settings.Instance.Colums)
                {
                    foreach (Object o in Objects)
                        foreach (Field f in o.Fields.ToList())
                        {
                            if (f.y == i)
                            {
                                o.RemoveField(f);
                            }
                        }
                    Settings.Instance.Score += 100000 / Settings.Instance.Speed;
                }
            }
        }
        #endregion
    }
}
