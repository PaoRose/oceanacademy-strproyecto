using OceanAcademy.DataStructures;
using OceanAcademy.Models;

namespace OceanAcademy.Services;

public class MateriaService
{
    private ListaSimple<Materia> materias = new();

    public MateriaService() { }

    public ListaSimple<Materia> ObtenerLista() => materias;

    public void AgregarMateria()
    {
        Console.Write("ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Materia materia = new(id, nombre);

        materias.Agregar(materia);

        Console.WriteLine("Materia registrada correctamente.");
    }

    public void MostrarMaterias()
    {
        Console.WriteLine("\n=== LISTA DE MATERIAS ===");

        if (materias.EstaVacia())
        {
            Console.WriteLine("No hay materias registradas.");
            return;
        }

        foreach (var materia in materias.ObtenerTodos())
        {
            Console.WriteLine(materia);
        }
    }

    public void BuscarMateria()
    {
        Console.Write("Ingrese el ID de la materia: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var materia = materias.Buscar(m => m.Id == id);

        if (materia == null)
        {
            Console.WriteLine("Materia no encontrada.");
            return;
        }

        Console.WriteLine(materia);
    }

    public void ActualizarMateria()
    {
        Console.Write("Ingrese el ID de la materia: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var materia = materias.Buscar(m => m.Id == id);

        if (materia == null)
        {
            Console.WriteLine("Materia no encontrada.");
            return;
        }

        Console.Write("Nuevo nombre: ");
        materia.Nombre = Console.ReadLine() ?? "";

        Console.WriteLine("Materia actualizada correctamente.");
    }

    public void EliminarMateria()
    {
        Console.Write("Ingrese el ID de la materia: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var materia = materias.Buscar(m => m.Id == id);

        if (materia == null)
        {
            Console.WriteLine("Materia no encontrada.");
            return;
        }

        materias.Eliminar(materia);

        Console.WriteLine("Materia eliminada correctamente.");
    }
}