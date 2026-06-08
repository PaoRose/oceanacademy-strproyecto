using OceanAcademy.DataStructures;
using OceanAcademy.Models;

namespace OceanAcademy.Services;

public class NotasService
{
    private MatrizNotas matriz = new();
    private ListaSimple<Estudiante> estudiantes;
    private ListaSimple<Materia> materias;
    private InscripcionService inscripciones;

    public NotasService(
        ListaSimple<Estudiante> estudiantes,
        ListaSimple<Materia>    materias,
        InscripcionService      inscripciones)
    {
        this.estudiantes   = estudiantes;
        this.materias      = materias;
        this.inscripciones = inscripciones;
    }

    public MatrizNotas ObtenerMatriz() => matriz;

    // ── HELPERS ───────────────────────────────────────────────────────────────

    private Estudiante? SeleccionarEstudiante()
    {
        var lista = estudiantes.ObtenerTodos();
        if (lista.Count == 0) { Console.WriteLine("No hay estudiantes registrados."); return null; }

        Console.WriteLine("\nEstudiantes disponibles:");
        foreach (var e in lista)
            Console.WriteLine($"  ID {e.Id} - {e.Nombre}");

        Console.Write("\nIngrese el ID del estudiante: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return null;
        }

        var estudiante = lista.Find(e => e.Id == id);
        if (estudiante == null) Console.WriteLine("Estudiante no encontrado.");
        return estudiante;
    }

    private Materia? SeleccionarMateriaInscrita(int estudianteId, string nombreEstudiante)
    {
        var listaMat = materias.ObtenerTodos();

        var inscritas = listaMat
            .Where(m => inscripciones.EstaInscrito(estudianteId, m.Id))
            .ToList();

        if (inscritas.Count == 0)
        {
            Console.WriteLine($"⚠️  {nombreEstudiante} no está inscrito en ninguna materia.");
            return null;
        }

        Console.WriteLine($"\nMaterias de {nombreEstudiante}:");
        foreach (var m in inscritas)
        {
            var nota = matriz.ObtenerNota(estudianteId, m.Id);
            string notaStr = nota.HasValue ? $"nota actual: {nota}" : "sin nota";
            Console.WriteLine($"  ID {m.Id} - {m.Nombre} ({notaStr})");
        }

        Console.Write("\nIngrese el ID de la materia: ");
        if (!int.TryParse(Console.ReadLine(), out int materiaId))
        {
            Console.WriteLine("ID inválido.");
            return null;
        }

        if (!inscripciones.EstaInscrito(estudianteId, materiaId))
        {
            Console.WriteLine("❌ Ese estudiante no está inscrito en esa materia.");
            return null;
        }

        var materia = listaMat.Find(m => m.Id == materiaId);
        if (materia == null) Console.WriteLine("Materia no encontrada.");
        return materia;
    }

    // ── REGISTRAR ─────────────────────────────────────────────────────────────

    public void RegistrarNota()
    {
        Console.WriteLine("\n=== REGISTRAR NOTA ===");

        var estudiante = SeleccionarEstudiante();
        if (estudiante == null) return;

        var materia = SeleccionarMateriaInscrita(estudiante.Id, estudiante.Nombre);
        if (materia == null) return;

        if (matriz.ObtenerNota(estudiante.Id, materia.Id).HasValue)
        {
            Console.WriteLine($"⚠️  Ya existe una nota para {estudiante.Nombre} en {materia.Nombre}. Use 'Actualizar nota'.");
            return;
        }

        Console.Write("Nota (0-100): ");
        if (!int.TryParse(Console.ReadLine(), out int nota) || nota < 0 || nota > 100)
        {
            Console.WriteLine("Nota inválida. Debe ser entre 0 y 100.");
            return;
        }

        matriz.RegistrarNota(estudiante.Id, materia.Id, nota);
        Console.WriteLine($"✅ Nota {nota} registrada para {estudiante.Nombre} en {materia.Nombre}.");
    }

