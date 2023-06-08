namespace EspacioTarea;

public class Tarea {
  private int tareaId;
  private string descripcion;
  private int duracion;

  public int TareaId {
    get { return tareaId; }
  }

  public string Descripcion {
    get { return descripcion; }
  }

  public int Duracion {
    get { return duracion; }
  }

  public Tarea(int tareaId, string descripcion, int duracion) {
    this.tareaId = tareaId;
    this.descripcion = descripcion;
    this.duracion = duracion;
  }

  
}