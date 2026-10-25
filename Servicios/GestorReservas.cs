using PlanazoApp.Excepciones;
using PlanazoApp.Modelos;

namespace PlanazoApp.Servicios;

public sealed class GestorReservas
{
    private readonly List<Evento> _eventos;
    private readonly List<Reserva> _reservas;
    private readonly List<Usuario> _usuarios;
    private int _siguienteIdReserva;
    private int _siguienteIdUsuario;

    public GestorReservas()
    {
        _eventos = new List<Evento>();
        _reservas = new List<Reserva>();
        _usuarios = new List<Usuario>();
        _siguienteIdReserva = 1;
        _siguienteIdUsuario = 4;

        CargarDatosIniciales();
    }

    public IReadOnlyList<Evento> ObtenerEventos()
    {
        return _eventos.AsReadOnly();
    }

    public IReadOnlyList<Usuario> ObtenerUsuarios()
    {
        return _usuarios.AsReadOnly();
    }

    public IReadOnlyList<Reserva> ObtenerReservas()
    {
        return _reservas.AsReadOnly();
    }

    public Evento BuscarEvento(int id)
    {
        var evento = _eventos.FirstOrDefault(e => e.Id == id);

        if (evento is null)
            throw new EventoNoEncontradoException(id);

        return evento;
    }

    public Usuario BuscarUsuario(int id)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario is null)
            throw new UsuarioNoEncontradoException(id);

        return usuario;
    }

    public Reserva BuscarReserva(int id)
    {
        var reserva = _reservas.FirstOrDefault(r => r.Id == id);

        if (reserva is null)
            throw new ReservaNoEncontradaException(id);

        return reserva;
    }

    public Reserva CrearReserva(int usuarioId, int eventoId, int cantidadEntradas)
    {
        var usuario = BuscarUsuario(usuarioId);
        var evento = BuscarEvento(eventoId);

        if (cantidadEntradas <= 0)
            throw new CantidadInvalidaException(cantidadEntradas);

        if (cantidadEntradas > evento.CuposDisponibles)
            throw new CupoInsuficienteException(cantidadEntradas, evento.CuposDisponibles);

        var reserva = new Reserva(_siguienteIdReserva, usuario, evento, cantidadEntradas);

        evento.ReservarCupos(cantidadEntradas);
        _reservas.Add(reserva);
        _siguienteIdReserva++;

        return reserva;
    }

    public void CancelarReserva(int reservaId)
    {
        var reserva = BuscarReserva(reservaId);
        reserva.Cancelar();
    }

    public IReadOnlyList<Reserva> ObtenerReservasPorUsuario(int usuarioId)
    {
        var usuario = BuscarUsuario(usuarioId);

        return _reservas
            .Where(r => r.Usuario.Id == usuario.Id)
            .ToList()
            .AsReadOnly();
    }

    public ResumenReservas ObtenerResumen()
    {
        var activas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
        var canceladas = _reservas.Count(r => r.Estado == EstadoReserva.Cancelada);

        return new ResumenReservas(
            activas.Count,
            activas.Sum(r => r.CantidadEntradas),
            activas.Sum(r => r.ValorTotal),
            canceladas);
    }

    public Usuario RegistrarUsuario(string nombre, string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
            throw new ArgumentException("El correo es obligatorio.", nameof(correo));

        string correoLimpio = correo.Trim();

        if (_usuarios.Any(u => u.Correo.Equals(correoLimpio, StringComparison.OrdinalIgnoreCase)))
            throw new CorreoDuplicadoException(correoLimpio);

        var usuario = new Usuario(_siguienteIdUsuario, nombre, correoLimpio);

        _usuarios.Add(usuario);
        _siguienteIdUsuario++;

        return usuario;
    }

    private void CargarDatosIniciales()
    {
        _usuarios.Add(new Usuario(1, "Ana Gómez", "ana@planazo.com"));
        _usuarios.Add(new Usuario(2, "Carlos Pérez", "carlos@planazo.com"));
        _usuarios.Add(new Usuario(3, "Laura Rodríguez", "laura@planazo.com"));

        _eventos.Add(new Cine(1, "Interestelar", DateTime.Today.AddDays(5).AddHours(19), 18000m, 40));
        _eventos.Add(new Cine(2, "El Padrino", DateTime.Today.AddDays(12).AddHours(20), 20000m, 30));
        _eventos.Add(new Concierto(3, "Rock al Parque", DateTime.Today.AddDays(20).AddHours(18), 50000m, 100));
        _eventos.Add(new Concierto(4, "Noche de Jazz", DateTime.Today.AddDays(30).AddHours(20), 75000m, 60));
        _eventos.Add(new Taller(5, "Cerámica", DateTime.Today.AddDays(7).AddHours(15), 30000m, 15));
        _eventos.Add(new Taller(6, "Fotografía", DateTime.Today.AddDays(15).AddHours(10), 45000m, 20));
        _eventos.Add(new EventoDeportivo(7, "Clásico de Fútbol", DateTime.Today.AddDays(25).AddHours(17), 40000m, 80));
    }
}
