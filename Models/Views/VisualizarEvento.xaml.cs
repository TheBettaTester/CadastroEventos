using CadastroEventos.Models;

namespace CadastroEventos.Views;

public partial class VisualizarEvento : ContentPage
{
    public VisualizarEvento(Evento evento)
    {
        InitializeComponent();

        BindingContext = evento;
    }

    private async void Voltar_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
