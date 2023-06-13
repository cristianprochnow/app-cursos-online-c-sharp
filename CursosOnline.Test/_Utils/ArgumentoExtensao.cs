namespace CursosOnline.Test._Utils
{
    public static class ArgumentoExtensao
    {
        /**
         * Como o método `this`, então quer dizer que o parâmetro passado
         * do `ArgumentException` vai funcionar no contexto dessa classe
         * que está sendo criada.
         *
         * Isto é, tudo funcionará como se fosse a questão de herança, da
         * orientação a objetos. Então, a função `ComMensagem` começará
         * a fazer parte da classe genérica ArgumentException, já que chama
         * ela ligando diretamente na hora de chamar, encadeada.
         */
        public static void ComMensagem(this ArgumentException excecaoArgumento, string mensagem)
        {
            if (excecaoArgumento.Message == mensagem)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(true, "Esperava a mensagem "+mensagem);
            }
        }
    }
}
