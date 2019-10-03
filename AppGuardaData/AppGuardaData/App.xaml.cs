using PCLExt.FileStorage.Folders;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Lembre-se que estamos utilizando dois pacotes Nuget:
// O primeiro é o sqlite-net-pcl para manipularmos os banco de dados SQLite
// O segundo é o PCLExt.FileStorage para manipularmos arquivos nos devices

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppGuardaData
{
    public partial class App : Application
    {        
        //primeiro vamos criar uma propriedade que vai carregar a "conexão" com o banco de dados
        //Lembrando: não uma conexão com um servidor... é link com nosso arquivo que esta no Device.
        public SQLite.SQLiteConnection Conexao { get; private set; } // o set é privado para não corremos o risco de outras Classes mudarem nossa "conexão" com o banco

        public App()
        {
            //Vamos Localizar ou criar nosso arquivo de banco de dados
            //
            var pasta = new LocalRootFolder();

            //crio o arquivo do banco (se ele nao existir)
            //se ele existir, abro ele
            var arquivo = pasta.CreateFile("banco.db", PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);

            //faço a "conexao"
            Conexao = new SQLite.SQLiteConnection(arquivo.Path);

            //criar a tabela, se ela nao existir
            Conexao.Execute("CREATE TABLE IF NOT EXISTS informacoes (id INTEGER PRIMARY KEY AUTOINCREMENT, info TEXT)");

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
