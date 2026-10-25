using System.Globalization;
using PlanazoApp.Excepciones;
using PlanazoApp.Modelos;
using PlanazoApp.Servicios;

namespace PlanazoApp.UI;

public sealed class MenuConsola
{
    private readonly GestorReservas _gestor;
    private readonly CultureInfo _cultura;

    public MenuConsola(GestorReservas gestor)
    {
        _gestor = gestor;
        _cultura = CultureInfo.GetCultureInfo("es-CO");
    }

    public void Ejecutar()
    {
        bool continuar = true;

        while (continuar)
        {
            MostrarMenu();

            try
            {
                int opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 0:
                        Console.WriteLine();
                        Console.WriteLine("Gracias por usar PLANAZO.");
                        continuar = false;
                        break;
                    case 1:
                        MostrarEventos();
                        break;
                    case 2:
                        ConsultarEvento();
                        break;
                    case 3:
                        ReservarEntradas();
                        break;
                    case 4:
                        MostrarReservas();
                        break;
                    case 5:
                        CancelarReserva();
                        break;
                    case 6:
                        MostrarResumen();
                        break;
                    case 7:
                        MostrarReservasPorUsuario();
                        break;
                    case 8:
                        RegistrarUsuario();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Opción inválida. Debe elegir un número del menú.");
                        break;
                }
            }
            catch (FinDeEntradaException)
            {
                continuar = false;
                continue;
            }
            catch (PlanazoException ex)
            {
                MostrarError(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError($"Ocurrió un error inesperado: {ex.Message}");
            }

            if (continuar && !Pausar())
                continuar = false;
        }
    }

    private static void MostrarMenu()
    {
        Console.WriteLine();
        Console.WriteLine("================================");
        Console.WriteLine("        PLANAZO - EVENTOS");
        Console.WriteLine("================================");
        Console.WriteLine();
        Console.WriteLine("1. Ver eventos disponibles");
        Console.WriteLine("2. Consultar evento");
        Console.WriteLine("3. Reservar entradas");
        Console.WriteLine("4. Ver reservas realizadas");
        Console.WriteLine("5. Cancelar una reserva");
        Console.WriteLine("6. Ver resumen de reservas");
        Console.WriteLine("7. Consultar reservas por usuario");
        Console.WriteLine("8. Registrar usuario");
        Console.WriteLine("0. Salir");
        Console.WriteLine();
    }

    private void MostrarEventos()
    {
        var eventos = _gestor.ObtenerEventos();

        Console.WriteLine();
        Console.WriteLine("EVENTOS DISPONIBLES");
        Console.WriteLine(new string('-', 100));
        Console.WriteLine($"{"ID",-4}{"TIPO",-13}{"EVENTO",-26}{"FECHA",-20}{"PRECIO FINAL",15}{"CUPOS",10}");
        Console.WriteLine(new string('-', 100));

        foreach (var evento in eventos)
        {
            string cupos = evento.CuposDisponibles == 0
                ? "AGOTADO"
                : $"{evento.CuposDisponibles}/{evento.CapacidadMaxima}";

            Console.WriteLine($"{evento.Id,-4}{evento.Tipo,-13}{evento.Nombre,-26}{evento.Fecha,-20:dd/MM/yyyy HH:mm}{FormatearMoneda(evento.CalcularPrecioFinal()),15}{cupos,10}");
        }

        Console.WriteLine(new string('-', 100));
    }

    private void ConsultarEvento()
    {
        int id = LeerEntero("Ingrese el ID del evento: ");
        var evento = _gestor.BuscarEvento(id);

        Console.WriteLine();
        Console.WriteLine($"ID: {evento.Id}");
        Console.WriteLine($"Tipo: {evento.Tipo}");
        Console.WriteLine($"Nombre: {evento.Nombre}");
        Console.WriteLine($"Fecha: {evento.Fecha:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Precio base: {FormatearMoneda(evento.PrecioBase)}");
        Console.WriteLine($"Precio final: {FormatearMoneda(evento.CalcularPrecioFinal())}");
        Console.WriteLine($"Capacidad máxima: {evento.CapacidadMaxima}");
        Console.WriteLine($"Cupos disponibles: {evento.CuposDisponibles}");
    }

    private void ReservarEntradas()
    {
        MostrarUsuarios();
        int usuarioId = LeerEntero("Ingrese el ID del usuario: ");
        var usuario = _gestor.BuscarUsuario(usuarioId);

        MostrarEventos();
        int eventoId = LeerEntero("Ingrese el ID del evento: ");
        var evento = _gestor.BuscarEvento(eventoId);

        Console.WriteLine();
        Console.WriteLine($"Evento seleccionado: {evento.Nombre}");
        Console.WriteLine($"Precio final por entrada: {FormatearMoneda(evento.CalcularPrecioFinal())}");
        Console.WriteLine($"Cupos disponibles: {evento.CuposDisponibles}");

        int cantidad = LeerEntero("Ingrese la cantidad de entradas: ");
        var reserva = _gestor.CrearReserva(usuarioId, eventoId, cantidad);

        Console.WriteLine();
        Console.WriteLine("RESERVA REGISTRADA");
        Console.WriteLine($"Reserva: #{reserva.Id}");
        Console.WriteLine($"Persona: {usuario.Nombre}");
        Console.WriteLine($"Evento: {reserva.Evento.Nombre}");
        Console.WriteLine($"Entradas: {reserva.CantidadEntradas}");
        Console.WriteLine($"Valor total: {FormatearMoneda(reserva.ValorTotal)}");
        Console.WriteLine($"Cupos restantes: {reserva.Evento.CuposDisponibles}");
    }

