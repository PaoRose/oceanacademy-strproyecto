namespace OceanAcademy.DataStructures;

public class PilaOperaciones<T>
{
    private Stack<T> operaciones = new();

    public void GuardarEliminado(T elemento)
    {
        operaciones.Push(elemento);
    }

    public T? Deshacer()
    {
        if (operaciones.Count == 0)
            return default;

        return operaciones.Pop();
    }

    public bool EstaVacia() => operaciones.Count == 0;
}