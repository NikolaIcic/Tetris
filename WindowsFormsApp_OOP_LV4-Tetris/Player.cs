using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    public class Player
    {
        #region atributs
        private string _name;
        private int _score;
        #endregion

        #region properties
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int score
        {
            get { return _score; }
            set { _score = value; }
        }
        #endregion

        #region methods
        public Player()
        {

        }
        public Player(string Name,int Score)
        {
            name = Name;
            score = Score;
        }
        #endregion
    }
}
