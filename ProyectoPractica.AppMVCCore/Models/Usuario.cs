using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPractica.AppMVCCore.Models;

public partial class Usuario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo Nombre de usuario es obligatorio.")]
    [Display(Name = "Usuario")]
    public string NombreUsuario { get; set; } = null!;
    [Display(Name = "Contraseña")]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "el password debe tener entre 5 y 50 caracteres")]
    public string Contrasena { get; set; } = null!;
   
    [EmailAddress]
    public string Email { get; set; } = null!;
 
    public string Rol { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
