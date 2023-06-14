using CursosOnline.Test._Utils;
using CursosOnline.Test._Builders;
using CursosOnline.Domain;

namespace CursosOnline.Test
{
    public class CursoTeste
    {
        private string? _nome;
        private double _cargaHoraria;
        private string? _publico;
        private double _valor;
        private string? _descricao;
        private double _nota;

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
        /* Agora tudo isso é desnecessário, já que o Builer faz o papel.

        public CursoTeste()
        {
            _nome = "Teste de Software";
            _cargaHoraria = 80.00;
            _descricao = "Aula essencial para usufruir de testes";
            _publico = "Professor";
            _valor = 150.00;
            _nota = 8;
        }
        */

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
                Name = "Teste de Software",
                Publico = "Universitários",
                Descricao = "Curso para iniciantes em Testes",
                /**
                 * Usada a convers�o de `double`, pois
                 * j� que n�o h� identifica��o de tipo,
                 * um simples n�mero fica muito amb�guo.
                 *
                 * Isto �, pode ser int, double, float,
                 * ent�o temos que identificar o tipo
                 * espec�fico.
                 */
                CargaHoraria = 80.0,
                Valor = 1800.50,
                Nota = 8.0
            };

            Curso novoCurso = new Curso(curso.Name, curso.Descricao, curso.CargaHoraria, curso.Publico, curso.Valor, curso.Nota);

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
                () => CursoBuilder.Novo().ComNome(nome).Criar()
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
                .Throws<ArgumentException>(
                    () => CursoBuilder.Novo().ComCargaHoraria(cargaHoraria).Criar()
                )
                /**
                 * `ComMensagem` recebe como primeiro par�metro a liga��o com a classe
                 * de exception, mas j� que ele est� usando o m�todo encadeado, ent�o o par�metro
                 * que � a classe pode at� ser destacado, j� que j� � pega por baixo dos panos
                 * por causa do encadeamento.
                 */
                .ComMensagem("Valor inv�lido para Carga Hor�ria!");
        }

        [Theory]
        [InlineData(-1)]
        public void CursoNotaInvalida(int nota)
        {
            /**
             * � tamb�m poss�vel verificar a mensagem de erro devolvida
             * em cada exception. Com isso, por exemplo, estou obrigando
             * o programador a colocar a mensagem correta que eu quero.
             */
            Assert
                .Throws<ArgumentException>(
                    () => CursoBuilder.Novo().ComNota(nota).Criar()
                )
                .ComMensagem("Nota Inválida!");
        }
    }
}