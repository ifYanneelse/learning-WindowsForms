using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentesVisuais01
{
    public class Estado
    {
        public string Sigla { get; set; }   
        public string Nome { get; set; }

        public Estado(string sigla, string nome)
        {
            this.Sigla = sigla;
            this.Nome = nome;
        }
    }
}
