namespace PlanazoApp.Excepciones;

public class PlanazoException : Exception
{
    public PlanazoException(string mensaje)
        : base(mensaje)
    {
    }
}
