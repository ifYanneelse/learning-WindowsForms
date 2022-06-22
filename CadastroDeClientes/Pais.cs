using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeClientes
{
    public class Pais
    {

        public string Sigla { get; set; }
        public string Nome { get; set; }

        public Pais(string sigla, string nome)
        {
            this.Sigla = sigla;
            this.Nome = nome;
        }


        public static List<Pais> Listagem { get; set; }

        static Pais()
        {

            Pais.Listagem = new List<Pais>();
            Pais.Listagem.Add(new Pais("BR", "Brasil"));
            Pais.Listagem.Add(new Pais("AR", "Argentina"));
            Pais.Listagem.Add(new Pais("EC", "Equador"));
            Pais.Listagem.Add(new Pais("PE", "Peru"));
            Pais.Listagem.Add(new Pais("CL", "Chile"));
            Pais.Listagem.Add(new Pais("BO", "Bolívia"));
            Pais.Listagem.Add(new Pais("PY", "Paraguay"));
            Pais.Listagem.Add(new Pais("VE", "Venezuela"));
            Pais.Listagem.Add(new Pais("CO", "Colômbia"));

        }



    }
}
