
using CadastroEventos.Models;

namespace CadastroEventos.Views;

public partial class Inicio : ContentPage
{
    public Inicio()
    {
        InitializeComponent();
    }

    private async void CadastrarEvento_Clicked(
        object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReservaEvento());
    }

    private async void MeusEventos_Clicked(
        object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MeusEventos());
    }
}
