namespace OceanAcademy.DataStructures;

public class GrafoRelaciones
{
    private Dictionary<int, List<int>> relaciones = new();

    public void AgregarRelacion(int estudianteId, int materiaId)
    {
        if (!relaciones.ContainsKey(estudianteId))
        {
            relaciones[estudianteId] = new List<int>();
        }

        relaciones[estudianteId].Add(materiaId);
    }

    public void MostrarRelaciones()
    {
        Console.WriteLine("\n=== GRAFO DE RELACIONES ===");

        foreach (var relacion in relaciones)
        {
            Console.Write($"Estudiante {relacion.Key} -> ");

            foreach (var materia in relacion.Value)
            {
                Console.Write($"{materia} ");
            }

            Console.WriteLine();
        }
    }
}