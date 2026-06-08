namespace OceanAcademy.Models;

public class Estudiante
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Estudiante(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre: {Nombre}";
    }
}