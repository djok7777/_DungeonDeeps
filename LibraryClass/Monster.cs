using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryClass
{
    public class Monster : Entity
    {
        private string _graphic, _fileName;
        private Elements _element;
        private string _description;

        public const int MAX_VERTICAL = 22;   //USO EN METODO FormatoGrafico
        public const int MAX_HORIZONTAL = 45;

        #region Constructor
        public Monster(int id, string name, int maxHp, int currentHp, int mana, float experience, int attack, int defense, Elements element, string description, string fileName)
            : base(id, name, maxHp, currentHp, mana, experience, attack, defense)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = currentHp;
            Mana = mana;
            Attack = attack;
            Defense = defense;
            Experience = experience;
            Element = element;
            Description = description;
            FileName = fileName;
            Graphic = DrawMonster(fileName);
        } 
        #endregion

        #region Properties
        public Elements Element
        {
            get
            {
                return _element;
            }

            set
            {
                _element = value;
            }
        }

        public string Graphic
        {
            get
            {
                return _graphic;
            }

            set
            {
                _graphic = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Extrae desde un .txt el grafico y le establece un formato en string, según nombre de mounstro en txt
        /// Ej: "BAT1" , "SKELETON1", "DEMON1"...
        /// </summary>
        /// <param name="name_id_monster"></param>
        /// <returns></returns>
        private string DrawMonster(string file_name)
        {
            StreamReader graphic_file = new StreamReader(@"Resources/Monsters/"+ file_name + ".txt");
            StringBuilder str = new StringBuilder();
            string line = string.Empty, formattedMon = string.Empty;

            while ((line = graphic_file.ReadLine()) != null)
            {
                str.AppendFormat("{0}", line + Environment.NewLine);
            }

            //Establecer formato grafico con espacios
            formattedMon = FormatoGrafico(str.ToString());
            return formattedMon;
        }

        private string FormatoGrafico(string buffer)
        {
            StringBuilder monG = new StringBuilder();
            int cont_char = 1;
            //Asignarle espacios para formato, según cada caracter del nuevo str
            for (int i = 0; i < buffer.Length; i++)
            {
                //Si encuentra retorno de carro, agrega espacios adicionales
                if (buffer[i] == '\r')
                {
                    monG.Append(new string('-', (MAX_HORIZONTAL - cont_char)) + buffer[i]);
                    cont_char = 0;
                }
                else if (buffer[i] == '\n')
                {
                    monG.Append(buffer[i]);
                    cont_char = 0;
                }
                else
                {
                    monG.Append(buffer[i]);
                }
                cont_char++;
            }

            //Asigna espacios para formato, en la zona superior
            //En posicion 0, insertar caracter ' ' MAX_HORIZONTAL - 1 veces + \r\n , Esto se repite (MAX_VERTICAL - CantLineas(buffer)) veces
            monG.Insert(0, new string('-', MAX_HORIZONTAL - 1) + Environment.NewLine, MAX_VERTICAL - CantLineas(buffer));

            return monG.ToString();
        }

        //Obtiene cantidad de lineas del grafico para simular alto de ventana formateada del mounstro
        private int CantLineas(string buffer)
        {
            int cant = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == '\r')
                {
                    cant++;
                }
            }
            return cant;
        }
        #endregion

        public override int CalculateAttack()
        {
            int damage = 0;
            damage = Attack;
            return damage;
        }

        public override int CalculateDefense(int value)
        {
            int damage = 0;
            damage = value - Defense;
            return damage;
        }

        public override int CalculateEscape()
        {
            throw new NotImplementedException();
        }
    }
}
