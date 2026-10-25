namespace PlanazoApp.Modelos;

public sealed class EventoDeportivo : Evento
{
    public EventoDeportivo(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override string Tipo => "Deportivo";

    public override decimal CalcularPrecioFinal()
    {
        return Redondear(PrecioBase * 1.10m);
    }
}
