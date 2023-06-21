using EspacioClaseTarea;


internal class Program
{
    private static void Main(string[] args)
    {
        var ListaPendientes = new LinkedList<Tarea>();
        var ListaRealizadas = new LinkedList<Tarea>();
        var ListaTareasCoincidentesXDescip = new LinkedList<Tarea>();

        cargarTareas(ref ListaPendientes);

        bool activador = true;
        int HorasTrabajadas = 0;
        while(activador)
        {
            Console.WriteLine("\n\n        MENÚ DEL PROGRAMA\n        *****************");
            Console.WriteLine(" 1).  Mostrar Tareas Pendientes");
            Console.WriteLine(" 2).  Mostrar Tareas Ya Realizadas");
            Console.WriteLine(" 3).  Marcar Tareas Pendientes Como Realizadas");
            Console.WriteLine(" 4).  Buscar Tarea por Descripción");
            Console.WriteLine(" 5).  Salir\n");

            string input = Console.ReadLine();
            int.TryParse(input, out int accion);

            switch (accion)
            {
                case 1:
                    MostarLista(ListaPendientes);
                    Console.WriteLine("\nPrecione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;

                case 2:
                    MostarLista(ListaRealizadas);
                    Console.WriteLine("\nPrecione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                
                case 3:
                    ConsoltorTareas(ref ListaPendientes, ref ListaRealizadas);
                    Console.WriteLine("\nPrecione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;

                case 4:
                    ListaTareasCoincidentesXDescip = tareasPendientesPorDescripcion(ref ListaPendientes, ref ListaRealizadas);
                    if (ListaTareasCoincidentesXDescip.Count == 0)
                    {
                        Console.WriteLine("Ninguna Tarea posee la descripcion ingresada...");
                    } else {
                        Console.WriteLine("Tareas cuya descripción coincidio con la ingresada:");
                        MostarLista(ListaTareasCoincidentesXDescip);
                    }
                    Console.WriteLine("\nPrecione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                
                case 5:
                    activador = false;
                    foreach (var tarea in ListaRealizadas)
                    {
                        HorasTrabajadas += tarea.Duracion;
                    }
                    Console.WriteLine("Se trabajaron un total de "+ HorasTrabajadas+"hs.");
                    using (var archivo = new StreamWriter("archivo.txt"))
                        {
                            archivo.WriteLine("Horas trabajadas: "+HorasTrabajadas); 
                            archivo.Close();   
                        }
                        Console.WriteLine("\nCerrando el Programa...");
                    break;
                default:
                    Console.WriteLine("Valor ingresado incorrecto; por favor intente nuevamente.\n");
                    break;
            }
        }
        


    }   

    public static void cargarTareas(ref LinkedList<Tarea> ListaPendientes)
    {
        Console.Write("Ingrese la cantidad de Tareas que desea Generar: ");
        string input = Console.ReadLine();
        int.TryParse(input, out int cantTareas);

        for (int i = 1; i < (cantTareas+1); i++)
        {
            ListaPendientes.AddLast(GeneradorTareas(i));
        }
    }

    public static Tarea GeneradorTareas(int id){
        
        Console.WriteLine("\n\nIngrese la descripcion de la Tarea "+(id));
        string? descripcion = Console.ReadLine();
        Random random = new Random();  // se genera la instacia de la clase ramdom
        int numAleatoreo = random.Next(1,101);  // obtengo el numero aleatore a travez de ramdom.netx. Entre () va el intervalo en el cual quiero que caiga mi valor, se usa la forma (min, max+1);
        Tarea tarea = new Tarea(id, descripcion, numAleatoreo);

        return tarea;
    }

    public static void MostarLista(LinkedList<Tarea> ListaTareas){
        if (ListaTareas.Count == 0)
        {
            Console.WriteLine("\n\n     No tiene tareas en esta categoria...");
        }

        foreach (var tarea in ListaTareas)
        {
            tarea.MostrarTarea();
        }
    }

    public static void ConsoltorTareas(ref LinkedList<Tarea> tareasPendientes, ref LinkedList<Tarea> tareasRealizadas)
    {   
        List<Tarea> tareasParaEliminar = new List<Tarea>(); // tuve que crear una lista donde almacenaria las tareas a eliminar porque no puedo tocar la coleccion sobre la que estoy iterando

        foreach (var tarea in tareasPendientes)
        {   
            int cantidadTareasRealizadas = tareasRealizadas.Count;
            tarea.MostrarTarea();
            Console.WriteLine("\nLa tarea se encuentra realizada?  SI - NO");
            string? respuesta = Console.ReadLine();

            if (respuesta.ToUpper() == "SI")    
            {
                tareasRealizadas.AddLast(tarea);
                tareasParaEliminar.Add(tarea);
                if (cantidadTareasRealizadas != tareasRealizadas.Count)
                {
                    Console.WriteLine("La tarea con ID "+tarea.TareaId+" se movio con exito...");
                } 
            } 
        }
        // aqui recien elimino la tarea...
        foreach (var tarea in tareasParaEliminar)
        {
            tareasPendientes.Remove(tarea);
        }

    }

    public static LinkedList<Tarea> tareasPendientesPorDescripcion(ref LinkedList<Tarea> tareasPendientes, ref LinkedList<Tarea> tareasRealizadas)
    {   
        var tareasCoincidentes = new LinkedList<Tarea>();
        Console.WriteLine("Ingrese la descripcion de la tarea a buscar...");
        string? descripcionABuscar = Console.ReadLine().ToUpper();
        foreach (var tarea in tareasPendientes)
        {
            if (tarea.Descripcion.ToUpper() == descripcionABuscar)
            {
                tareasCoincidentes.AddLast(tarea);
            }
        }
        return tareasCoincidentes;
    }


}
