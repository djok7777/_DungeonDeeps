using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass
{
    public class Map
    {
        private int _height, _width, _rateMonsters, _rateItems;
        private List<Monster> _monsters;
        private string _mapGraphic;
        private char[,] _logicalMap = new char[20,60];

        #region Constructor
        public Map(int height, int width, int rateMonsters, int rateItems, List<Monster> monsters, string file_name)
        {
            Height = height;
            Width = width;
            RateMonsters = rateMonsters;
            RateItems = rateItems;
            Monsters = monsters;
            MapGraphic = CreateMap(file_name);
            LogicalMap = ObtainLogicalMap(file_name);
        }
        #endregion

        #region Properties
        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public int RateMonsters
        {
            get
            {
                return _rateMonsters;
            }

            set
            {
                _rateMonsters = value;
            }
        }

        public int RateItems
        {
            get
            {
                return _rateItems;
            }

            set
            {
                _rateItems = value;
            }
        }

        public List<Monster> Monsters
        {
            get
            {
                return _monsters;
            }

            set
            {
                _monsters = value;
            }
        }

        public string MapGraphic
        {
            get
            {
                return _mapGraphic;
            }

            set
            {
                _mapGraphic = value;
            }
        }

        public char[,] LogicalMap
        {
            get
            {
                return _logicalMap;
            }

            set
            {
                _logicalMap = value;
            }
        }


        #endregion

        #region Methods
        public string CreateMap(string file_name)
        {
            StringBuilder strMap = new StringBuilder();
            StreamReader sr = new StreamReader(@"Resources/Maps/" + file_name + ".txt");
            string line = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                strMap.AppendFormat(line + Environment.NewLine);
            }
            return strMap.ToString();
        }

        public char[,] ObtainLogicalMap(string file_name)
        {
            StringBuilder strMap = new StringBuilder();
            StreamReader sr = new StreamReader(@"Resources/Maps/" + file_name + ".txt");
            string line = string.Empty;
            string fullMap = string.Empty;
            char[,] newMap = new char[24, 60];

            //Get string with map line to line, without \r + \n
            while ((line = sr.ReadLine()) != null)
            {
                fullMap = fullMap + line;
            }

            //String descomposed into a multiArray
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    newMap[i, j] = fullMap[i * 60 + j];
                }
            }
            return newMap;
        }//End ConverMapToArray

        public void SummonMonsters() {
            Random rnd = new Random();
            int freeZone = CountSpaces();   //Quantity of ' ' spaces into the multiArray

            for (int i = 0; i < LogicalMap.GetLength(0); i++)
            {
                for (int j = 0; j < LogicalMap.GetLength(1); j++)
                {
                    if (LogicalMap[i, j] == ' ' && rnd.Next(0, freeZone) < freeZone * RateMonsters/100) {
                        LogicalMap[i, j] = 'M';
                    }
                }
            }
        }//END SummonMonsters

        public void SummonItems() {
            Random rnd = new Random();
            int freeZone = CountSpaces();
            for (int i = 0; i < LogicalMap.GetLength(0); i++)
            {
                for (int j = 0; j < LogicalMap.GetLength(1); j++)
                {
                    if (LogicalMap[i, j] == ' ' && rnd.Next(0, freeZone) < freeZone * RateItems / 100)
                    {
                        LogicalMap[i, j] = '#';
                    }
                }
            }
        }

        public int CountSpaces() {
            int spaces = 0;
            for (int i = 0; i < LogicalMap.GetLength(0); i++)
            {
                for (int j = 0; j < LogicalMap.GetLength(1); j++)
                {
                    if (LogicalMap[i, j] == ' ')
                        spaces++;
                }
            }//END CountSpaces
            return spaces;
        }
        #endregion
    }   
}
