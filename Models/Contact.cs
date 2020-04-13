using System;
using System.ComponentModel.DataAnnotations;

namespace sms_pitang_netcore.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public int IdOwner { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public int IdTarget { get; set; }

    }
}
