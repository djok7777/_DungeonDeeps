using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Items
{
    public class Armor : Item
    {
        private int _defenseValue;

        public Armor(int id, string name, int price, int defenseValue) : base(id, name, price)
        {
            DefenseValue = defenseValue;
        }

        public int DefenseValue
        {
            get
            {
                return _defenseValue;
            }
            set
            {
                _defenseValue = value;
            }
        }
    }
}
