using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ventas.Core.Entities.Models;

public partial class Usuario
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string NombreUsuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    public bool Activo { get; set; }
}
