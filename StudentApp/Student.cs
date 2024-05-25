using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Section { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }

    public class Contact
    {
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
    }

    public class Address
    {
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }
}