    private void MostrarReservas()
    {
        var reservas = _gestor.ObtenerReservas();

        Console.WriteLine();
        Console.WriteLine("RESERVAS REALIZADAS");

        if (reservas.Count == 0)
        {
            Console.WriteLine("No hay reservas registradas.");
            return;
        }

        MostrarTablaReservas(reservas);
    }

    private void CancelarReserva()
    {
        var reservas = _gestor.ObtenerReservas();

        if (reservas.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("No hay reservas registradas para cancelar.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("RESERVAS REGISTRADAS");
        MostrarTablaReservas(reservas);

        int id = LeerEntero("Ingrese el ID de la reserva a cancelar: ");
        var reserva = _gestor.BuscarReserva(id);
        string nombreEvento = reserva.Evento.Nombre;
        int cantidad = reserva.CantidadEntradas;

        _gestor.CancelarReserva(id);

        Console.WriteLine();
        Console.WriteLine($"Reserva #{id} cancelada correctamente.");
        Console.WriteLine($"Se devolvieron {cantidad} cupos al evento \"{nombreEvento}\".");
        Console.WriteLine($"Cupos disponibles ahora: {reserva.Evento.CuposDisponibles}");
    }

    private void MostrarResumen()
    {
        var resumen = _gestor.ObtenerResumen();

        Console.WriteLine();
        Console.WriteLine("RESUMEN DE RESERVAS");
        Console.WriteLine(new string('-', 45));
        Console.WriteLine($"Reservas activas: {resumen.ReservasActivas}");
        Console.WriteLine($"Entradas reservadas: {resumen.EntradasReservadas}");
        Console.WriteLine($"Valor total activo: {FormatearMoneda(resumen.ValorTotal)}");
        Console.WriteLine($"Reservas canceladas: {resumen.ReservasCanceladas}");
        Console.WriteLine(new string('-', 45));
    }

    private void MostrarReservasPorUsuario()
    {
        MostrarUsuarios();
        int usuarioId = LeerEntero("Ingrese el ID del usuario: ");
        var usuario = _gestor.BuscarUsuario(usuarioId);
        var reservas = _gestor.ObtenerReservasPorUsuario(usuarioId);

        Console.WriteLine();
        Console.WriteLine($"RESERVAS DE {usuario.Nombre.ToUpperInvariant()}");

        if (reservas.Count == 0)
        {
            Console.WriteLine("Este usuario no tiene reservas registradas.");
            return;
        }

        foreach (var reserva in reservas)
        {
            Console.WriteLine($"#{reserva.Id} - {reserva.Evento.Nombre} - {reserva.CantidadEntradas} entradas - {FormatearMoneda(reserva.ValorTotal)} - {reserva.Estado}");
        }
    }

    private void RegistrarUsuario()
    {
        Console.WriteLine();
        Console.WriteLine("REGISTRO DE USUARIO");

        string nombre = LeerTexto("Nombre completo: ");
        string correo = LeerTexto("Correo electrónico: ");

        var usuario = _gestor.RegistrarUsuario(nombre, correo);

        Console.WriteLine();
        Console.WriteLine($"Usuario registrado con el ID {usuario.Id}.");
        Console.WriteLine($"Nombre: {usuario.Nombre}");
        Console.WriteLine($"Correo: {usuario.Correo}");
    }

    private void MostrarUsuarios()
    {
        Console.WriteLine();
        Console.WriteLine("USUARIOS REGISTRADOS");
        Console.WriteLine(new string('-', 65));

        foreach (var usuario in _gestor.ObtenerUsuarios())
        {
            Console.WriteLine($"{usuario.Id,-4}{usuario.Nombre,-25}{usuario.Correo}");
        }

        Console.WriteLine(new string('-', 65));
    }

    private void MostrarTablaReservas(IReadOnlyList<Reserva> reservas)
    {
        Console.WriteLine(new string('-', 110));
        Console.WriteLine($"{"ID",-5}{"PERSONA",-22}{"EVENTO",-24}{"ENTRADAS",10}{"TOTAL",15} {"ESTADO",-12}{"FECHA",18}");
        Console.WriteLine(new string('-', 110));

        foreach (var reserva in reservas)
        {
            Console.WriteLine($"{reserva.Id,-5}{reserva.NombrePersona,-22}{reserva.Evento.Nombre,-24}{reserva.CantidadEntradas,10}{FormatearMoneda(reserva.ValorTotal),15} {reserva.Estado,-12}{reserva.FechaReserva,18:dd/MM/yyyy HH:mm}");
        }

        Console.WriteLine(new string('-', 110));
    }

    private string FormatearMoneda(decimal valor)
    {
        return "$" + valor.ToString("N0", _cultura);
    }

    private static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (entrada is null)
                throw new FinDeEntradaException();

            if (int.TryParse(entrada.Trim(), out int valor))
                return valor;

            Console.WriteLine("Entrada inválida. Debe ingresar un número entero.");
        }
    }

    private static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (entrada is null)
                throw new FinDeEntradaException();

            if (!string.IsNullOrWhiteSpace(entrada))
                return entrada.Trim();

            Console.WriteLine("El valor no puede estar vacío.");
        }
    }

    private static bool Pausar()
    {
        Console.WriteLine();
        Console.Write("Presione ENTER para continuar...");
        return Console.ReadLine() is not null;
    }

    private static void MostrarError(string mensaje)
    {
        Console.WriteLine();
        Console.WriteLine($"ERROR: {mensaje}");
    }

    private sealed class FinDeEntradaException : Exception
    {
    }
}
