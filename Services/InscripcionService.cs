using OceanAcademy.DataStructures;
using OceanAcademy.Models;

namespace OceanAcademy.Services;

public class InscripcionService
{
    private ListaSimple<Inscripcion>    inscripciones = new();
    private PilaOperaciones<Inscripcion> pila         = new();
    private GrafoRelaciones             grafo         = new();
    private ListaSimple<Estudiante>     estudiantes;
    private ListaSimple<Materia>        materias;

    public InscripcionService(
        ListaSimple<Estudiante> estudiantes,
        ListaSimple<Materia>    materias)
    {
        this.estudiantes = estudiantes;
        this.materias    = materias;
    }

    public ListaSimple<Inscripcion> ObtenerLista() => inscripciones;

    public bool EstaInscrito(int estudianteId, int materiaId)
    {
        return inscripciones.ObtenerTodos()
            .Any(i => i.EstudianteId == estudianteId && i.MateriaId == materiaId);
    }

    public void AgregarInscripcion()
    {
        var listaEst = estudiantes.ObtenerTodos();
        var listaMat = materias.ObtenerTodos();

        if (listaEst.Count == 0) { Console.WriteLine("No hay estudiantes registrados."); return; }
        if (listaMat.Count == 0) { Console.WriteLine("No hay materias registradas.");    return; }

        // Mostrar estudiantes disponibles
        Console.WriteLine("\nEstudiantes disponibles:");
        foreach (var e in listaEst)
            Console.WriteLine($"  ID {e.Id} - {e.Nombre}");

        Console.Write("\nID Estudiante: ");
        if (!int.TryParse(Console.ReadLine(), out int estudianteId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var estudiante = listaEst.Find(e => e.Id == estudianteId);
        if (estudiante == null) { Console.WriteLine("Estudiante no encontrado."); return; }

        // Mostrar materias separadas: ya inscrito / disponibles
        var inscritas    = listaMat.Where(m =>  EstaInscrito(estudianteId, m.Id)).ToList();
        var disponibles  = listaMat.Where(m => !EstaInscrito(estudianteId, m.Id)).ToList();

        if (disponibles.Count == 0)
        {
            Console.WriteLine($"{estudiante.Nombre} ya está inscrito en todas las materias.");
            return;
        }

        if (inscritas.Count > 0)
        {
            Console.WriteLine($"\nYa inscrito en:");
            foreach (var m in inscritas)
                Console.WriteLine($"  ✅ ID {m.Id} - {m.Nombre}");
        }

        Console.WriteLine($"\nMaterias disponibles para inscribir:");
        foreach (var m in disponibles)
            Console.WriteLine($"  ID {m.Id} - {m.Nombre}");

        Console.Write("\nID Materia: ");
        if (!int.TryParse(Console.ReadLine(), out int materiaId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (EstaInscrito(estudianteId, materiaId))
        {
            Console.WriteLine("Esa inscripción ya existe.");
            return;
        }

        var materia = listaMat.Find(m => m.Id == materiaId);
        if (materia == null) { Console.WriteLine("Materia no encontrada."); return; }

        var inscripcion = new Inscripcion(estudianteId, materiaId);
        inscripciones.Agregar(inscripcion);
        grafo.AgregarRelacion(estudianteId, materiaId);

        Console.WriteLine($"✅ {estudiante.Nombre} inscrito en {materia.Nombre}.");
    }

    public void EliminarInscripcion()
    {
        var listaEst = estudiantes.ObtenerTodos();
        if (listaEst.Count == 0) { Console.WriteLine("No hay estudiantes registrados."); return; }

        Console.WriteLine("\nEstudiantes disponibles:");
        foreach (var e in listaEst)
            Console.WriteLine($"  ID {e.Id} - {e.Nombre}");

        Console.Write("\nID Estudiante: ");
        if (!int.TryParse(Console.ReadLine(), out int estudianteId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var estudiante = listaEst.Find(e => e.Id == estudianteId);
        if (estudiante == null) { Console.WriteLine("Estudiante no encontrado."); return; }

        var inscritas = inscripciones.ObtenerTodos()
            .Where(i => i.EstudianteId == estudianteId).ToList();

        if (inscritas.Count == 0)
        {
            Console.WriteLine($"{estudiante.Nombre} no tiene inscripciones.");
            return;
        }

        Console.WriteLine($"\nInscripciones de {estudiante.Nombre}:");
        foreach (var i in inscritas)
        {
            var mat = materias.ObtenerTodos().Find(m => m.Id == i.MateriaId);
            Console.WriteLine($"  ID {i.MateriaId} - {mat?.Nombre ?? "Desconocida"}");
        }

        Console.Write("\nID Materia a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int materiaId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var inscripcion = inscripciones.ObtenerTodos()
            .Find(i => i.EstudianteId == estudianteId && i.MateriaId == materiaId);

        if (inscripcion == null)
        {
            Console.WriteLine("Inscripción no encontrada.");
            return;
        }

        pila.GuardarEliminado(inscripcion);
        inscripciones.Eliminar(inscripcion);

        var materia = materias.ObtenerTodos().Find(m => m.Id == materiaId);
        Console.WriteLine($"✅ Inscripción de {estudiante.Nombre} en {materia?.Nombre} eliminada.");
    }

    public void DeshacerEliminacion()
    {
        var inscripcion = pila.Deshacer();

        if (inscripcion == null)
        {
            Console.WriteLine("No hay eliminaciones para deshacer.");
            return;
        }

        if (EstaInscrito(inscripcion.EstudianteId, inscripcion.MateriaId))
        {
            Console.WriteLine("La inscripción ya existe, no se necesita restaurar.");
            return;
        }

        inscripciones.Agregar(inscripcion);
        grafo.AgregarRelacion(inscripcion.EstudianteId, inscripcion.MateriaId);

        var est = estudiantes.ObtenerTodos().Find(e => e.Id == inscripcion.EstudianteId);
        var mat = materias.ObtenerTodos().Find(m => m.Id == inscripcion.MateriaId);
        Console.WriteLine($"✅ Inscripción de {est?.Nombre} en {mat?.Nombre} restaurada.");
    }

    public void MostrarInscripciones()
    {
        Console.WriteLine("\n=== INSCRIPCIONES ===\n");

        if (inscripciones.EstaVacia())
        {
            Console.WriteLine("No existen inscripciones.");
            return;
        }

        var lista    = inscripciones.ObtenerTodos();
        var listaEst = estudiantes.ObtenerTodos();
        var listaMat = materias.ObtenerTodos();

        // Anchos fijos para IDs + dinámicos para nombres
        int colIdEst  = 6;  // "ID Est"
        int colIdMat  = 6;  // "ID Mat"
        int colEst = Math.Max("Estudiante".Length, lista
            .Select(i => listaEst.Find(e => e.Id == i.EstudianteId)?.Nombre ?? "")
            .Max(n => n.Length)) + 2;
        int colMat = Math.Max("Materia".Length, lista
            .Select(i => listaMat.Find(m => m.Id == i.MateriaId)?.Nombre ?? "")
            .Max(n => n.Length)) + 2;

        // Encabezado
        Console.WriteLine(
            "┌" + new string('─', colIdEst) +
            "┬" + new string('─', colEst)   +
            "┬" + new string('─', colIdMat) +
            "┬" + new string('─', colMat)   + "┐");
        Console.WriteLine(
            "│" + "ID Est".PadRight(colIdEst) +
            "│" + "Estudiante".PadRight(colEst) +
            "│" + "ID Mat".PadRight(colIdMat) +
            "│" + "Materia".PadRight(colMat) + "│");
        Console.WriteLine(
            "├" + new string('─', colIdEst) +
            "┼" + new string('─', colEst)   +
            "┼" + new string('─', colIdMat) +
            "┼" + new string('─', colMat)   + "┤");

        // Filas
        foreach (var i in lista)
        {
            var estNombre = listaEst.Find(e => e.Id == i.EstudianteId)?.Nombre ?? "?";
            var matNombre = listaMat.Find(m => m.Id == i.MateriaId)?.Nombre    ?? "?";

            Console.WriteLine(
                "│" + i.EstudianteId.ToString().PadRight(colIdEst) +
                "│" + estNombre.PadRight(colEst) +
                "│" + i.MateriaId.ToString().PadRight(colIdMat) +
                "│" + matNombre.PadRight(colMat) + "│");
        }

        // Pie
        Console.WriteLine(
            "└" + new string('─', colIdEst) +
            "┴" + new string('─', colEst)   +
            "┴" + new string('─', colIdMat) +
            "┴" + new string('─', colMat)   + "┘");
    }

    public void MostrarGrafo()
    {
        grafo.MostrarRelaciones();
    }
}