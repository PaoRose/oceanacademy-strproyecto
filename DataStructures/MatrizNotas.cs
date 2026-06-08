using OceanAcademy.Models;

namespace OceanAcademy.DataStructures;

public class MatrizNotas
{
    private Dictionary<(int, int), int> notas = new();

    public void RegistrarNota(int estudianteId, int materiaId, int nota)
    {
        notas[(estudianteId, materiaId)] = nota;
    }

    public int? ObtenerNota(int estudianteId, int materiaId)
    {
        if (notas.TryGetValue((estudianteId, materiaId), out int nota))
            return nota;
        return null;
    }

    // Expone las notas como lista para que DataService las pueda guardar
    public List<NotaGuardada> ObtenerTodasLasNotas()
    {
        return notas.Select(kv => new NotaGuardada
        {
            EstudianteId = kv.Key.Item1,
            MateriaId    = kv.Key.Item2,
            Nota         = kv.Value
        }).ToList();
    }

    public void MostrarMatriz(
        List<(int Id, string Nombre)> estudiantes,
        List<(int Id, string Nombre)> materias)
    {
        Console.WriteLine("\n=== LIBRETA DE CALIFICACIONES ===\n");

        if (estudiantes.Count == 0 || materias.Count == 0)
        {
            Console.WriteLine("No hay estudiantes o materias registrados.");
            return;
        }

        int colEst = Math.Max(15, estudiantes.Max(e => e.Nombre.Length) + 2);
        List<int> colMat = materias
            .Select(m => Math.Max(10, m.Nombre.Length + 2))
            .ToList();

        Console.Write("┌" + new string('─', colEst) + "┬");
        Console.WriteLine(string.Join("┬", colMat.Select(w => new string('─', w))) + "┐");

        Console.Write("│" + "Estudiante".PadRight(colEst) + "│");
        for (int i = 0; i < materias.Count; i++)
            Console.Write(materias[i].Nombre.PadRight(colMat[i]) + "│");
        Console.WriteLine();

        Console.Write("├" + new string('─', colEst) + "┼");
        Console.WriteLine(string.Join("┼", colMat.Select(w => new string('─', w))) + "┤");

        foreach (var e in estudiantes)
        {
            Console.Write("│" + e.Nombre.PadRight(colEst) + "│");
            for (int i = 0; i < materias.Count; i++)
            {
                var nota = ObtenerNota(e.Id, materias[i].Id);
                string valor = nota.HasValue ? nota.Value.ToString() : "-";
                Console.Write(valor.PadRight(colMat[i]) + "│");
            }
            Console.WriteLine();
        }

        Console.Write("└" + new string('─', colEst) + "┴");
        Console.WriteLine(string.Join("┴", colMat.Select(w => new string('─', w))) + "┘");
    }
}