using System.ComponentModel.DataAnnotations;
using PruebaTecnica.Validaciones;

namespace PruebaTecnica.Models
{
    public class Visitas
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El campo comercial es obligatorio.")]
        public string? Comercial { get; set; }

        [Required(ErrorMessage = "El campo cliente es obligatorio.")]
        public string? Cliente { get; set; }
        
        public string? DireccionCliente { get; set; }

        public string? PersonaContactoCliente { get; set; }

        [Required(ErrorMessage = "El campo telefono de cliente es obligatorio.")]
        [MinLength(9, ErrorMessage = "El campo telefono de cliente no puede tener menos 9 caracteres.")]
        public string? TelefonoCliente { get; set; }

        [Required(ErrorMessage = "El campo email es obligatorio.")]
        public string? EmailCliente { get; set; }

        [Required(ErrorMessage = "La fecha de visita es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Visita")]
        public DateTime? FechaVisita { get; set; }

        [DataType(DataType.Time)]
        public DateTime? HoraInicio { get; set; }

        [DataType(DataType.Time)]
        public DateTime? HoraFin { get; set; }

        public bool PedidoRealizado { get; set; }

        public decimal? ImportePedidoRealizado { get; set; }

        public string? Notas { get; set; }

    }
}
