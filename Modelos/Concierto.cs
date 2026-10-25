namespace PlanazoApp.Modelos;

public sealed class Concierto : Evento
{
    public Concierto(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override string Tipo => "Concierto";

    public override decimal CalcularPrecioFinal()
    {
        return Redondear(PrecioBase * 1.15m);
    }
}
