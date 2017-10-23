using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace AutomatedTellerMachine.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, int.MaxValue, ErrorMessage = "Please enter an amount greater than {1}")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "Enter an amount up to 2 decimal places")]
        public decimal Amount { get; set; }

        [Required]
        public int CheckingAccountId { get; set; }
    }
}