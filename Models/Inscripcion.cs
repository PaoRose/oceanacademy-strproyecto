namespace OceanAcademy.Models;

public class Inscripcion
{
    public int EstudianteId { get; set; }
    public int MateriaId { get; set; }

    public Inscripcion(int estudianteId, int materiaId)
    {
        EstudianteId = estudianteId;
        MateriaId = materiaId;
    }

    public override string ToString()
    {
        return $"Estudiante: {EstudianteId} | Materia: {MateriaId}";
    }
}