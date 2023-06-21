namespace EspacioClaseTarea {

    public class Tarea {
        public int TareaId;
        public string? Descripcion;
        public int Duracion;

        public Tarea(int tareaId, string? descripcion, int duracion)
        {
            TareaId = tareaId;
            Descripcion = descripcion;
            Duracion = duracion;
        }

        public void MostrarTarea(){
            Console.WriteLine("\n\n    ID: "+TareaId);
            Console.WriteLine("     Descripción: "+Descripcion);
            Console.WriteLine("     Duración: "+Duracion);
        }
    }
}