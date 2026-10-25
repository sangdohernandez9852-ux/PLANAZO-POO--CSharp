namespace PlanazoApp.Servicios;

public sealed class ResumenReservas
{
    public int ReservasActivas { get; }
    public int EntradasReservadas { get; }
    public decimal ValorTotal { get; }
    public int ReservasCanceladas { get; }

    public ResumenReservas(int reservasActivas, int entradasReservadas, decimal valorTotal, int reservasCanceladas)
    {
        ReservasActivas = reservasActivas;
        EntradasReservadas = entradasReservadas;
        ValorTotal = valorTotal;
        ReservasCanceladas = reservasCanceladas;
    }
}
