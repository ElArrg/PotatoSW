using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoSW
{
    public class Attribute
    {
        private string name;
        private string dataType;
        private string domain;

        /// <summary>
        /// Construye un atributo solo con el nombre del atributo.
        /// </summary>
        /// <param name="name">Nombre del atributo.</param>
        public Attribute(string name) => Name = name;

        /// <summary>
        /// Construye un atributo con todos los datos que le describen.
        /// </summary>
        /// <param name="name">Nombre del atributo.</param>
        /// <param name="dataType">Tipo de dato del atributo.</param>
        /// <param name="domain">Expresión regular que define el dominio del atributo.</param>
        public Attribute(string name, string dataType, string domain)
        {
            Name = name;
            DataType = dataType;
            Domain = domain;
        }


        /// <summary>
        /// Nombre del atributo.
        /// </summary>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Tipo de dato del atributo.
        /// </summary>
        public string DataType { get => dataType; set => dataType = value; }
        /// <summary>
        /// Expresión regular que define el dominio del atributo.
        /// </summary>
        public string Domain { get => domain; set => domain = value; }
    }
}
