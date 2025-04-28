using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Editoriale
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
    [EmailAddress]
    public string? Email { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
