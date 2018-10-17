using LibraryClass.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.System
{
    public static class DataLoad
    {
        private static string _path;
        private static List<Item> _items = new List<Item>();
        private static List<Potion> _potions = new List<Potion>();
        private static List<Sword> _swords = new List<Sword>();
        private static List<Armor> _armors = new List<Armor>();
        private static List<Monster> _monsters = new List<Monster>();
        private static string[] _monsterDescription;
        private static List<Map> _maps = new List<Map>();
        private static Character character;

        static DataLoad()
        {
            Path = @"Resources";
            LoadCharacter();
            LoadItems();
            LoadMaps();
            LoadMonstersDescriptions();
            LoadMonsters();

            Character.Sword = Swords[0];
            Character.Armor = Armors[0];
        }

        #region Propiedades
        public static String Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
            }
        }

        public static List<Item> Items
        {
            get
            {
                return _items;
            }

            set
            {
                _items = value;
            }
        }

        public static List<Potion> Potions
        {
            get
            {
                return _potions;
            }

            set
            {
                _potions = value;
            }
        }

        public static List<Sword> Swords
        {
            get
            {
                return _swords;
            }

            set
            {
                _swords = value;
            }
        }

        public static List<Armor> Armors
        {
            get
            {
                return _armors;
            }

            set
            {
                _armors = value;
            }
        }

        public static List<Monster> Monsters
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

        public static List<Map> Maps
        {
            get
            {
                return _maps;
            }

            set
            {
                _maps = value;
            }
        }

        public static Character Character
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

        public static string[] MonsterDescription
        {
            get
            {
                return _monsterDescription;
            }

            set
            {
                _monsterDescription = value;
            }
        }
        #endregion

        #region Metodos
        private static void LoadCharacter() {
            Character = new Character(1, "Player", 1, 100, 100, 20, 0.1f, 1, 1, 0);
        }

        private static void LoadMonstersDescriptions() {
            MonsterDescription = new string[5];
            MonsterDescription[0] = "A hostile bat living \nin caves or dungeons";
            MonsterDescription[1] = "Undead living \nanimated by black \nmagic";
            MonsterDescription[2] = "Summoned beast from \nthe deeps of hell";
            MonsterDescription[3] = "Mysterious magical \nbeing, loves play \nwith it's prey";
            MonsterDescription[4] = "A chest with a\n unknow item inside";
        }

        private static void LoadMonsters() {
            Monsters.Add(new Monster(100, "Bat", 30, 30, 10, 10, 2, 1, Elements.Neutral, MonsterDescription[0],"_bat"));
            Monsters.Add(new Monster(110, "Skeleton", 50, 50, 10, 15, 2, 2, Elements.Dark, MonsterDescription[1], "_skeleton"));
            Monsters.Add(new Monster(120, "Demon", 50, 50, 10, 15, 3, 3, Elements.Fire, MonsterDescription[2], "_demon"));
            Monsters.Add(new Monster(130, "Fairy", 10, 10, 50, 5, 1, 1, Elements.Ligth, MonsterDescription[3], "_fairy"));
            Monsters.Add(new Monster(140, "Chest", 1, 1, 0, 0, 0, 0, Elements.Neutral, MonsterDescription[4],"_chest"));
        }

        private static void LoadItems()
        {
            Potions.Add(new Potion(101, "Red potion", 50, 20));
            Potions.Add(new Potion(102, "Orange potion", 200, 40));
            Potions.Add(new Potion(103, "White Potion", 500, 100));
            Potions.Add(new Potion(120, "Blue potion", 300, 10));

            Swords.Add(new Sword(200, "Wood sword", 200, 10));
            Swords.Add(new Sword(201, "Iron sword", 1000, 20));

            Armors.Add(new Armor(300, "Leather Jacket", 10, 1));
            Armors.Add(new Armor(301, "Iron chest", 100, 2));
        }

        private static void LoadMaps() {
            Maps.Add(new Map(0, 0, 10, 3, DataLoad.Monsters, "_map1"));
        }
        #endregion
    }
}
