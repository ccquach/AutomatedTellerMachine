﻿using System;
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
        [Range(0.01, int.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
        public decimal Amount { get; set; }

        public int CheckingAccountId {
            get {
                return Convert.ToInt32(HttpContext.Current.User.Identity.GetUserId());
            }
        }
    }
}