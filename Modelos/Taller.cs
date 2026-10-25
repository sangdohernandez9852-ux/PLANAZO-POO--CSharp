namespace PlanazoApp.Modelos;

public sealed class Taller : Evento
{
    public Taller(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override string Tipo => "Taller";

    public override decimal CalcularPrecioFinal()
    {
        return Redondear(PrecioBase * 0.90m);
    }
}
