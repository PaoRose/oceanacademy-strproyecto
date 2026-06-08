using OceanAcademy.Services;

namespace OceanAcademy.Menus;

public class MenuNotas
{
    private NotasService notasService;

    public MenuNotas(NotasService notasService)
    {
        this.notasService = notasService;
    }

    public void Mostrar()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n========================");
            Console.WriteLine("      MENU NOTAS");
            Console.WriteLine("========================");
            Console.WriteLine("1. Registrar nota");
            Console.WriteLine("2. Actualizar nota");
            Console.WriteLine("3. Ver notas de un estudiante");
            Console.WriteLine("4. Mostrar matriz completa");
            Console.WriteLine("0. Volver");
            Console.Write("Opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1: notasService.RegistrarNota();       break;
                case 2: notasService.ActualizarNota();      break;
                case 3: notasService.VerNotasEstudiante();  break;
                case 4: notasService.MostrarNotas();        break;
                case 0: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opción no válida."); break;
            }

        } while (opcion != 0);
    }
}