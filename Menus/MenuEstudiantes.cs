using OceanAcademy.Services;

namespace OceanAcademy.Menus;

public class MenuEstudiantes
{
    private EstudianteService estudianteService;

    public MenuEstudiantes(EstudianteService estudianteService)
    {
        this.estudianteService = estudianteService;
    }

    public void Mostrar()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n========================");
            Console.WriteLine("   MENU ESTUDIANTES");
            Console.WriteLine("========================");
            Console.WriteLine("1. Agregar estudiante");
            Console.WriteLine("2. Mostrar estudiantes");
            Console.WriteLine("3. Buscar estudiante");
            Console.WriteLine("4. Actualizar estudiante");
            Console.WriteLine("5. Eliminar estudiante");
            Console.WriteLine("6. Mostrar índice BST");
            Console.WriteLine("7. Ver historial de consultas");
            Console.WriteLine("8. Deshacer eliminación");
            Console.WriteLine("0. Volver");
            Console.Write("Opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    estudianteService.AgregarEstudiante();
                    break;

                case 2:
                    estudianteService.MostrarEstudiantes();
                    break;

                case 3:
                    estudianteService.BuscarEstudiante();
                    break;

                case 4:
                    estudianteService.ActualizarEstudiante();
                    break;

                case 5:
                    estudianteService.EliminarEstudiante();
                    break;

                case 6:
                    estudianteService.MostrarIndiceBST();
                    break;

                case 7:
                    estudianteService.MostrarColaConsultas();
                    break;

                case 8:
                    estudianteService.DeshacerEliminacion();
                    break;

                case 0:
                    Console.WriteLine("Volviendo...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

        } while (opcion != 0);
    }
}