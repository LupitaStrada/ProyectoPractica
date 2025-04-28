using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Autore
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public string? Biografia { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
