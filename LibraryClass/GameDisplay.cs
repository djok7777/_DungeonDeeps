using LibraryClass.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass
{
    public class GameDisplay
    {
        private int xPosition = 10, yPosition = 10;
        private int xMapPosition = 4, yMapPosition = 4;
        private List<Map> maps = DataLoad.Maps;
        private Map _actualMap;
        private string[] _message = { "","",""};

        private Character character = DataLoad.Character;
        //Constants
        private const int height = 40, width = 100;

        public GameDisplay(int xPlayerPos, int yPlayerPos) {
            maps = DataLoad.Maps;
            ActualMap = DataLoad.Maps[0];
            ActualMap.SummonMonsters();
            ActualMap.SummonItems();
            DrawMap(ActualMap.LogicalMap);
            DrawPlayerInfo();
            DrawMessageBox("");
        }

        #region Properties
        public int XPlayerPosition
        {
            get
            {
                return xPosition;
            }

            set
            {
                xPosition = value;
            }
        }

        public int YPlayerPosition
        {
            get
            {
                return yPosition;
            }

            set
            {
                yPosition = value;
            }
        }

        public Character Character
        {
            get
            {
                return character;
            }

            set
            {
                character = value;
            }
        }

        public Map ActualMap
        {
            get
            {
                return _actualMap;
            }

            set
            {
                _actualMap = value;
            }
        }

        public string[] Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }
        #endregion

        //Draw mainframe of game
        public void DrawMainFrame()
        {
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("│");
                Console.SetCursorPosition(width, i);
                Console.Write("│");
            }
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("─");
                Console.SetCursorPosition(i, height);
                Console.Write("─");
            }
            Console.SetCursorPosition(1, 1); //upper-left
            Console.Write("┌");
            Console.SetCursorPosition(width, 1);    //upper-right
            Console.Write("┐");
            Console.SetCursorPosition(1, height);   //bottom-left
            Console.Write("└");
            Console.SetCursorPosition(width, height);   //bottom-right
            Console.Write("┘");
        }

        //Draw a loaded map, into the map section of display according to xPosition and yPosition
        private void DrawMap(char[,] logicalMap)
        {
            int x = xMapPosition, y = yMapPosition;
            int initXPos = xMapPosition;    //to rememeber for the next line print
            for (int i = 0; i < logicalMap.GetLength(0); i++)
            {
                x = initXPos;
                for (int j = 0; j < logicalMap.GetLength(1); j++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(logicalMap[i, j]);
                    x++;
                }
                y++;
            }
        }

        public void DrawPlayerInfo() {
            string info = PlayerInfo();
            int xPosition = 74, yPosition = 5;
            int xInitialPos = xPosition;

            for (int i = 0; i < info.Length; i++)
            {
                if (info[i] == '\n')
                {
                    yPosition++;
                    xPosition = xInitialPos;
                }
                else
                {
                    Console.SetCursorPosition(xPosition, yPosition);
                    Console.Write(info[i]);
                    xPosition++;
                }
            }
        }

        public string PlayerInfo() {
            StringBuilder str = new StringBuilder();
            int padR = 15;
            str.AppendFormat((new string('-', 5) + "Player Info" + new string('-', 5)));
            str.AppendFormat("\n\nLevel".PadRight(padR) + Character.Level);
            str.AppendFormat("\n\nName".PadRight(padR) + Character.Name);
            str.AppendFormat("\n\nHp".PadRight(padR) + Character.CurrentHp);
            str.AppendFormat("\n\nSp".PadRight(padR) + Character.Mana);
            str.AppendFormat("\n\nExp".PadRight(padR) + Character.Experience);
            str.AppendFormat("\n\n" + (new string('-', 22)));
            str.AppendFormat("\n\nPotions".PadRight(padR));
            str.AppendFormat("\n\nGold".PadRight(padR) + Character.Gold);
            return str.ToString();
        }

        public bool CheckCollision(ConsoleKey ck) {
            int x = xPosition - xMapPosition;
            int y = yPosition - xMapPosition;
            char wall = (char)9618;
            if (ck == ConsoleKey.LeftArrow && ActualMap.LogicalMap[y, x - 1] == wall)
            {
                return true;
            }
            else if (ck == ConsoleKey.RightArrow && ActualMap.LogicalMap[y, x + 1] == wall)
            {
                return true;
            }
            else if (ck == ConsoleKey.UpArrow && ActualMap.LogicalMap[y - 1, x] == wall) {
                return true;
            }
            else if (ck == ConsoleKey.DownArrow && ActualMap.LogicalMap[y + 1, x] == wall)
            {
                return true;
            }
                return false;
        }

        public void PlayerMovement()
        {
            var ch = Console.ReadKey().Key;
                switch (ch)
                {
                    case ConsoleKey.LeftArrow:
                    if (! CheckCollision(ch))
                    {
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition--;
                        CheckEvent();
                    }
                        break;
                    case ConsoleKey.UpArrow:
                    if (!CheckCollision(ch))
                    {
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition--;
                        CheckEvent();
                    }
                    break;
                    case ConsoleKey.RightArrow:
                    if (!CheckCollision(ch))
                    {
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition++;
                        CheckEvent();
                    }
                        break;
                    case ConsoleKey.DownArrow:
                    if (!CheckCollision(ch))
                    {
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition++;
                        CheckEvent();
                    }
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(xPosition, yPosition);
                Console.WriteLine((char)2);
        }//END PlayerMovement

        public void SetPlayerPosition(int xPos, int yPos) {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.WriteLine((char)2);
        }

        public void DrawMessageBox(string msg) {
            int x = 4, y = 30;
            int sentenceLength = 60;

            Message[0] = Message[1];
            Message[1] = Message[2];
            Message[2] = msg;

            Console.SetCursorPosition(x, y);
            Console.Write("Message : ");
            for (int i = 0; i < Message.Length; i++)
            {
                Console.SetCursorPosition(x, ++y);
                switch (i)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        break;
                }
                Console.Write(">" + Message[i]);
                Console.Write(new string(' ', sentenceLength - Message[i].Length));
            }
        }

        public void CheckEvent()
        {
            char symbol = ActualMap.LogicalMap[yPosition - yMapPosition, xPosition - xMapPosition];
            switch (symbol)
            {
                case 'M':
                    Battle battle = new Battle(Character);
                    DrawMessageBox("Un mounstro se ha aparecido!");
                    battle.BattleEvent();
                    ReDrawElements();
                    break;
                case '#':
                    DrawMessageBox("Has obtenido un item");
                    break;
                default:
                    break;
            }
        }

        public void ReDrawElements()
        {
            Console.Clear();
            DrawMainFrame();
            DrawMap(ActualMap.LogicalMap);
            DrawPlayerInfo();
            DrawMessageBox("");
        }
    }
}
