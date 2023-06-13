using CursosOnline.Test._Util;

namespace CursosOnline.Test
{
    public class CursoTeste
    {
        private string _nome;
        private double _cargaHoraria;
        private string _publico;
        private double _valor;
        private string _descricao;

        /**
         * Numa classe normal, o construtor sempre � chamado apenas uma vez, no momento
         * que instancia a classe.
         *
         * Na classe de teste, o construtor � chamado em todo in�cio da execu��o
         * de testes. Ent�o, a cada teste que for executado, o construtor � chamado.
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
            _descricao = "Aula essencial para usufruir de testes";
            _publico = "Professor";
            _valor = 150.00;
        }

        /**
         * Criar Curso
         *
         * Eu, como administrador, preciso criar e editar os cursos para as novas matr�culas.
         *
         * 1. Criar curso com nome, carga hor�ria, p�blico-alvo e valor do curso;
         * 2. Op��es de p�blico-alvo: secundarista, universit�ria, profissional e empreendedor.
         * 3. Todos os campos s�o de preenchimento obrigat�rio.
         * 14. Cada curso PODE ter uma descrição
         */

        [Fact]
        public void CriarCurso()
        {
            /**
             * Isso daqui � um objeto an�nimo, que em grosso modo � a tradu��o
             * para c�digo de um JSON.
             *
             * Por isso que no momento de compara��o colocamos a fun��o de
             * `ToExpectedObject`, para que a compara��o seja poss�vel
             * e assim possamos ter objeto com objeto para compara��o.
             */
            var curso = new
            {
                Name = _nome,
                Publico = _publico,
                Descricao = _descricao,
                /**
                 * Usada a convers�o de `double`, pois
                 * j� que n�o h� identifica��o de tipo,
                 * um simples n�mero fica muito amb�guo.
                 *
                 * Isto �, pode ser int, double, float,
                 * ent�o temos que identificar o tipo
                 * espec�fico.
                 */
                CargaHoraria = _cargaHoraria,
                Valor = _valor
            };

            Curso novoCurso = new Curso(curso.Name, curso.Descricao, curso.CargaHoraria, curso.Publico, curso.Valor);

            /**
             * Feito assim pois as boas pr�ticas recomendam ter apenas um Assert por
             * fun��o de teste. No caso, j� que queremos comparar v�rias propriedades,
             * ter�amos de usar v�rios e v�rios Assert, o que n�o � legal.
             *
             * Com isso, usamos essa lib para fazer com que haja apenas uma compara��o,
             * fazendo com que seja poss�vel testar, al�m de seguir o recomendado.
             */
            curso.ToExpectedObject().ShouldMatch(novoCurso);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CursoNomeVazio(string nome)
        {
            /*
             `string.Empty` � uma alternativa para passar as aspas vazias tamb�m.
             � uma forma mais limpa na sintaxe, visto que as aspas vazias pode
             acabar ficando confusa em certos casos.
             */

            // Act e Assert ao mesmo tempo.
            /**
             * Esse comando vai ficar apenas sinalizando as exce��es que
             * forem do tipo `ArgumentException`.
             */
            /**
             * As arrow function do C# � igualzin �s fun��es an�nimas do JavaScript
             * (iguais mesma).
             */
            Assert.Throws<ArgumentException>(
                () => new Curso(nome, _descricao, _cargaHoraria, _publico, _valor)
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CursoCargaHorariaInvalida(int cargaHoraria)
        {
            /**
             * � tamb�m poss�vel verificar a mensagem de erro devolvida
             * em cada exception. Com isso, por exemplo, estou obrigando
             * o programador a colocar a mensagem correta que eu quero.
             */
            Assert
                .Throws<ArgumentException>(() => new Curso(_nome, _descricao, cargaHoraria, _publico, _valor))
                /**
                 * `ComMensagem` recebe como primeiro par�metro a liga��o com a classe
                 * de exception, mas j� que ele est� usando o m�todo encadeado, ent�o o par�metro
                 * que � a classe pode at� ser destacado, j� que j� � pega por baixo dos panos
                 * por causa do encadeamento.
                 */
                .ComMensagem("Valor inv�lido para Carga Hor�ria!");
        }
    }

    public class Curso
    {
        // Estes s�o os "campos", que possuem representa��o apenas na classe
        private string name;
        private string descricao;
        private double cargaHoraria;
        private string publico;
        private double valor;
        private double valorDesconto;

        public Curso(string name, string descricao, double cargaHoraria, string publico, double valor)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Preenchimento do nome do curso � obrigat�rio!");
            }
            if (cargaHoraria <= 0)
            {
                throw new ArgumentException("Valor inv�lido para Carga Hor�ria!");
            }

            this.Name = name;
            this.CargaHoraria = cargaHoraria;
            this.Descricao = descricao;
            this.Publico = publico;
            this.Valor = valor;
            this.valorDesconto = 10;
        }

        // E aqui temos as propriedades, que servem mais para a representa��o e manipula��o de valores.
        public string Name { get => name; set => name = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        /**
         * Nesse caso, quando temos tanto o get quanto set, podemos dar valor � vari�vel e tamb�m
         * ler o que est� dentro dela.
         */
        public double CargaHoraria { get => cargaHoraria; set => cargaHoraria = value; }
        public string Publico { get => publico; set => publico = value; }
        /**
         * A propriedade pode tamb�m ter apenas um ou outro. Ou seja, apenas ser lida ou apenas
         * ganhar valor.
         */
        public double Valor { get => valor; set => valor = value; }

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