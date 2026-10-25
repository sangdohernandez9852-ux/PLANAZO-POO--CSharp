namespace PlanazoApp.Excepciones;

public sealed class EventoNoEncontradoException : PlanazoException
{
    public EventoNoEncontradoException(int id)
        : base($"No existe un evento con el identificador {id}.")
    {
    }
}

public sealed class UsuarioNoEncontradoException : PlanazoException
{
    public UsuarioNoEncontradoException(int id)
        : base($"No existe un usuario con el identificador {id}.")
    {
    }
}

public sealed class ReservaNoEncontradaException : PlanazoException
{
    public ReservaNoEncontradaException(int id)
        : base($"No existe una reserva con el identificador {id}.")
    {
    }
}

public sealed class CupoInsuficienteException : PlanazoException
{
    public CupoInsuficienteException(int solicitadas, int disponibles)
        : base($"No hay cupos suficientes. Solicitó {solicitadas} y solo hay {disponibles} disponibles.")
    {
    }
}

public sealed class CantidadInvalidaException : PlanazoException
{
    public CantidadInvalidaException(int cantidad)
        : base($"La cantidad de entradas debe ser mayor que cero. Valor recibido: {cantidad}.")
    {
    }
}

public sealed class ReservaYaCanceladaException : PlanazoException
{
    public ReservaYaCanceladaException(int id)
        : base($"La reserva #{id} ya fue cancelada anteriormente.")
    {
    }
}

public sealed class CorreoDuplicadoException : PlanazoException
{
    public CorreoDuplicadoException(string correo)
        : base($"Ya existe un usuario registrado con el correo {correo}.")
    {
    }
}
