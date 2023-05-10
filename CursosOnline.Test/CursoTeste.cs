using CursosOnline.Test._Util;
using ExpectedObjects;

namespace CursosOnline.Test
{
    public class CursoTeste
    {
        private string _nome;
        private double _cargaHoraria;
        private string _publico;
        private double _valor;

        /**
         * Numa classe normal, o construtor sempre é chamado apenas uma vez, no momento
         * que instancia a classe.
         * 
         * Na classe de teste, o construtor é chamado em todo início da execução
         * de testes. Então, a cada teste que for executado, o construtor é chamado.
         */
        /*
        public CursoTeste(string nome, double cargaHoraria, string publico, double valor)
        {
            _nome = nome;
            _cargaHoraria = cargaHoraria;
            _publico = publico;
            _valor = valor;
        }
        */
        public CursoTeste()
        {
            _nome = "Teste de Software";
            _cargaHoraria = 80.00;
            _publico = "Professor";
            _valor = 150.00;
        }

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
                Name = _nome,
                Publico = _publico,
                /**
                 * Usada a conversão de `double`, pois
                 * já que não há identificação de tipo,
                 * um simples número fica muito ambíguo.
                 * 
                 * Isto é, pode ser int, double, float, 
                 * então temos que identificar o tipo 
                 * específico.
                 */
                CargaHoraria = _cargaHoraria,
                Valor = _valor
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CursoNomeVazio(string nome)
        {
            /*
             `string.Empty` é uma alternativa para passar as aspas vazias também.
             É uma forma mais limpa na sintaxe, visto que as aspas vazias pode
             acabar ficando confusa em certos casos.
             */

            // Act e Assert ao mesmo tempo.
            /**
             * Esse comando vai ficar apenas sinalizando as exceções que
             * forem do tipo `ArgumentException`.
             */
            /**
             * As arrow function do C# é igualzin às funções anônimas do JavaScript
             * (iguais mesma).
             */
            Assert.Throws<ArgumentException>(
                () => new Curso(nome, _cargaHoraria, _publico, _valor)
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CursoCargaHorariaInvalida(int cargaHoraria)
        {
            /**
             * É também possível verificar a mensagem de erro devolvida
             * em cada exception. Com isso, por exemplo, estou obrigando
             * o programador a colocar a mensagem correta que eu quero.
             */
            Assert
                .Throws<ArgumentException>(() => new Curso(_nome, cargaHoraria, _publico, _valor))
                /**
                 * `ComMensagem` recebe como primeiro parãmetro a ligação com a classe
                 * de exception, mas já que ele está usando o método encadeado, então o parâmetro
                 * que é a classe pode até ser destacado, já que já é pega por baixo dos panos 
                 * por causa do encadeamento.
                 */
                .ComMensagem("Valor inválido para Carga Horária!");
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
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Preenchimento do nome do curso é obrigatório!");
            }
            if (cargaHoraria <= 0)
            {
                throw new ArgumentException("Valor inválido para Carga Horária!");
            }

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