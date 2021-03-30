using BAS.AppCommon.StaticValues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Database.Models
{
    public class Personnel
    {
        public long Id { get; set; }
        [MaxLength(StaticValues.PersonnelNameMaxLength)]
        [Required]
        public string Name { get; set; }
        [MaxLength(StaticValues.PersonnelSurnameMaxLength)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(StaticValues.PersonnelNationalityMaxLength)]
        [Required]
        public string Nationality { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public virtual List<MoviePersonnel> Movies { get; set; }
    }
}
