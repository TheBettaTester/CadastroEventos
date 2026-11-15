
using CadastroEventos.Models;
using CadastroEventos.Services;

namespace CadastroEventos.Views;

public partial class MeusEventos : ContentPage
{
    public MeusEventos()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ListaEventos.ItemsSource = EventoService.Eventos
            .OrderBy(evento => evento.DataInicio.Date.Add(evento.HorarioInicio))
            .ToList();
    }

    private async void EventoSelecionado(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Evento evento)
        {
            ((CollectionView)sender!).SelectedItem = null;

            await Navigation.PushAsync(new VisualizarEvento(evento));
        }
    }

    private async void Voltar_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void CadastrarOutro_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
        await Navigation.PushAsync(new ReservaEvento());
    }

    private async void Inicio_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
