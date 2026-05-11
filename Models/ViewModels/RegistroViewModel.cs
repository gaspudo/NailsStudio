using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NailsStudio.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage ="Nome obrigatório")]
        public string NomeCompleto {get;set;} = string.Empty;

        [Required(ErrorMessage ="Email obrigatório")]
        [EmailAddress(ErrorMessage ="Email inválido")]
        public string Email {get;set;} = string.Empty;

        [Required(ErrorMessage ="Senha obrigatória")]
        [MinLength(8, ErrorMessage ="A senha precisa de 8 caracteres no mínimo")]
        [DataType(DataType.Password)]
        public string Senha {get;set;} = string.Empty;

        [Required(ErrorMessage ="Confirmação obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage ="As senhas devem ser iguais")]
        public string ConfirmarSenha {get;set;} = string.Empty;
    }
}