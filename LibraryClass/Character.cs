using LibraryClass.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass
{
    public class Character : Entity
    {
        private int _level, _gold;
        private List<Item> _inventory = new List<Item>();
        private Armor _armor = null;
        private Sword _sword = null;

        public Character(int id, string name, int level, int maxHp, int currentHp, int mana, float experience, int attack, int defense, int gold) 
            : base (id, name, maxHp, currentHp, mana, experience, attack, defense)
        {
            Level = level;
            Gold = _gold;
        }

        #region Propierties
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }

        public List<Item> Inventory
        {
            get
            {
                return _inventory;
            }

            set
            {
                _inventory = value;
            }
        }

        public int Gold
        {
            get
            {
                return _gold;
            }

            set
            {
                _gold = value;
            }
        }

        public Armor Armor
        {
            get
            {
                return _armor;
            }

            set
            {
                _armor = value;
            }
        }

        public Sword Sword
        {
            get
            {
                return _sword;
            }

            set
            {
                _sword = value;
            }
        }
        #endregion

        #region Methods
        public override int CalculateAttack()
        {
            int damage = 0;
            if (Sword != null)
            {
                damage = Attack + Sword.AttackValue;
                return damage;
            }
            else
            {
                damage = Attack;
            }
            return damage;
        }

        public override int CalculateDefense(int value)
        {
            int damage = 0;
            damage = value - (Defense + Armor.DefenseValue);
            return damage;
        }

        public override int CalculateEscape()
        {
            throw new NotImplementedException();
        } 
        #endregion

    }
}