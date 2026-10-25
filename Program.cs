using System.Text;
using PlanazoApp.Servicios;
using PlanazoApp.UI;

Console.OutputEncoding = Encoding.UTF8;
Console.Title = "PLANAZO - Eventos";

var gestor = new GestorReservas();
var menu = new MenuConsola(gestor);

menu.Ejecutar();
