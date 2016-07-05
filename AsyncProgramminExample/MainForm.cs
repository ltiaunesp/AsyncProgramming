using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AsyncProgramminExample
{
    /** EXEMPLO DE PROGRAMAÇÃO ASSÍNCRONA, POR MATHEUS C. SOLHA
     *  
     *  Objetivo:
     *  Simples aplicação desenvolvida com janelas que tem como objetivo mostrar o funcionamento de tarefas rodando sincronamente e assícronamente.
     *  
     *  Público alvo:
     *  Novos integrantes do Laboratório de Tecnologia da Informação Aplicada, LTIA
     */

    /// <summary>
    /// Classe que representa o FormPrincipal
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Objeto do tipo Counter para realizar a contagem que será apresentada na primeira Label
        /// </summary>
        Counter counter1;

        /// <summary>
        /// Objeto do tipo Counter para realizar a contagem que será apresentada na segunda Label
        /// </summary>
        Counter counter2;

        /// <summary>
        /// Classe onde se encontram as propriedades que serviram como fonte de dados para componentes do Form
        /// </summary>
        ViewModel vm;

        /// <summary>
        /// Construtor do MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();            
            vm = new ViewModel();   //Instanciação da classe ViewModel
            counter1 = new Counter(ChangeLabel1); //Instanciação da classe Counter, a qual tem como parâmetro um delegate Action o qual será responsável por atualizar o dado da Label
            counter2 = new Counter(ChangeLabel2);
            counter1.TarefaTerminada += Counter1_TarefaTerminada; //Adicionando a função ao evento TarefaTerminada do counter1, o qual será executado quando a função StartCounting ou StartCountingAsync forem finalizadas
            counter2.TarefaTerminada += Counter1_TarefaTerminada; //Adicionando a função ao evento TarefaTerminada do counter2...
            this.Icon = SystemIcons.WinLogo;
        }

        /// <summary>
        /// Função executada quando o evento TarefaTerminada for gerado
        /// </summary>
        /// <param name="sender">Objeto que gerou a execução do evento</param>
        /// <param name="e">Dados do evento</param>
        private void Counter1_TarefaTerminada(object sender, EventArgs e)
        {
            vm.Terminado = counter1.State ? counter2.State : false; //Altera o estado da propriedade Terminado da seguinte forma: Caso o estado do contador1 seja false, Terminado = false, caso seja verdadeiro: Terminado = estado do contador 2;
        }

        /// <summary>
        /// Função executada quando o evento de Click no botão Async for gerado
        /// </summary>
        /// <param name="sender">Objeto que gerou a execução do evento</param>
        /// <param name="e">Dados do evento</param>
        private void asyncButton_Click(object sender, EventArgs e)
        {
            vm.Terminado = false; //Altera o estado da propriedade Terminado para false para que o botão permaneça inativo enquanto os processos são executados
            counter1.StartCountingAsync(asyncButton); //Inicia a contagem do contador 1 
            counter2.StartCountingAsync(asyncButton);
        }

        /// <summary>
        /// Função para atualizar o dado da Label1
        /// </summary>
        /// <param name="i">Valor inteiro que será apresentado na label</param>
        private void ChangeLabel1(int i)
        {
            cont1Label.Text = i.ToString();
        }

        /// <summary>
        /// Função para atualizar o dado da Label2
        /// </summary>
        /// <param name="i">Valor inteiro que será apresentado na label</param>
        private void ChangeLabel2(int i)
        {
            cont2Label.Text = i.ToString();
        }

        /// <summary>
        /// Função executada quando o evento de Click no botão Sync for gerado
        /// </summary>
        /// <param name="sender">Objeto que gerou a execução do evento</param>
        /// <param name="e">Dados do evento</param>
        private async void syncButton_Click(object sender, EventArgs e)
        {
            cont1Label.ResetText();
            cont2Label.ResetText();
            vm.Terminado = false;
            await counter1.StartCountingAsync(syncButton);
            await counter2.StartCountingAsync(syncButton);
        }

        /// <summary>
        /// Função executada quando o Form termina de carregar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            asyncButton.DataBindings.Add("Enabled", vm, "Terminado"); //Determina que a propriedade Enabled do asyncButton e do sync serão dependentes da propriedade Terminado, pode-se alterar o estado do botão apenas alterando o estado da propriedade Terminado
            syncButton.DataBindings.Add("Enabled", vm, "Terminado");
        }
    }

    /// <summary>
    /// Classe que contém as propriedades que são interligadas aos componentes do form
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Variavel que indica se o processo de contagem foi terminado ou não
        /// </summary>
        private bool terminado = true;

        /// <summary>
        /// Encapsulamento da variável terminado
        /// </summary>
        public bool Terminado
        {            
            get { return terminado; } //Retorna o valor da variavel terminado
            set
            {
                if (terminado == value) //Se o valor da variavel terminado for igual ao valor solicitado para ser atribuido, a funçao set retorna sem atribuir nada
                    return;
                terminado = value; //Caso contrário, atribui o valor a variavel
                RaisePropertyChanged("Terminado"); //Gera o evento de propriedade alterada e passa por parametro o nome da propriedade que foi alterada
            }
        }

        /// <summary>
        /// Evento que será chamado quando uma propriedade for alterada
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Função que gera o evento
        /// </summary>
        /// <param name="propertyName">Nome da propriedade alterada</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
