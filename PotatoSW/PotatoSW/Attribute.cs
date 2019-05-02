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

        public Attribute(string name) => Name = name;

        public Attribute(string name, string dataType, string domain)
        {
            Name = name;
            DataType = dataType;
            Domain = domain;
        }

        public string Name { get => name; set => name = value; }
        public string DataType { get => dataType; set => dataType = value; }
        public string Domain { get => domain; set => domain = value; }
    }
}
