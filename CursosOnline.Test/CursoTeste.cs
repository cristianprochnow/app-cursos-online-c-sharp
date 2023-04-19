using ExpectedObjects;

namespace CursosOnline.Test
{
    public class CursoTeste
    {
        /**
         * Criar Curso
         * 
         * Eu, como administrador, preciso criar e editar os cursos para as novas matrículas.
         * 
         * 1. Criar curso com nome, carga horária, público-alvo e valor do curso;
         * 2. Opções de público-alvo: secundarista, universitária, profissional e empreendedor.
         * 3. Todos os campos são de preenchimento obrigatório.
         */

        [Fact]
        public void CriarCurso()
        {
            /**
             * Isso daqui é um objeto anônimo, que em grosso modo é a tradução
             * para código de um JSON.
             * 
             * Por isso que no momento de comparação colocamos a função de 
             * `ToExpectedObject`, para que a comparação seja possível
             * e assim possamos ter objeto com objeto para comparação.
             */
            var curso = new
            {
                Name = "Contabilidade Básica",
                Publico = "Emprendedor",
                /**
                 * Usada a conversão de `double`, pois
                 * já que não há identificação de tipo,
                 * um simples número fica muito ambíguo.
                 * 
                 * Isto é, pode ser int, double, float, 
                 * então temos que identificar o tipo 
                 * específico.
                 */
                CargaHoraria = (double) 30,
                Valor = (double) 45
            };

            Curso novoCurso = new Curso(curso.Name, curso.CargaHoraria, curso.Publico, curso.Valor);

            /**
             * Feito assim pois as boas práticas recomendam ter apenas um Assert por
             * função de teste. No caso, já que queremos comparar várias propriedades,
             * teríamos de usar vários e vários Assert, o que não é legal.
             * 
             * Com isso, usamos essa lib para fazer com que haja apenas uma comparação,
             * fazendo com que seja possível testar, além de seguir o recomendado.
             */
            curso.ToExpectedObject().ShouldMatch(novoCurso);
        }
    }

    public class Curso
    {
        // Estes são os "campos", que possuem representação apenas na classe
        private string name;
        private double cargaHoraria;
        private string publico;
        private double valor;
        private double valorDesconto;

        public Curso(string name, double cargaHoraria, string publico, double valor)
        {
            this.Name = name;
            this.CargaHoraria = cargaHoraria;
            this.Publico = publico;
            this.Valor = valor;
            this.valorDesconto = 10;
        }

        // E aqui temos as propriedades, que servem mais para a representação e manipulação de valores.
        public string Name { get => name; set => name = value; }
        /**
         * Nesse caso, quando temos tanto o get quanto set, podemos dar valor à variável e também
         * ler o que está dentro dela.
         */
        public double CargaHoraria { get => cargaHoraria; set => cargaHoraria = value; }
        public string Publico { get => publico; set => publico = value; }
        /**
         * A propriedade pode também ter apenas um ou outro. Ou seja, apenas ser lida ou apenas 
         * ganhar valor.
         */
        public double Valor { get => valor; set => valor = value; }

        /**
         * Além disso, em cada um dos métodos, é possível passar um processamento personalizado, 
         * para que cada vez que seja realizada uma interação, então o processamento vai ser
         * feito conforme a alteração.
         */
        public double ValorComDesconto { get => valor - (valor * (this.valorDesconto / 100)); }

        /**
         * Com isso, temos então os campos, que determinam a personalidade, a característica intrínseca
         * da classe, dizendo as características que a tornam assim. já, a persa
         */
    }
}