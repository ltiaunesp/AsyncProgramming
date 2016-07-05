using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncProgramminExample
{
    /// <summary>
    /// Classe para realizar contagem de maneira síncrona e assíncrona
    /// </summary>
    class Counter
    {
        /// <summary>
        /// Propriedade do tipo int para realizar a contagem
        /// </summary>
        public int Cont { get; set; }

        /// <summary>
        /// Evento que indica o término da contagem
        /// </summary>
        public event EventHandler TarefaTerminada;

        /// <summary>
        /// Classe responsável por indicar o progresso da contagem
        /// </summary>
        private Progress<int> progress;

        /// <summary>
        /// Estado da tarefa, true para terminada e false para não terminada
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Construtor da classe Counter, instancia um objeto do tipo Progress
        /// </summary>
        /// <param name="handler">(OPCIONAL!) Função que será executada quando o progresso for alterado</param>
        public Counter(Action<int> handler = null)
        {
            progress = new Progress<int>(handler); //Instancia um objeto do tipo Progress e passa como parametro a função que será executada quando uma alteração no progresso for feita
        }

        /// <summary>
        /// Função que será chamada quando a contagem terminar
        /// </summary>
        /// <param name="e">Dados do evento</param>
        protected virtual void OnTarefaTerminada(EventArgs e)
        {
            var args = e is TarefaEventArgs ? (TarefaEventArgs)e : null; // Verifica se o parâmetro é do tipo TarefaEventArgs, caso seja, é realizado um cast e ele é atribuido a variavel args, senão, é atribuido null a variavel args
            State = true; //Altera o estado do contador
            if (args != null) //Verifica se existem dados sobre o evento gerado
            {
                var button = args.sender is Button ? (Button)args.sender : null; //Caso seja do tipo TarefaEventArgs, o parametro poderá conter um Objeto (sender) e se o sender for do tipo Button, ele sofrerá um cast para ser do tipo botão e em seguida atribuido a variavel button, caso contrário a variável button será null;
                if (TarefaTerminada != null && button != null) //Se existir uma função atribuída ao delegate que representa o evento e a variavel button nao for nula a linha abaixo será executada
                    button.BeginInvoke(TarefaTerminada,this,e); //O botão enviado por parametro através dos dados do evento, possui uma função BeginInvoke, a qual recebe por parâmetro uma função a ser executada na mesma Thread que criou o botão, podendo assim realizar mudanças nas propriedades do mesmo;
            }
        }

        /// <summary>
        /// Função que retorna uma Task que executa a contagem em uma Thread separada, dessa forma, a execução dela é assíncrona
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>Task que executa a contagem em outra Thread</returns>
        public Task StartCountingAsync(object sender = null)
        {
            /** EXEMPLO DE FUNÇÃO ANÔNIMA
             * Funções desse tipo:
             *      () => 
             *      {
             *          //Stuff Here
             *      }
             * São chamadas Lambda expressions, as quais podem também ser chamadas de funções anônimas, não possuindo um nome e podendo ser passada como
             * um objeto de algum delegate. A seguinte função 'void FazAlgumaCoisa(Action funcParam)' pode receber uma função anônima como paramêtro, como no exemplo abaixo:
             *      FazAlgumaCoisa( () => 
             *      {
             *          código...
             *          código...
             *      });
             */
            return Task.Run(async () => // Pode parecer estranho o uso do termo async na declaração da função, mas ela é utilizada APENAS para que seja possível a utilização do termo await dentro da função!!!
            {
                TarefaEventArgs e = new TarefaEventArgs() { sender = sender is Button ? sender : null }; //Cria um novo dado sobre o evento que será gerado no término da função
                State = false; //Altera o estado da váriavel State, o qual desativa os botões para que se inicie novas contagens
                var progress = this.progress is IProgress<int> ? (IProgress<int>)this.progress : null; //Caso a váriavel progress seja do tipo IProgress ou covariante da mesma, ela sofrerá cast e será atribuida na váriavel progress, senão null será atribuido
                Random temp = new Random(); //Instancia um novo objeto do tipo Random para que seja possível gerar numeros randomicos
                for (Cont = 0; Cont < 100; Cont++) //Contagem de 0 a 99
                {
                    await Task.Delay(temp.Next(10, 100) * 1); //Interrompe a contagem por um valor aleatorio de 0,01 segundos até no máximo 0,1 segundos, para que a alteração do progresso na label seja visível
                    progress?.Report(Cont); //Se progress não for nulo, irá executar a função report, a qual executa em um Thread responsável por alterações GUI a função passada por parâmetro (caso tenha sido passado) no construtor da classe Counter
                }
                OnTarefaTerminada(e); //Chama a função que realizará o evento de término da contagem
            }); //Inicia a Task através do método Run, o qual retorna um objeto Task que representa a função que está sendo executada
        }
    }

    /// <summary>
    /// Representa uma classe que contém dados de eventos de Tarefa
    /// </summary>
    class TarefaEventArgs : EventArgs
    {
        public object sender { get; set; }
    }
}
