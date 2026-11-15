
using CadastroEventos.Models;
using CadastroEventos.Services;

namespace CadastroEventos.Views;

public partial class Confirmacao : ContentPage
{
    private Evento evento;

    public Confirmacao(Evento eventoRecebido)
    {
        InitializeComponent();

        evento = eventoRecebido;

        BindingContext = evento;
    }

    private async void Confirmar_Clicked(object? sender, EventArgs e)
    {
        EventoService.Adicionar(evento);

        await Navigation.PushAsync(new Sucesso());
    }

    private async void Voltar_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
