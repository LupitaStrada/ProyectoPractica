using System;
using System.Collections.Generic;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Editoriale
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
