using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGuardaData
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CarregarInformacoes();
        }

        private void CarregarInformacoes()
        {
            string sql = "SELECT * FROM informacoes";
            var lista = ((App)Application.Current).Conexao.Query<Model>(sql);
            listView.ItemsSource = lista;
        }

        private void ButtonAdicionar_Clicked(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO informacoes (info) VALUES ('{DateTime.Now:dd-MM-yyyy HH:mm:ss}')";
            ((App)Application.Current).Conexao.Execute(sql);
            DisplayAlert("SUCESSO", "Item inserido", "OK");
            CarregarInformacoes();
        }

        private async void ButtonApagarTudo_Clicked(object sender, EventArgs e)
        {
            var confirmacao = await DisplayAlert("Confirmação",
                                        "tem Certeza que vai deletar a porra toda?",
                                        "SIM",
                                        "NÃO"); 

            if(confirmacao == true)
            {
                string sql = "DELETE FROM informacoes";
                ((App)Application.Current).Conexao.Execute(sql);
                await DisplayAlert("SUCESSO!!", "Informações Deletadas", "OK");
                CarregarInformacoes();
            }
            else
            {
                await DisplayAlert("Ops", "Houve um erro", "OK");
            }            
        }

        private void MenuItemAtualizar_Clicked(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            var model = (Model)mi.CommandParameter;

            var sql = $"UPDATE informacoes SET info = '{DateTime.Now}' WHERE id = {model.ID}";
            ((App)Application.Current).Conexao.Execute(sql);

            DisplayAlert("SUCESSO!", "Informação Atualizada", "OK");
            CarregarInformacoes();
        }

        private async void MenuItemDeletar_Clicked(object sender, EventArgs e)
        {
            var confirmacao = await DisplayAlert("Confirmação", "Tem certeza?", "SIM", "Nope");
            if (confirmacao == true)
            {
                var mi = (MenuItem)sender;
                var model = (Model)mi.CommandParameter;

                var sql = $"DELETE FROM informacoes WHERE id = {model.ID}";
                ((App)Application.Current).Conexao.Execute(sql);
                await DisplayAlert("SUCESSO!", "Informação Deleteda", "OK");
            }
            else
            {
                await DisplayAlert("Ops", "Houve um erro", "OK");
            }
            CarregarInformacoes();            
        }
    }
}
