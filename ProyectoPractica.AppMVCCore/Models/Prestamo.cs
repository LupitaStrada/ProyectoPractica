using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProyectoPractica.AppMVCCore.Models;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? LibroId { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateOnly? FechaDevolucion { get; set; }
    [Required(ErrorMessage = "El campo Estado es obligatorio.")]
    public string Estado { get; set; } = null!;

    public virtual Libro? Libro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
