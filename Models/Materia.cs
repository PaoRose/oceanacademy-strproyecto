namespace OceanAcademy.Models;

public class Materia
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Materia(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre: {Nombre}";
    }
}