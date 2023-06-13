/*
Esse é um padrão de projeto, para evitar exatamente os problemas
relacionados com a inserção de novas propriedades em projetos que já
são grandes ou muito extensos.

Com isso, usando o design pattern Builder, precisamos apenas mudar uma
vez para usar em todo o resto.
*/
namespace CursosOnline.Test._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Banco de Dados";
        private string _descricao = "SQL para pessoas iniciantes no assunto.";
        private double _cargaHoraria = 80;
        private string _publico = "Universitário";
        private double _valor = 150.50;
        private double _valorDesconto = 100;
        private double _nota = 8;

        /*
        Meio que seguindo uma lógica de Singleton, essa função vai reservar
        um espaço na memória para esse formato.

        Com isso, temos que uma nova instância vai retornar exatamente a
        estrutura que precisamos que seja retornada.
        */
        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Criar()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publico, _valor, _nota);
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;

            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;

            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;

            return this;
        }

        public CursoBuilder ComPublico(string publico)
        {
            _publico = publico;

            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;

            return this;
        }

        public CursoBuilder ComValorDesconto(double valorDesconto)
        {
            _valorDesconto = valorDesconto;

            return this;
        }

        public CursoBuilder ComNota(double nota)
        {
            _nota = nota;

            return this;
        }
    }
}