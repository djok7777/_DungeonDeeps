using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass
{
    public abstract class Entity : BattleCommands
    {
        public string _name;
        public int _id, _maxHp, _currentHp, _mana, _attack, _defense;
        public float _experience;

        #region Constructor
        public Entity(int id, String name, int maxHp, int currentHp, int mana, float experience, int attack, int defense)
        {
            Id = id;
            Name = name;
            MaxHp = maxHp;
            CurrentHp = currentHp;
            Mana = mana;
            Experience = experience;
            Attack = attack;
            Defense = defense;
        }
        #endregion

        #region Propieties
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public int MaxHp
        {
            get
            {
                return _maxHp;
            }

            set
            {
                _maxHp = value;
            }
        }

        public int CurrentHp
        {
            get
            {
                return _currentHp;
            }

            set
            {
                _currentHp = value;
            }
        }

        public int Mana
        {
            get
            {
                return _mana;
            }

            set
            {
                _mana = value;
            }
        }

        public int Attack
        {
            get
            {
                return _attack;
            }

            set
            {
                _attack = value;
            }
        }

        public int Defense
        {
            get
            {
                return _defense;
            }

            set
            {
                _defense = value;
            }
        }

        public float Experience
        {
            get
            {
                return _experience;
            }

            set
            {
                _experience = value;
            }
        }
        #endregion

        public abstract int CalculateAttack();
        public abstract int CalculateDefense(int value);
        public abstract int CalculateEscape();
    }
}
