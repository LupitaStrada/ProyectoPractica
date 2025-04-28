using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Libro
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo TItulo es obligatorio.")]
    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "El campo Precio es obligatorio.")]
    public decimal Precio { get; set; }
    [Required(ErrorMessage = "El campo Stock es obligatorio.")]
    public int Stock { get; set; }

    [Display(Name="Editorial")]
    public int? EditorialId { get; set; }
    [Display(Name = "Autor")]
    public int? AutorId { get; set; }

    public DateOnly? FechaPublicacion { get; set; }

    public virtual Autore? Autor { get; set; }

    public virtual Editoriale? Editorial { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
