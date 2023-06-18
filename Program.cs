internal class Program {
  private static void Main(string[] args) {
    string directorioAListar = getRuta();

    List<string> archivos = Directory.GetFiles(directorioAListar).ToList();
    List<string> lineasCSV = new List<string>();
    
    int indiceRegitro = 0;
    archivos.ForEach(archivo => {
        lineasCSV.Add(getLineaCSV(indiceRegitro, archivo));
        indiceRegitro++;
    });

    File.WriteAllLines(@"index.csv", lineasCSV);

    Console.WriteLine("");
    Console.WriteLine("Se registraron " + archivos.Count() + " archivos, revise el archivo .index.csv");
  }

  static string getLineaCSV(int index, string pathArchivo) {
    string lineaCSV = "";
    lineaCSV += index + ",";
    lineaCSV += Path.GetFileNameWithoutExtension(pathArchivo) + ",";
    lineaCSV += Path.GetExtension(pathArchivo) + "";
    return lineaCSV;
  }

  static string getRuta() {
    Console.WriteLine("Ingrese la ruta de la cual quiere listar sus archivos:");
    string? directorioAListar = Console.ReadLine();

    if (directorioAListar == null) {
      Console.WriteLine("x Ruta invalida, porfavor reintente.");
      return getRuta();
    }

    if (!Directory.Exists(directorioAListar)) {
      Console.WriteLine("x Ruta apunta a una dirección inexistente, por favor reintente.");
      return getRuta();
    }
    
    return directorioAListar;
  }
}
