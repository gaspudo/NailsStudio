using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NailsStudio.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="E-mail obrigatório")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email {get;set;} = string.Empty;

        [Required(ErrorMessage ="Senha obrigatória")]
        [DataType(DataType.Password)]
        public string Senha {get;set;} = string.Empty;

        public bool LembrarMe {get;set;}
    }
}