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
    while (decision != 4) {
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
          buscarTareaPorDescripcion(listaTareas);
          break;
        case 3:
          guardarSumatoriaHorasTrabajadasEnArchivo(tareasFinalizadas);
          break;
        case 4:
          break;
      }

      decision = mostrarMenuYPedirOpcion();
    }
  }

  static void guardarSumatoriaHorasTrabajadasEnArchivo(List<Tarea> tareasRealizadas) {
    DateTime hoy = DateTime.Now;

    using (StreamWriter sw = new StreamWriter("tareas-realizadas-" + hoy.ToShortDateString().Replace("/", "-") + ".txt")) {
      int total = 0;
      sw.WriteLine("Sumatoria de tareas finalizadas:");
      foreach (var tarea in tareasRealizadas) {
        sw.WriteLine("\tx Tarea ID° " + tarea.TareaId + ", duración: " + tarea.Duracion);
        total += tarea.Duracion;
      }
      sw.WriteLine("\nDuración total: " + total);
    }
  }

  static void buscarTareaPorDescripcion(List<Tarea> listaTareas) {
    Console.Write("\n-Ingrese una descripcion o palabra a buscar:");
    string? descripcionABuscar = Console.ReadLine();

    List<int> tareasCoincidientes = new List<int>();
    if (descripcionABuscar == null) {
      Console.WriteLine("x Descripcion invalida, vuelva a intentar.");
      buscarTareaPorDescripcion(listaTareas);
    } else {
      for (int i = 0; i < listaTareas.Count(); i++) {
        if (listaTareas[i].Descripcion.Contains(descripcionABuscar)) {
          tareasCoincidientes.Add(i);
        }
      }
    }

    if (tareasCoincidientes.Count() > 0) {
      Console.WriteLine("Se encontraron tareas coincidientes:");
      foreach (var tareaId in tareasCoincidientes) {
        mostrarTarea(listaTareas[tareaId]);
      }
    } else {
      Console.WriteLine("No se encontraron coincidencias.");
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
    Console.WriteLine("\n-- Ingrese una opción para operar con las tareas ingresadas: --");
    Console.WriteLine(" 1- Mover tarea pendiente a realizada");
    Console.WriteLine(" 2- Buscar tarea por descripcion");
    Console.WriteLine(" 3- Volcar tareas realizadas en archivo de texto");
    Console.WriteLine(" 4- Salir");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 4) {
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
    Console.WriteLine("\n- Creando tarea nueva ID° (" + tareaId + ")");
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
    Console.WriteLine("\n-Tarea ID° " + tarea.TareaId);
    Console.WriteLine(" Duracion: " + tarea.Duracion);
    Console.WriteLine(" Descripción: " + tarea.Descripcion);
  }
}