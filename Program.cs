using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using ORM;

namespace IntroSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var departmentRepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("New Department? \nYes  /   No");
            string input = Console.ReadLine();
            var convertInput = GetUserInput.ConvertInput(input);
                while (convertInput)
                {
                    string? newDepName = Console.ReadLine();
                    departmentRepo.InsertDepartment(newDepName);
                    Console.WriteLine("New Department? \nYes  /   No");
                    input = Console.ReadLine();                    
                    convertInput = GetUserInput.ConvertInput(input);
                }
            var deps = departmentRepo.GetAllDepartments();

            foreach (var department in deps)
            {
                Console.WriteLine($"{department.DepartmentID} | {department.Name} ");
                Console.WriteLine();
            }
        }
    }
}