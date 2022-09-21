using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitCard.Models
{
    public class Address
    {
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        /*/public Address(string street,string city)
        {
            this.Street = street;
            this.City = city;
        }/**/

    }
}
