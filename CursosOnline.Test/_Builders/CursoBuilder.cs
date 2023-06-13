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
        private string _name = "Banco de Dados";
        private string _descricao = "SQL para pessoas iniciantes no assunto.";
        private double _cargaHoraria = 80;
        private string _publico = "Universitário";
        private double _valor = 150.50;
        private double _valorDesconto = 100;

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
          return new Curso(_name, _descricao, _cargaHoraria, _publico, _valor);
        }
    }
}