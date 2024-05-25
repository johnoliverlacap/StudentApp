using ConsoleTableExt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            Get();
            //Post();
            //Delete(2);
            //Put(10);
        }

        public static void Get()
        {
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync("https://test102.ustangelicum.edu.ph/api/student").Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result;

            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(responseBody);

            List<StudentDTO> studentDto = students
                .Select(m => new StudentDTO()
                {
                    Id = m.Id,
                    StudentNumber = m.StudentNumber,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Section = m.Section,
                    EmailAddress = m.Contact.EmailAddress,
                    MobileNumber = m.Contact.MobileNumber,
                    PresentAddress = m.Address.PresentAddress,
                    PermanentAddress = m.Address.PermanentAddress
                })
                .ToList();

            ConsoleTableBuilder
                .From(studentDto)
                .ExportAndWriteLine();
           
            Console.ReadLine();
        }

        public static void Post()
        {
            Student student = new Student()
            {
                StudentNumber = "000001",
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Section = "Section 1",
                Contact = new Contact() 
                { 
                    EmailAddress = "test@gmail.com",
                    MobileNumber = "1234567890"
                },
                Address = new Address()
                { 
                    PresentAddress = "Present Address 1",
                    PermanentAddress = "Permanent Address 2"
                }
            };

            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = httpClient.PostAsync("https://test102.ustangelicum.edu.ph/api/student", content).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Student added");
            }

            Console.ReadLine();
        }

        public static void Delete(int id)
        {
            HttpResponseMessage httpResponseMessage = httpClient.DeleteAsync($"https://test102.ustangelicum.edu.ph/api/student/{id}").Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Student deleted");
            }

            Console.ReadLine();
        }

        public static void Put(int id)
        {
            Student student = new Student()
            {
                StudentNumber = "000001",
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Section = "Section 1",
                Contact = new Contact()
                {
                    EmailAddress = "test@gmail.com",
                    MobileNumber = "1234567890"
                },
                Address = new Address()
                {
                    PresentAddress = "Present Address 1",
                    PermanentAddress = "Permanent Address 2"
                }
            };

            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = httpClient.PutAsync($"https://test102.ustangelicum.edu.ph/api/student/{id}", content).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Student updated");
            }

            Console.ReadLine();
        }
    }
}
