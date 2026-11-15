
using CadastroEventos.Models;
using System.Globalization;
using System.Linq;

namespace CadastroEventos.Views;

public partial class ReservaEvento : ContentPage
{
    private bool formatandoCusto = false;

    private readonly CultureInfo culturaBrasileira =
        new CultureInfo("pt-BR");

    public ReservaEvento()
    {
        InitializeComponent();

        DataInicioPicker.MinimumDate = DateTime.Today;
        DataTerminoPicker.MinimumDate = DateTime.Today;

        BindingContext = new Evento();
    }

    private void DataInicio_DateSelected(object? sender, DateChangedEventArgs e)
    {
        DateTime dataInicioSelecionada =
            e.NewDate.GetValueOrDefault(DateTime.Today).Date;

        // O término pode acontecer no mesmo dia do início.
        DataTerminoPicker.MinimumDate = dataInicioSelecionada;

        DateTime dataTerminoAtual =
            DataTerminoPicker.Date.GetValueOrDefault(DateTime.Today).Date;

        if (dataTerminoAtual < dataInicioSelecionada)
        {
            DataTerminoPicker.Date = dataInicioSelecionada;
        }
    }

    private void CustoEntry_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (formatandoCusto)
            return;

        formatandoCusto = true;

        try
        {
            string textoDigitado = e.NewTextValue ?? "";

            string numeros = new string(
                textoDigitado.Where(char.IsDigit).ToArray()
            );

            Evento evento = (Evento)BindingContext;

            if (string.IsNullOrEmpty(numeros))
            {
                evento.CustoPorParticipante = 0;
                CustoEntry.Text = "";
            }
            else if (decimal.TryParse(
                numeros,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out decimal centavos))
            {
                decimal valor = centavos / 100m;

                evento.CustoPorParticipante = valor;

                CustoEntry.Text = valor.ToString(
                    "C2",
                    culturaBrasileira
                );
            }

            CustoEntry.CursorPosition = CustoEntry.Text?.Length ?? 0;
            CustoEntry.SelectionLength = 0;
        }
        finally
        {
            formatandoCusto = false;
        }
    }

    private async void Continuar_Clicked(object? sender, EventArgs e)
    {
        Evento evento = (Evento)BindingContext;

        if (string.IsNullOrWhiteSpace(evento.Nome))
        {
            await DisplayAlertAsync("Atenção", "Informe o nome do evento.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(evento.Local))
        {
            await DisplayAlertAsync("Atenção", "Informe o local do evento.", "OK");
            return;
        }

        if (evento.NumeroParticipantes <= 0)
        {
            await DisplayAlertAsync("Atenção", "Informe o número de participantes.", "OK");
            return;
        }

        if (evento.CustoPorParticipante <= 0)
        {
            await DisplayAlertAsync("Atenção", "Informe um custo válido por participante.", "OK");
            return;
        }

        DateTime inicio = evento.DataInicio.Date.Add(evento.HorarioInicio);
        DateTime termino = evento.DataTermino.Date.Add(evento.HorarioTermino);

        if (evento.DataInicio.Date < DateTime.Today)
        {
            await DisplayAlertAsync(
                "Atenção",
                "A data de início não pode ser anterior a hoje.",
                "OK"
            );
            return;
        }

        if (inicio <= DateTime.Now)
        {
            await DisplayAlertAsync(
                "Atenção",
                "O horário de início precisa ser posterior ao horário atual.",
                "OK"
            );
            return;
        }

        if (evento.DataTermino.Date < evento.DataInicio.Date)
        {
            await DisplayAlertAsync(
                "Atenção",
                "A data de término não pode ser anterior à data de início.",
                "OK"
            );
            return;
        }

        if (termino <= inicio)
        {
            await DisplayAlertAsync(
                "Atenção",
                "O horário de término precisa ser posterior ao horário de início.",
                "OK"
            );
            return;
        }

        await Navigation.PushAsync(new Confirmacao(evento));
    }
}
