namespace OceanAcademy.DataStructures;

public class ColaConsultas
{
    private Queue<string> consultas = new();

    public void Encolar(string consulta)
    {
        consultas.Enqueue(consulta);
    }

    public void MostrarConsultas()
    {
        Console.WriteLine("\n=== COLA DE CONSULTAS ===");

        if (consultas.Count == 0)
        {
            Console.WriteLine("No hay consultas registradas.");
            return;
        }

        foreach (var consulta in consultas)
        {
            Console.WriteLine(consulta);
        }
    }
}