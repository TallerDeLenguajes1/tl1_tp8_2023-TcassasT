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

    List<Tarea> tareasFinalizadas = new List<Tarea>();
    int decision = mostrarMenuYPedirOpcion();
    while (decision != 3) {
      switch(decision) {
        case 1:
          if (listaTareas.Count() == 0) {
            Console.WriteLine("\n¡No hay tareas pendientes!");
            break;
          }

          Tarea tareaAMover = moverTareaAFinalizadas(listaTareas);
          tareasFinalizadas.Add(tareaAMover);
          listaTareas.Remove(tareaAMover);
          break;
        case 2:
          break;
        case 3:
          break;
      }

      decision = mostrarMenuYPedirOpcion();
    }
  }

  static Tarea moverTareaAFinalizadas(List<Tarea> tareasPendientes) {
    Console.WriteLine("\nListado de tareas (" + tareasPendientes.Count() + "): ");

    foreach (var tarea in tareasPendientes) {
      mostrarTarea(tarea);
    }

    int tareaIdAMover;
    Console.Write("\nIngrese el ID de la tarea que desea mover a finalizadas:");

    if (!int.TryParse(Console.ReadLine(), out tareaIdAMover)) {
      Console.WriteLine("x Input invalido, intente nuevamente.");
      return moverTareaAFinalizadas(tareasPendientes);
    }

    int tareaListIndex = 0;
    Boolean tareaExistente = false;
    for (int i = 0; i < tareasPendientes.Count(); i++) {
      if (tareasPendientes[i].TareaId == tareaIdAMover) {
        tareaListIndex = i;
        tareaExistente = true;
      }
    }

    if (!tareaExistente) {
      Console.WriteLine("x Tarea inexistente, intente nuevamente.");
      return moverTareaAFinalizadas(tareasPendientes);
    }

    return tareasPendientes[tareaListIndex];
  }

  static int mostrarMenuYPedirOpcion() {
    Console.WriteLine("\nIngrese una opción para operar con las tareas ingresadas:");
    Console.WriteLine(" 1- Mover tarea pendiente a realizada");
    Console.WriteLine(" 2- Buscar tarea por descripcion");
    Console.WriteLine(" 3- Salir");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 3) {
        Console.WriteLine("x Opción invalida, intente nuevamente.");
        return mostrarMenuYPedirOpcion();
      }
      return decision;
    } else {
      Console.WriteLine("x Opción invalida, intente nuevamente.");
      return mostrarMenuYPedirOpcion();
    }
  }

  static Tarea crearTareaNueva(int tareaId) {
    Console.WriteLine("\n- Creando tarea nueva N° (" + tareaId + ")");
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
    Console.WriteLine("\n- Tarea N° " + tarea.TareaId);
    Console.WriteLine(" Duracion: " + tarea.Duracion);
    Console.WriteLine(" Descripción: " + tarea.Descripcion);
  }
}