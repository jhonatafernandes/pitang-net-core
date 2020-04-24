using System;
using System.ComponentModel.DataAnnotations;

namespace Pitang.Sms.NetCore.Entities.Models
{
    public class Contact : AuditEntity
    {
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public int TargetId { get; set; }
        public User Target { get; set; }


    }
}
