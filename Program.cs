using EspacioTarea;

internal class Program {
  private static void Main(string[] args) {
    List<Tarea> listaTareas = new List<Tarea>();

    int cantidadDeTareas;
    Console.Write("¿Cuantas tareas desea cargar?: ");
    if (!int.TryParse(Console.ReadLine(), out cantidadDeTareas)) {
      Console.WriteLine("x Input invalido, vuelva a intentar.");
      return;
    }

    for(int i = 0; i < cantidadDeTareas; i++) {
      listaTareas.Add(crearTareaNueva(i));
    }
    
    
  }

  static int mostrarMenuYPedirOpcion(Boolean volverAPedir) {
    Console.WriteLine("Ingrese una opción para operar con las tareas ingresadas:");
    Console.WriteLine(" 1- Mover tarea pendiente a realizada");
    Console.WriteLine(" 2- Buscar tarea por descripcion");
    Console.WriteLine(" 3- Salir");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 3) {
        Console.WriteLine("Opción invalida, ingrese una opción para operar con las tareas ingresadas:");
        return mostrarMenuYPedirOpcion(true);
      }
      return decision;
    } else {
      Console.WriteLine("Opción invalida, ingrese una opción para operar con las tareas ingresadas:");
      return mostrarMenuYPedirOpcion(true);
    }
  }

  static Tarea crearTareaNueva(int tareaId) {
    Console.WriteLine("- Creando tarea nueva N° (" + tareaId + ")");
    string descripcion = solicitaDescripcion();
    int duracion = solicitaDuracion();
    return new Tarea(tareaId, descripcion, duracion);
  }

  static string solicitaDescripcion() {
    Console.Write(" Ingrese la descripción: ");
    string? descripcion = Console.ReadLine();

    if (descripcion == null) {
      Console.WriteLine("x Descripción invalida, vuelva a intentar.");
      return solicitaDescripcion();
    }

    return descripcion;
  }

  static int solicitaDuracion() {
    Console.Write(" Ingrese la duración de entre (10 y 100): ");
    int duracion;

    if (!int.TryParse(Console.ReadLine(), out duracion) || duracion < 10 || duracion > 100) {
      Console.WriteLine("x Duración invalida, vuelva a intentar.");
      return solicitaDuracion();
    }

    return duracion;
  }

  static void mostrarTarea(Tarea tarea) {
    Console.WriteLine("- Tarea N° " + tarea.TareaId);
    Console.WriteLine(" Duracion: " + tarea.Duracion);
    Console.WriteLine(" Descripción: " + tarea.Descripcion);
  }
}