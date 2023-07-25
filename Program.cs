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

            #region DepartmentSection

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

            #endregion

            #region ProductSection

            var productRepo = new DapperProductRepository(conn);

            #region UserInputUpdate--ToWorkOnLater
            //Console.WriteLine("New Product? \nYes   /    No");
            //string input2 = Console.ReadLine();
            //Console.WriteLine("What is the Product ID?");
            //int userRequest;
            //bool isInt = int.TryParse(Console.ReadLine(), out userRequest);
            //while (!isInt)
            //{
            //    Console.WriteLine("Please input a valid Product ID:");
            //    isInt = int.TryParse(Console.ReadLine(), out userRequest);
            //}
            //var productToUpdate = productRepo.GetProduct(userRequest);
            //Console.WriteLine("What would you like to update?");
            //Console.WriteLine("1. CategoryID \n2. Product Name \n3.Price \n4. Sale Status \n5. Amount in Stock \n (Make sure to comma seperate the ones you'd like. Eg. 1,3,4.)");
            //productRepo.UpdateProductsFunction(productToUpdate);

            //Product test = productRepo.GetProduct(1);
            //Console.WriteLine($"Product ID: {test.ProductID} | Category ID: {test.CategoryID} | \n" +
            //       $"{test.Name} | ${test.Price} | Sale Status: {test.OnSale} | Currently In Stock: {test.StockLevel}");
            #endregion

            var productToUpdate = productRepo.GetProduct(886);

            productToUpdate.Name = "Update: Zims Alien Adventures";
            productToUpdate.Price = 100;
            productToUpdate.StockLevel = 15;

            productRepo.UpdateProduct(productToUpdate);

            var products = productRepo.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductID} | Category ID: {product.CategoryID} | \n" +
                    $"{product.Name} | ${product.Price} | Sale Status: {product.OnSale} | Currently In Stock: {product.StockLevel}");
                Console.WriteLine();
            }

            #endregion
        }
    }
}