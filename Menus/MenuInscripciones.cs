using OceanAcademy.Services;

namespace OceanAcademy.Menus;

public class MenuInscripciones
{
    private InscripcionService inscripcionService;

    public MenuInscripciones(InscripcionService inscripcionService)
    {
        this.inscripcionService = inscripcionService;
    }

    public void Mostrar()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n========================");
            Console.WriteLine("  MENU INSCRIPCIONES");
            Console.WriteLine("========================");
            Console.WriteLine("1. Agregar inscripción");
            Console.WriteLine("2. Eliminar inscripción");
            Console.WriteLine("3. Deshacer eliminación");
            Console.WriteLine("4. Mostrar inscripciones");
            Console.WriteLine("5. Mostrar grafo");
            Console.WriteLine("0. Volver");
            Console.Write("Opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1: inscripcionService.AgregarInscripcion();   break;
                case 2: inscripcionService.EliminarInscripcion();  break;
                case 3: inscripcionService.DeshacerEliminacion();  break;
                case 4: inscripcionService.MostrarInscripciones(); break;
                case 5: inscripcionService.MostrarGrafo();         break;
                case 0: Console.WriteLine("Volviendo...");         break;
                default: Console.WriteLine("Opción no válida.");   break;
            }

        } while (opcion != 0);
    }
}