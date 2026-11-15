
namespace CadastroEventos.Views;

public partial class Sucesso : ContentPage
{
    public Sucesso()
    {
        InitializeComponent();
    }

    private async void Inicio_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void MeusEventos_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MeusEventos());
    }
}
