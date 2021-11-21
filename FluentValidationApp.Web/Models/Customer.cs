using System;
using System.Collections.Generic;

namespace FluentValidationApp.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public int Age { get; set; }
        
        public DateTime? BirthDay { get; set; }

        public IList<Address> Addresses { get; set; }

        public Gender Gender { get; set; }

        public string GetFullName() // Metodun AutoMapper ile dönüstürülmesi icin basina Get eki konur. Aksi durumda, manuel olarak dönüstürme islemi tanimlanmalidir.
        {
            return $"{Name} - {Email} - {Age}";
        }

        public string FullName2() 
        {
            return $"{Name} - {Email} - {Age}";
        }

        public CreditCard CreditCard { get; set; }
    }
}
