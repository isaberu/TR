using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Validaciones
{
    public class FechaNoFuturaAttribute : ValidationAttribute
    {
        public FechaNoFuturaAttribute() : base("La fecha no puede ser futura.")
        {
            // Definir el mensaje por defecto si la fecha es futura
            ErrorMessage = "La fecha no puede ser futura.";
        }

        // Método de validación que verifica si la fecha es futura
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime fecha)
            {
                // Verifica si la fecha es futura
                if (fecha > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}