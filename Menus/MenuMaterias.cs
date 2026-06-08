using OceanAcademy.Services;

namespace OceanAcademy.Menus;

public class MenuMaterias
{
    private MateriaService materiaService;

    public MenuMaterias(MateriaService materiaService)
    {
        this.materiaService = materiaService;
    }

    public void Mostrar()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n========================");
            Console.WriteLine("     MENU MATERIAS");
            Console.WriteLine("========================");
            Console.WriteLine("1. Agregar materia");
            Console.WriteLine("2. Mostrar materias");
            Console.WriteLine("3. Buscar materia");
            Console.WriteLine("4. Actualizar materia");
            Console.WriteLine("5. Eliminar materia");
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
                    materiaService.AgregarMateria();
                    break;

                case 2:
                    materiaService.MostrarMaterias();
                    break;

                case 3:
                    materiaService.BuscarMateria();
                    break;

                case 4:
                    materiaService.ActualizarMateria();
                    break;

                case 5:
                    materiaService.EliminarMateria();
                    break;
            }

        } while (opcion != 0);
    }
}