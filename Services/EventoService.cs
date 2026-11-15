
using CadastroEventos.Models;

namespace CadastroEventos.Services;

public static class EventoService
{
    public static List<Evento> Eventos { get; private set; } = new();

    public static void Adicionar(Evento evento)
    {
        Eventos.Add(evento);
    }
}
