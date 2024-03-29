﻿namespace CursosOnline.Domain
{
    public class Curso
    {
        // Estes s�o os "campos", que possuem representa��o apenas na classe
        private string? name;
        private string? descricao;
        private double cargaHoraria;
        private string? publico;
        private double valor;
        private double valorDesconto;
        private double nota;

        public Curso(string name, string descricao, double cargaHoraria, string publico, double valor, double nota)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Preenchimento do nome do curso � obrigat�rio!");
            }
            if (cargaHoraria <= 0)
            {
                throw new ArgumentException("Valor inv�lido para Carga Hor�ria!");
            }
            if (nota < 0) {
                throw new ArgumentException("Nota Inválida!");
            }

            this.Name = name;
            this.CargaHoraria = cargaHoraria;
            this.Descricao = descricao;
            this.Publico = publico;
            this.Valor = valor;
            this.valorDesconto = 10;
            this.Nota = nota;
        }

        // E aqui temos as propriedades, que servem mais para a representa��o e manipula��o de valores.
        public string Name { get => (name != null ? name : ""); set => name = value; }
        public string Descricao { get => (descricao != null ? descricao : ""); set => descricao = value; }
        /**
         * Nesse caso, quando temos tanto o get quanto set, podemos dar valor � vari�vel e tamb�m
         * ler o que est� dentro dela.
         */
        public double CargaHoraria { get => cargaHoraria; set => cargaHoraria = value; }
        public string Publico { get => (publico != null ? publico : ""); set => publico = value; }
        /**
         * A propriedade pode tamb�m ter apenas um ou outro. Ou seja, apenas ser lida ou apenas
         * ganhar valor.
         */
        public double Valor { get => valor; set => valor = value; }
        public double Nota { get => nota; set => nota = value; }

        /**
         * Al�m disso, em cada um dos m�todos, � poss�vel passar um processamento personalizado,
         * para que cada vez que seja realizada uma intera��o, ent�o o processamento vai ser
         * feito conforme a altera��o.
         */
        public double ValorComDesconto { get => valor - (valor * (this.valorDesconto / 100)); }

        /**
         * Com isso, temos ent�o os campos, que determinam a personalidade, a caracter�stica intr�nseca
         * da classe, dizendo as caracter�sticas que a tornam assim. j�, a persa
         */
    }
}