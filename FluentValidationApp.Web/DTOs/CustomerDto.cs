using System;
using System.Globalization;

namespace FluentValidationApp.Web.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string FullName { get; set; }

        public string FullName2 { get; set; }

        //public string CCNumber { get; set; } // Complex Tipin bir property'sini istersek, kaynak nesnedeki ilgili property'inin türü gecilir, devaminda property adi ve ilgili complex tipin ilgili property adi PascalCase olarak yazilir. string CreditCard+Number. AutoMapper bunu otomatik taniyacaktir. Bu isleme Flattening denir. Complex bir türü basit bir türe cevirmeye yarar.

        //public DateTime CCValidDate { get; set; } 

        //Eger AutoMapper profile de IncludeMembers() metodunu kullanmak istersek, karmasik tipin property isimleri aynen buraya gecilir. Profile tarafinda dönüstürülür.
        public string Number { get; set; } 

        public DateTime ValidDate { get; set; }
    }
}
