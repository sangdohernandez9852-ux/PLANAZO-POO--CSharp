using PlanazoApp.Excepciones;

namespace PlanazoApp.Modelos;

public sealed class Reserva
{
    public int Id { get; }
    public Usuario Usuario { get; }
    public Evento Evento { get; }
    public int CantidadEntradas { get; }
    public decimal ValorTotal { get; }
    public EstadoReserva Estado { get; private set; }
    public DateTime FechaReserva { get; }

    public Reserva(int id, Usuario usuario, Evento evento, int cantidadEntradas)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");

        if (usuario is null)
            throw new ArgumentNullException(nameof(usuario));

        if (evento is null)
            throw new ArgumentNullException(nameof(evento));

        if (cantidadEntradas <= 0)
            throw new CantidadInvalidaException(cantidadEntradas);

        if (cantidadEntradas > evento.CuposDisponibles)
            throw new CupoInsuficienteException(cantidadEntradas, evento.CuposDisponibles);

        Id = id;
        Usuario = usuario;
        Evento = evento;
        CantidadEntradas = cantidadEntradas;
        ValorTotal = decimal.Round(evento.CalcularPrecioFinal() * cantidadEntradas, 2, MidpointRounding.AwayFromZero);
        Estado = EstadoReserva.Activa;
        FechaReserva = DateTime.Now;
    }

    public string NombrePersona => Usuario.Nombre;

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new ReservaYaCanceladaException(Id);

        Evento.LiberarCupos(CantidadEntradas);
        Estado = EstadoReserva.Cancelada;
    }
}
