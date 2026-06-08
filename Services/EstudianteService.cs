using OceanAcademy.DataStructures;
using OceanAcademy.Models;

namespace OceanAcademy.Services;

public class EstudianteService
{
    private ListaSimple<Estudiante> estudiantes = new();
    private ArbolBST bst = new();
    private ColaConsultas cola = new();
    private PilaOperaciones<Estudiante> pila = new();

    public EstudianteService() { }

    public void AgregarEstudiante()
    {
        Console.Write("ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (bst.Buscar(id))
        {
            Console.WriteLine("Ya existe un estudiante con ese ID.");
            return;
        }

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Estudiante estudiante = new(id, nombre);

        estudiantes.Agregar(estudiante);
        bst.Insertar(id);

        Console.WriteLine("\nEstudiante registrado correctamente.");
    }

    public void MostrarEstudiantes()
    {
        Console.WriteLine("\n=== LISTA DE ESTUDIANTES ===");

        if (estudiantes.EstaVacia())
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }

        foreach (var estudiante in estudiantes.ObtenerTodos())
        {
            Console.WriteLine(estudiante);
        }
    }

    public void BuscarEstudiante()
    {
        Console.Write("Ingrese el ID del estudiante: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        cola.Encolar($"Búsqueda de estudiante ID {id}");

        if (!bst.Buscar(id))
        {
            Console.WriteLine("Estudiante no encontrado.");
            return;
        }

        var estudiante = estudiantes.Buscar(e => e.Id == id);

        if (estudiante == null)
        {
            Console.WriteLine("Estudiante no encontrado.");
            return;
        }

        Console.WriteLine(estudiante);
    }

    public void ActualizarEstudiante()
    {
        Console.Write("Ingrese el ID del estudiante: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (!bst.Buscar(id))
        {
            Console.WriteLine("Estudiante no encontrado.");
            return;
        }

        var estudiante = estudiantes.Buscar(e => e.Id == id);

        if (estudiante == null)
        {
            Console.WriteLine("Estudiante no encontrado.");
            return;
        }

        Console.Write("Nuevo nombre: ");
        estudiante.Nombre = Console.ReadLine() ?? "";

        Console.WriteLine("Estudiante actualizado correctamente.");
    }

    public void EliminarEstudiante()
    {
        Console.Write("Ingrese el ID del estudiante: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var estudiante = estudiantes.Buscar(e => e.Id == id);

        if (estudiante == null)
        {
            Console.WriteLine("Estudiante no encontrado.");
            return;
        }

        pila.GuardarEliminado(estudiante);

        estudiantes.Eliminar(estudiante);

        Console.WriteLine("Estudiante eliminado correctamente.");
    }

    public ListaSimple<Estudiante> ObtenerLista() => estudiantes;
    public ArbolBST ObtenerBST() => bst;

    public void MostrarIndiceBST()
    {
        bst.MostrarEnOrden();
    }

    public void MostrarColaConsultas()
    {
        cola.MostrarConsultas();
    }

    public void DeshacerEliminacion()
    {
        Estudiante? estudiante = pila.Deshacer();

        if (estudiante == null)
        {
            Console.WriteLine("No hay operaciones para deshacer.");
            return;
        }

        estudiantes.Agregar(estudiante);

        if (!bst.Buscar(estudiante.Id))
        {
            bst.Insertar(estudiante.Id);
        }

        Console.WriteLine("Eliminación deshecha correctamente.");
    }
}