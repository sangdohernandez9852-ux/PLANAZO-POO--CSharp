using PlanazoApp.Excepciones;

namespace PlanazoApp.Modelos;

public abstract class Evento
{
    public int Id { get; }
    public string Nombre { get; }
    public DateTime Fecha { get; }
    public decimal PrecioBase { get; }
    public int CapacidadMaxima { get; }
    public int CuposDisponibles { get; private set; }

    protected Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del evento es obligatorio.", nameof(nombre));

        if (precioBase < 0)
            throw new ArgumentOutOfRangeException(nameof(precioBase), "El precio base no puede ser negativo.");

        if (capacidadMaxima <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacidadMaxima), "La capacidad debe ser mayor que cero.");

        Id = id;
        Nombre = nombre.Trim();
        Fecha = fecha;
        PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima;
        CuposDisponibles = capacidadMaxima;
    }

    public abstract string Tipo { get; }

    public abstract decimal CalcularPrecioFinal();

    public void ReservarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new CantidadInvalidaException(cantidad);

        if (cantidad > CuposDisponibles)
            throw new CupoInsuficienteException(cantidad, CuposDisponibles);

        CuposDisponibles -= cantidad;
    }

    public void LiberarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new CantidadInvalidaException(cantidad);

        if (CuposDisponibles + cantidad > CapacidadMaxima)
            throw new InvalidOperationException("No se pueden liberar más cupos que la capacidad máxima del evento.");

        CuposDisponibles += cantidad;
    }

    protected decimal Redondear(decimal valor)
    {
        return decimal.Round(valor, 2, MidpointRounding.AwayFromZero);
    }

    public override string ToString()
    {
        return $"{Tipo} #{Id}: {Nombre}";
    }
}
