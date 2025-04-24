using System;
using System.Collections.Generic;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int? EditorialId { get; set; }

    public int? AutorId { get; set; }

    public DateOnly? FechaPublicacion { get; set; }

    public virtual Autore? Autor { get; set; }

    public virtual Editoriale? Editorial { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
