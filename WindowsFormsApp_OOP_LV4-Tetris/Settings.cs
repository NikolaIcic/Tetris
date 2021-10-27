using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    class Settings
    {
        #region atributs
        private static Settings _Instance = null;
        #endregion

        #region properties
        public static Settings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Settings();
                return _Instance;
            }
        }
        public int fieldSize { get; set; }
        public int Rows { get; set; }
        public int Colums { get; set; }
        public int Speed { get; set; }
        public bool GameOver { get; set; }
        public bool Pause { get; set; }
        public int Time { get; set; }
        public int Score { get; set; }
        #endregion

        #region methods
        private Settings()
        {
            fieldSize = 40;
            Colums = 10;
            Rows = 15;
            Speed = 1000;
            GameOver = false;
            Pause = false;
            Time = 0;
            Score = 0;
        }
        public void Reset()
        {
            GameOver = false;
            Pause = false;
            Time = 0;
            Score = 0;
        }
        #endregion
    }
}
