using System.Text.Json;
using OceanAcademy.DataStructures;
using OceanAcademy.Models;

namespace OceanAcademy.Services;

public class DataService
{
    private const string Archivo = "datos.json";

    private class DatosGuardados
    {
        public List<Estudiante>   Estudiantes   { get; set; } = new();
        public List<Materia>      Materias      { get; set; } = new();
        public List<Inscripcion>  Inscripciones { get; set; } = new();
        public List<NotaGuardada> Notas         { get; set; } = new();
    }

    private static JsonSerializerOptions Opciones => new() { WriteIndented = true };

    // Buffer temporal para las notas (se cargan después de crear NotasService)
    private static List<NotaGuardada> _notasPendientes = new();

    // ── GUARDAR ────────────────────────────────────────────────────────────────
    public static void Guardar(
        ListaSimple<Estudiante>  estudiantes,
        ListaSimple<Materia>     materias,
        ListaSimple<Inscripcion> inscripciones,
        MatrizNotas              matriz)
    {
        var datos = new DatosGuardados
        {
            Estudiantes   = estudiantes.ObtenerTodos(),
            Materias      = materias.ObtenerTodos(),
            Inscripciones = inscripciones.ObtenerTodos(),
            Notas         = matriz.ObtenerTodasLasNotas()
        };

        File.WriteAllText(Archivo, JsonSerializer.Serialize(datos, Opciones));
        Console.WriteLine($"\n💾 Datos guardados en '{Archivo}'.");
    }

    // ── CARGAR ─────────────────────────────────────────────────────────────────
    public static void Cargar(
        ListaSimple<Estudiante>  estudiantes,
        ListaSimple<Materia>     materias,
        ListaSimple<Inscripcion> inscripciones,
        ArbolBST                 bst)
    {
        if (!File.Exists(Archivo))
        {
            Console.WriteLine("📂 No se encontró archivo de datos. Iniciando vacío.");
            return;
        }

        try
        {
            var json  = File.ReadAllText(Archivo);
            var datos = JsonSerializer.Deserialize<DatosGuardados>(json, Opciones);

            if (datos == null) return;

            foreach (var e in datos.Estudiantes)
            {
                estudiantes.Agregar(e);
                bst.Insertar(e.Id);
            }

            foreach (var m in datos.Materias)
                materias.Agregar(m);

            foreach (var i in datos.Inscripciones)
                inscripciones.Agregar(i);

            _notasPendientes = datos.Notas;

            Console.WriteLine($"✅ Datos cargados desde '{Archivo}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️  Error al cargar datos: {ex.Message}");
        }
    }

    // ── CARGAR NOTAS (llamar después de crear NotasService) ───────────────────
    public static void CargarNotas(MatrizNotas matriz)
    {
        foreach (var n in _notasPendientes)
            matriz.RegistrarNota(n.EstudianteId, n.MateriaId, n.Nota);
        _notasPendientes.Clear();
    }
}