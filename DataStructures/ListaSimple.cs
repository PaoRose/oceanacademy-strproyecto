namespace OceanAcademy.DataStructures;

public class ListaSimple<T>
{
    private List<T> elementos = new();

    public void Agregar(T elemento)
    {
        elementos.Add(elemento);
    }

    public bool Eliminar(T elemento)
    {
        return elementos.Remove(elemento);
    }

    public T? Buscar(Predicate<T> criterio)
    {
        return elementos.Find(criterio);
    }

    public List<T> ObtenerTodos()
    {
        return elementos;
    }

    public bool EstaVacia()
    {
        return elementos.Count == 0;
    }
}