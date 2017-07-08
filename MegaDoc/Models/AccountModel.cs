using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MegaDoc.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage ="Не корректно введены данные")]
        [Display(Name = "Имя")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Не корректно введены данные")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Не корректно введены данные")]
        [Display(Name = "Логин")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Не корректно введены данные")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}