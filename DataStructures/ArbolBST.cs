namespace OceanAcademy.DataStructures;

public class ArbolBST
{
    private NodoBST? raiz;

    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    private NodoBST InsertarRecursivo(NodoBST? nodo, int valor)
    {
        if (nodo == null)
        {
            return new NodoBST(valor);
        }

        if (valor < nodo.Valor)
        {
            nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, valor);
        }
        else if (valor > nodo.Valor)
        {
            nodo.Derecha = InsertarRecursivo(nodo.Derecha, valor);
        }

        return nodo;
    }

    public bool Buscar(int valor)
    {
        return BuscarRecursivo(raiz, valor);
    }

    private bool BuscarRecursivo(NodoBST? nodo, int valor)
    {
        if (nodo == null)
        {
            return false;
        }

        if (nodo.Valor == valor)
        {
            return true;
        }

        if (valor < nodo.Valor)
        {
            return BuscarRecursivo(nodo.Izquierda, valor);
        }

        return BuscarRecursivo(nodo.Derecha, valor);
    }

    public void MostrarEnOrden()
    {
        Console.WriteLine("\n=== BST EN ORDEN ===");

        MostrarEnOrdenRecursivo(raiz);

        Console.WriteLine();
    }

    private void MostrarEnOrdenRecursivo(NodoBST? nodo)
    {
        if (nodo == null)
        {
            return;
        }

        MostrarEnOrdenRecursivo(nodo.Izquierda);

        Console.Write($"{nodo.Valor} ");

        MostrarEnOrdenRecursivo(nodo.Derecha);
    }
}