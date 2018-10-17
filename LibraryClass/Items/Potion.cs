using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Items
{
    public class Potion : Item
    {
        private int _regenerativeValue;

        public Potion(int id, string name, int price, int regenerativeValue) : base(id, name, price)
        {
            RegenerativeValue = regenerativeValue;
        }

        public int RegenerativeValue
        {
            get
            {
                return _regenerativeValue;
            }

            set
            {
                _regenerativeValue = value;
            }
        }
    }
}
