using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    public class HighScores
    {
        #region atributs
        private List<Player> Players = null;
        private static HighScores _Instance = null;
        #endregion

        #region properties
        public static HighScores Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new HighScores();
                return _Instance;
            }
        }
        public List<Player> PlayerValues
        {
            get { return Players; }
        }
        #endregion

        #region methods
        private HighScores()
        {
            Players = new List<Player>();
        }
        public void AddPlayer(Player p)
        {
            Players.Add(p);
            Players = Players.OrderByDescending(o => o.score).ToList();
        }
        public void DeleteAt(int index)
        {
            Players.RemoveAt(index);
        }
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Player>));
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(File.Create("highscores.xml"));
                ser.Serialize(sw, Players);
            }
            finally
            {
                sw.Close();
            }
        }
        public void Load()
        {
            StreamReader sr = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Player>));
                sr = new StreamReader(File.OpenRead("highscores.xml"));
                Players = (List<Player>)xs.Deserialize(sr);
            }
            finally
            {
                sr.Close();
            }
        }
        #endregion
    }
}
