namespace PlanazoApp.Modelos;

public sealed class Usuario
{
    public int Id { get; }
    public string Nombre { get; }
    public string Correo { get; }

    public Usuario(int id, string nombre, string correo)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del usuario es obligatorio.", nameof(nombre));

        if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            throw new ArgumentException("El correo no tiene un formato válido.", nameof(correo));

        Id = id;
        Nombre = nombre.Trim();
        Correo = correo.Trim();
    }

    public override string ToString()
    {
        return $"{Nombre} ({Correo})";
    }
}
