
class Program
{
    static void Main()
    {
        Console.Write("Ingrese la ruta de la carpeta: ");
        string ruta = Console.ReadLine();

        string[] archivos = Directory.GetFiles(ruta);

        Console.WriteLine("Archivos encontrados:");
        foreach (string archivo in archivos)
        {
            string nombreArchivo = Path.GetFileName(archivo);
            string extencion = Path.GetExtension(archivo);
            Console.WriteLine($"- {nombreArchivo} ({extencion})");
        }

        // Guardar el listado de archivos en un archivo CSV
        string csvFilePath = Path.Combine(@"C:\Clases\Modulo3\Taller de Lenguajes 1\RepositorioTaller1\tl1_tp8_2023-NazaretCS", "index.csv");
        GuardarArchivoCSV(csvFilePath, archivos);

        Console.WriteLine("Listado de archivos guardado en index.csv");
    }

    static void GuardarArchivoCSV(string filePath, string[] files)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Índice,Nombre,Extensión");

            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileName(files[i]);
                string fileExtension = Path.GetExtension(files[i]);
                writer.WriteLine($"{i + 1},{fileName},{fileExtension}");
            }
        }
    }
}
