using System;
using System.ComponentModel.DataAnnotations;

namespace Pitang.Sms.NetCore.Entities.Models
{
    public class HistoricPassword : AuditEntity
    {

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo deve ser maior ou igual a 1")]
        public int UserId { get; set; }
        public User User { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Password { get; set; }

        public DateTime Alteration { get; set; }
        public HistoricPassword()
        {
            this.Alteration = DateTime.Now;
        }
    }
}
