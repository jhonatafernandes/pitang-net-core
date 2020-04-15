﻿using System.ComponentModel.DataAnnotations;

namespace sms_pitang_netcore.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 8 e 60 caracteres.")]
        [MinLength(8, ErrorMessage = "Este campo deve conter entre 8 e 60 caracteres.")]
        public string Password { get; set; }

       
        public string ImageUrl { get; set; }


        public string Role { get; set; }

    }
}