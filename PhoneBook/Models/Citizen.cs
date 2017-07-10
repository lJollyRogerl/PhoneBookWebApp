using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook
{
    [MetadataType(typeof(CitizenMetadata))]
    public partial class Citizen
    {
    }

    public class CitizenMetadata
    {
        [ScaffoldColumn(false)]
        public int CitizenId { get; set; }
        [Required(ErrorMessage = "Введите пожалуйста имя")]
        [Display(Name = "ФИО")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?){3,}", ErrorMessage = "Введите имя корректно, например: Иванов Иван Иванович")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пожалуйста номер телефона")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }

}