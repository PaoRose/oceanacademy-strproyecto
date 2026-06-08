using OceanAcademy.Services;

namespace OceanAcademy.Menus;

public class MenuPrincipal
{
    private EstudianteService   estudianteService  = new();
    private MateriaService      materiaService     = new();
    private InscripcionService  inscripcionService;
    private NotasService        notasService;

    private MenuEstudiantes     menuEstudiantes;
    private MenuMaterias        menuMaterias;
    private MenuInscripciones   menuInscripciones;
    private MenuNotas           menuNotas;

    public MenuPrincipal()
    {
        // InscripcionService necesita las listas para mostrar nombres
        inscripcionService = new InscripcionService(
            estudianteService.ObtenerLista(),
            materiaService.ObtenerLista()
        );

        // 1. Cargar datos desde JSON
        DataService.Cargar(
            estudianteService.ObtenerLista(),
            materiaService.ObtenerLista(),
            inscripcionService.ObtenerLista(),
            estudianteService.ObtenerBST()
        );

        // 2. Crear NotasService con todas las dependencias
        notasService = new NotasService(
            estudianteService.ObtenerLista(),
            materiaService.ObtenerLista(),
            inscripcionService
        );

        // 3. Cargar notas en la matriz
        DataService.CargarNotas(notasService.ObtenerMatriz());

        menuEstudiantes   = new MenuEstudiantes(estudianteService);
        menuMaterias      = new MenuMaterias(materiaService);
        menuInscripciones = new MenuInscripciones(inscripcionService);
        menuNotas         = new MenuNotas(notasService);
    }

    public void Mostrar()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n========================");
            Console.WriteLine("      OCEAN ACADEMY");
            Console.WriteLine("========================");
            Console.WriteLine("1. Estudiantes");
            Console.WriteLine("2. Materias");
            Console.WriteLine("3. Inscripciones");
            Console.WriteLine("4. Notas");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1: menuEstudiantes.Mostrar();   break;
                case 2: menuMaterias.Mostrar();       break;
                case 3: menuInscripciones.Mostrar();  break;
                case 4: menuNotas.Mostrar();           break;
            }

        } while (opcion != 0);

        DataService.Guardar(
            estudianteService.ObtenerLista(),
            materiaService.ObtenerLista(),
            inscripcionService.ObtenerLista(),
            notasService.ObtenerMatriz()
        );
    }
}