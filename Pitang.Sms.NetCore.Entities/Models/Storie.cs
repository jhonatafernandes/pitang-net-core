using System;
using System.ComponentModel.DataAnnotations;

namespace Pitang.Sms.NetCore.Entities.Models
{
    public class Storie : AuditEntity
    {

        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Publicate { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public char Type { get; set; }

        public Storie()
        {
            this.Publicate = DateTime.Now;
        }

    }

    
}
