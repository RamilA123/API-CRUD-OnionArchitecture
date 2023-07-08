using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Company
{
    public class CompanyUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
