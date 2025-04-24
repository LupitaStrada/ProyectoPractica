using System;
using System.Collections.Generic;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Resena
{
    public int Id { get; set; }

    public int? LibroId { get; set; }

    public int? UsuarioId { get; set; }

    public int Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public virtual Libro? Libro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
