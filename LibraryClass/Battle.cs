using LibraryClass.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryClass
{
    class Battle
    {
        private Monster _monster;
        private Character _character;
        private int xMenuFrame = 54, yMenuFrame = 25;
        public bool BattleState;
        private string[] _message = { "", "", "" };
        private const int height = 40, width = 100;

        #region Constructor
        public Battle(Character character)
        {
            Character = character;
            Init();
        }

        public void Init() {
            BattleState = true;
            Monster = PickRandomMonster();
            DrawBattleElements();
            BattleEvent();
        }
        #endregion

        #region Propierties
        public Monster Monster
        {
            get
            {
                return _monster;
            }

            set
            {
                _monster = value;
            }
        }

        public Character Character
        {
            get
            {
                return _character;
            }

            set
            {
                _character = value;
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
        public void MonsterDissapear()
        {
            int x = 25, y = 3;  //Coords to display each Graphic character of Monster
            int sleepInterval = 80;
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', 60));

            Thread.Sleep(sleepInterval);
            for (int i = 0; i < Monster.Graphic.Length; i++)
            {
                if (Monster.Graphic[i] == '\n')
                {
                    y++;
                    Console.SetCursorPosition(x, y);
                    Console.Write(new string(' ', 60));
                    Thread.Sleep(sleepInterval);
                }
            }
        }

        public int MonsterAttack() {
            int damage = 0;
            //Normal attack
            damage = Monster.Attack - Character.Defense;
            if (damage < 1)
            {
                damage = 1;
            }
            DrawMessageBox("The enemy hits you " + damage + " damage!");
            return damage;
        }

        public void BattleEvent()
        {
            int command = 0;
            int x = xMenuFrame + 2, y = yMenuFrame + 2;
            DrawCursor(x, y);

            while (BattleState)
            {
                var ch = Console.ReadKey().Key;
                switch (ch)
                {
                    case ConsoleKey.Enter:
                        MenuEnterCommand(command);
                        break;
                    case ConsoleKey.UpArrow:
                        if (y >= yMenuFrame + 4)
                        {
                            Console.SetCursorPosition(xMenuFrame + 2, y);
                            Console.Write(" ");
                            DrawCursor(x, y -= 2);
                            command--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (y <= yMenuFrame + 8)
                        {
                            Console.SetCursorPosition(xMenuFrame + 2, y);
                            Console.Write(" ");
                            DrawCursor(x, y += 2);
                            command++;
                        }
                        break;
                }//End switch

                //Holds cursor in the defined position after press a Key
                Console.SetCursorPosition(x + 1, y);

                if (Monster.CurrentHp <= 0)
                {
                    MonsterDissapear();
                    DrawMessageBox("The enemy has been defeated!");
                    Thread.Sleep(1000);
                    BattleState = false;
                    Console.ReadKey();
                }
            }
        }

        public void MenuEnterCommand(int command) {
            switch (command)
            {
                #region Command = 0 (Attack)
                case 0:
                    //Character turn
                    int damage = Character.CalculateAttack() - Monster.Defense;
                    Monster.CurrentHp = Monster.CurrentHp - damage;
                    DrawMessageBox("You attack does " + damage + " damage to the enemy");
                    //Monster turn
                    if (Monster.CurrentHp > 0)
                    {
                        MonsterAttack();
                    }
                    break; 
                #endregion
                case 1: //Skills
                    DrawMessageBox("Bash");
                    break;
                case 2: //Defense
                    DrawMessageBox("Te defiendes del ataque");
                    break;
                case 3: //Items
                    DrawMessageBox("Selecciona el item");
                    break;
                #region Command = 4 (Escape)
                case 4: //Escape
                    if (EscapeOption())
                    {
                        Random rnd = new Random();
                        if (rnd.Next(0, 5) == 0)
                        {
                            DrawMessageBox("Has logrado escapar");
                            Thread.Sleep(1000);
                            BattleState = false;
                        }
                        else
                        {
                            DrawMessageBox("Escape sin éxito");
                            Thread.Sleep(1000);
                            MonsterAttack();
                        }
                    }
                    break; 
                #endregion
                default:
                    break;
            }
        }

        public void EraseEscapeMenu(int x, int y) {
            Console.SetCursorPosition(x - 2, y);
            Console.Write("          ");
            Console.SetCursorPosition(x - 2, y + 2);
            Console.Write("          ");
        }

        public Monster PickRandomMonster() {
            Random rnd = new Random();
            Monster m = DataLoad.Monsters[rnd.Next(0, 5)];
            Monster mon = new Monster(m.Id, m.Name, m.MaxHp, m.CurrentHp, m.Mana, m.Experience, m.Attack, m.Defense, m.Element, m.Description, m.FileName);
            return mon;
        }

        //Methods used to draw battle elements, character info, monsters, etc.
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

        public void DrawCursor(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(">");
        }

        public void DrawBattleElements()
        {
            Console.Clear();
            DrawMainFrame();
            DrawPlayerInfo();
            DrawMonster();
            DrawMonsterInfo();
            DrawMenuCommandFrame();
            DrawMenuCommands();
            DrawMessageBox("");
        }

        public void DrawMenuCommands()
        {
            int x = xMenuFrame + 4, y = yMenuFrame + 2;

                        Console.SetCursorPosition(x, y);
                        Console.Write("Attack");

                        Console.SetCursorPosition(x, y + 2);
                        Console.Write("Skill");

                        Console.SetCursorPosition(x, y + 4);
                        Console.Write("Defense");

                        Console.SetCursorPosition(x, y + 6);
                        Console.Write("Items");

                        Console.SetCursorPosition(x, y + 8);
                        Console.Write("Escape");
        }

        public void DrawMenuCommandFrame()
        {
            int x = xMenuFrame, y = yMenuFrame;
            int wide = 15;
            Console.SetCursorPosition(x, y);
            Console.Write(new string('─', wide));
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(x, y + i + 1);
                Console.Write("│");
                Console.SetCursorPosition(x + wide, y+i + 1);
                Console.Write("│");
            }
            Console.SetCursorPosition(x, y + 10);
            Console.Write(new string('─', wide));
        }

        public bool EscapeOption()
        {

            int x = xMenuFrame + 18, y = yMenuFrame + 8;
            bool decision = true;
            bool escapeCycle = false;

            DrawMessageBox("50 % para escapar");
            Console.SetCursorPosition(x, y);
            Console.Write("Yes");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("No");
            DrawCursor(x - 2, y);

            while (escapeCycle == false)
            {
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (decision)
                        {
                            escapeCycle = true;
                            EraseEscapeMenu(x, y);
                            return true;
                        }
                        else
                        {
                            EraseEscapeMenu(x, y);
                            return false;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(x - 2, y + 2);
                        Console.Write(" ");
                        DrawCursor(x - 2, y);
                        decision = true;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(x - 2, y);
                        Console.Write(" ");
                        DrawCursor(x - 2, y + 2);
                        decision = false;
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(x - 1, y);
            }
            return false;
        }

        public void DrawPlayerInfo()
        {
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

        public string PlayerInfo()
        {
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

        public void DrawMonsterInfo() {
            string description = string.Empty;
            int x = 3, y = 3;
            int initX = x;
            for (int i = 0; i <= 8; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(x, y + i);
                        Console.Write("Enemy");
                        Console.ResetColor();
                        break;
                    case 1:
                        Console.SetCursorPosition(x, y + i);
                        Console.Write(Monster.Name);
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(x, y + i);
                        Console.Write("Attribute");
                        Console.ResetColor();
                        break;
                    case 4:
                        Console.SetCursorPosition(x, y + i);
                        Console.Write("{0} element", Monster.Element);
                        break;
                    case 5:
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(x, y + i);
                        Console.Write("Description");
                        Console.ResetColor();
                        break;
                    case 7:
                        for (int j = 0; j < Monster.Description.Length; j++)
                        {
                            if (Monster.Description[j] == '\n')
                            {
                                Console.Write(Monster.Description[j]);
                                y++;
                                x = initX;
                            }
                            else {
                                Console.SetCursorPosition(x++, y + i);
                                Console.Write(Monster.Description[j]);
                            }
                        }
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(x, y + i);
                        Console.Write("HP" + Monster.CurrentHp);
                        Console.ResetColor();
                        break;
                    default:
                        break;
                }
            }
        }

        public void DrawMonster() {
            int x = 25, y = 3;  //Coords to display each Graphic character of Monster
            int initX = x; //Initial position of X

            for (int i = 0; i < Monster.Graphic.Length; i++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(Monster.Graphic[i]);
                x++;
                if (Monster.Graphic[i] == '\n') {
                    Console.Write(Monster.Graphic[i]);
                    y++;
                    x = initX;
                }
            }
        }

        public void DrawMessageBox(string msg)
        {
            int x = 4, y = 30;
            int sentenceLength = 46;

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
                //If word is too length, the console will print until "sentenceLength" characters
                if (sentenceLength >= Message[i].Length)
                {
                    Console.Write(">" + Message[i]);
                    Console.Write(new string(' ', sentenceLength - Message[i].Length));
                }
                else
                {
                    Message[i] = Message[i].Remove(sentenceLength);
                    Console.Write(">" + Message[i]);
                }
            }
        }
    }
}
