
namespace CadastroEventos.Models;

public class Evento
{
    public string Nome { get; set; } = string.Empty;

    public DateTime DataInicio { get; set; } = DateTime.Today;

    public TimeSpan HorarioInicio { get; set; } = new TimeSpan(9, 0, 0);

    public DateTime DataTermino { get; set; } = DateTime.Today.AddDays(1);

    public TimeSpan HorarioTermino { get; set; } = new TimeSpan(10, 0, 0);

    public int NumeroParticipantes { get; set; }

    public string Local { get; set; } = string.Empty;

    public decimal CustoPorParticipante { get; set; }

    // Calcula a duração considerando as datas e os horários.
    public TimeSpan Duracao
    {
        get
        {
            DateTime inicio = DataInicio.Date.Add(HorarioInicio);
            DateTime termino = DataTermino.Date.Add(HorarioTermino);

            return termino - inicio;
        }
    }

    // Exibe a duração total sem reiniciar a contagem a cada 24 horas.
    public string DuracaoFormatada
    {
        get
        {
            return $"{(int)Duracao.TotalHours:D2}h {Duracao.Minutes:D2}min";
        }
    }

    // Calcula o custo total do evento.
    public decimal CustoTotal
    {
        get
        {
            return NumeroParticipantes * CustoPorParticipante;
        }
    }
}
