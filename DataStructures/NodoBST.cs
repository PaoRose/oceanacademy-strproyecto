namespace OceanAcademy.DataStructures;

public class NodoBST
{
    public int Valor { get; set; }

    public NodoBST? Izquierda { get; set; }

    public NodoBST? Derecha { get; set; }

    public NodoBST(int valor)
    {
        Valor = valor;
    }
}