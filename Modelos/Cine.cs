namespace PlanazoApp.Modelos;

public sealed class Cine : Evento
{
    public Cine(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override string Tipo => "Cine";

    public override decimal CalcularPrecioFinal()
    {
        return PrecioBase;
    }
}