    // ── ACTUALIZAR ────────────────────────────────────────────────────────────

    public void ActualizarNota()
    {
        Console.WriteLine("\n=== ACTUALIZAR NOTA ===");

        var estudiante = SeleccionarEstudiante();
        if (estudiante == null) return;

        var materia = SeleccionarMateriaInscrita(estudiante.Id, estudiante.Nombre);
        if (materia == null) return;

        var notaActual = matriz.ObtenerNota(estudiante.Id, materia.Id);
        if (!notaActual.HasValue)
        {
            Console.WriteLine($"⚠️  No hay nota registrada aún. Use 'Registrar nota'.");
            return;
        }

        Console.Write($"Nueva nota (actual: {notaActual}) (0-100): ");
        if (!int.TryParse(Console.ReadLine(), out int nuevaNota) || nuevaNota < 0 || nuevaNota > 100)
        {
            Console.WriteLine("Nota inválida. Debe ser entre 0 y 100.");
            return;
        }

        matriz.RegistrarNota(estudiante.Id, materia.Id, nuevaNota);
        Console.WriteLine($"✅ Nota actualizada de {notaActual} → {nuevaNota} para {estudiante.Nombre} en {materia.Nombre}.");
    }

    // ── VER NOTAS DE UN ESTUDIANTE ────────────────────────────────────────────

    public void VerNotasEstudiante()
    {
        Console.WriteLine("\n=== NOTAS POR ESTUDIANTE ===");

        var estudiante = SeleccionarEstudiante();
        if (estudiante == null) return;

        var inscritas = materias.ObtenerTodos()
            .Where(m => inscripciones.EstaInscrito(estudiante.Id, m.Id))
            .ToList();

        if (inscritas.Count == 0)
        {
            Console.WriteLine($"{estudiante.Nombre} no está inscrito en ninguna materia.");
            return;
        }

        // Anchos dinámicos
        int colMat  = Math.Max("Materia".Length, inscritas.Max(m => m.Nombre.Length)) + 2;
        int colNota = 7;

        Console.WriteLine($"\nEstudiante: {estudiante.Nombre} (ID {estudiante.Id})\n");
        Console.WriteLine("┌" + new string('─', colMat) + "┬" + new string('─', colNota) + "┐");
        Console.WriteLine("│" + "Materia".PadRight(colMat) + "│" + "Nota".PadRight(colNota) + "│");
        Console.WriteLine("├" + new string('─', colMat) + "┼" + new string('─', colNota) + "┤");

        var notasValidas = new List<int>();
        foreach (var m in inscritas)
        {
            var nota = matriz.ObtenerNota(estudiante.Id, m.Id);
            string notaStr = nota.HasValue ? nota.Value.ToString() : "-";
            if (nota.HasValue) notasValidas.Add(nota.Value);
            Console.WriteLine("│" + m.Nombre.PadRight(colMat) + "│" + notaStr.PadRight(colNota) + "│");
        }

        Console.WriteLine("└" + new string('─', colMat) + "┴" + new string('─', colNota) + "┘");

        // Promedio
        if (notasValidas.Count > 0)
        {
            double promedio = notasValidas.Average();
            Console.WriteLine($"\n  Promedio: {promedio:F2}  ({notasValidas.Count}/{inscritas.Count} materias calificadas)");
        }
        else
        {
            Console.WriteLine("\n  Sin notas registradas aún.");
        }
    }

    // ── MOSTRAR MATRIZ COMPLETA ───────────────────────────────────────────────

    public void MostrarNotas()
    {
        var listaEst = estudiantes.ObtenerTodos()
            .Select(e => (e.Id, e.Nombre)).ToList();
        var listaMat = materias.ObtenerTodos()
            .Select(m => (m.Id, m.Nombre)).ToList();

        matriz.MostrarMatriz(listaEst, listaMat);
    }
}