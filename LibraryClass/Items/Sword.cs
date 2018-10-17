using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Items
{
    public class Sword : Item
    {
        private int _attackValue;

        public Sword(int id, string name, int price, int attackValue) : base(id, name, price)
        {
            AttackValue = attackValue;
        }

        #region Propiedades
        public int AttackValue
        {
            get
            {
                return _attackValue;
            }

            set
            {
                _attackValue = value;
            }
        } 
        #endregion
    }
}
