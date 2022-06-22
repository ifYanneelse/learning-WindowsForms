using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeClientes
{
    public enum EnumEstadoCivil
    {
        Solteiro,
        Casado,
        Divorciado,
        Viúvo
    }
    public class Cliente
    {
     
        public int Cod { get; set; } //precisa ser autoincremento | não vai no construtor
        public string Nome { get; set; }
        public long CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal RendaMensal { get; set; }
        public EnumEstadoCivil EstadoCivil { get; set; }
        public bool TemFilhos { get; set; }
        public string Nacionalidade { get; set; }
        public string PlacaVeiculo { get; set; }


    public Cliente(string nome, long cPF, DateTime dataNascimento, 
        decimal rendaMensal, EnumEstadoCivil estadoCivil, 
        bool temFilhos, string nacionalidade, string placaVeiculo)

        {
            this.Cod = 0; //quando um novo cliente for criado, o código inicial será 0
            this.Nome = nome;
            this.CPF = cPF;
            this.DataNascimento = dataNascimento;
            this.RendaMensal = rendaMensal;
            this.EstadoCivil = estadoCivil;
            this.TemFilhos = temFilhos;
            this.Nacionalidade = nacionalidade;
            this.PlacaVeiculo = placaVeiculo;
        }

        public Cliente()
        {
            this.Cod = 0; //criar um cliente sem passar todos os valores
        }//cria um cliente vazio e depois vai modificando


        //precisa ter uma listagem de clientes estáticas
        public static List<Cliente> Listagem { get; set; }

        static Cliente()
        {
            Cliente.Listagem = new List<Cliente>();
        }


        //com valores que vem da interface visual
        //mecanismo de auto incremento
        public static Cliente Inserir(Cliente cliente) //vai inserir o cliente q recebe como parametro
        {//se a lista tiver vazia, o cliente recebe cod =1
         //se tiver cliente, vai pegar o cod máximo e somar +1
            int cod = Cliente.Listagem.Count > 0 ?
                Cliente.Listagem.Max(c => c.Cod) + 1 : 1;

            cliente.Cod = cod;
            Cliente.Listagem.Add(cliente); //o novo cliente vai receber novo cod
            return cliente; //retorna o código novo q ele recebeu
        }






    }






}
