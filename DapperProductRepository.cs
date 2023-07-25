using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DapperProductRepository : IProductsRepository
    {
        private readonly IDbConnection _conn;
        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("select * from products where ProductID = @id;",
                new { id = id });
        }
        public void UpdateProduct(Product product)
        {
            _conn.Execute("update products"     +
                        " set Name = @name,"    +
                        " Price = @price,"      +
                        " CategoryID = @catID," +
                        " OnSale = @onSale,"    +
                        " StockLevel = @stock"  +
                        " where ProductID = @id;",
                        new { 
                            id = product.ProductID,
                            name = product.Name, 
                            price = product.Price, 
                            catID = product.CategoryID, 
                            onSale = product.OnSale, 
                            stock = product.StockLevel 
                            });
        }

        #region UserUpdateMethod(s)--ToWorkOnLater
        //public void UpdateSingleProductColumn(string columnName, string newvalue, int id) 
        //{
        //    _conn.Execute("update products set " + columnName + " = @value where ProductID = @id", new { value = newvalue, id = id });

        //}
        //public void UpdateProductsFunction(Product product)
        //{
        //    string input = Console.ReadLine();

        //    string[] numbersAsString = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //    //int id = product.ProductID;

        //    foreach (string numberString in numbersAsString)
        //    {
        //        if (int.TryParse(numberString, out int number))
        //        {
        //            switch (number)
        //            {
        //                case 1:
        //                    Console.WriteLine("What is the new Category ID?");
        //                    int cID;
        //                    bool isInt = int.TryParse(Console.ReadLine(), out cID);
        //                    while (!isInt)
        //                    {
        //                        Console.WriteLine("Please input a valid number:");
        //                        isInt = int.TryParse(Console.ReadLine(), out cID);
        //                    }
        //                    _conn.Execute("update products set CategoryID = @catID where ProductID = @id;",
        //                    new { id = product.ProductID, cID = product.CategoryID } );

        //                    break;

        //                case 2:
        //                    Console.WriteLine("What is the new Product Name?");
        //                    string name = Console.ReadLine();
        //                    _conn.Execute("update products set Name = @name where ProductID = @id;",
        //                    new { id = product.ProductID, name = product.Name });
        //                    break;

        //                case 3:
        //                    Console.WriteLine("What is the new Price?");
        //                    double price;
        //                    bool isDouble = double.TryParse(Console.ReadLine(), out price);
        //                    while (!isDouble)
        //                    {
        //                        Console.WriteLine("Please input a valid number:");
        //                        isDouble = double.TryParse(Console.ReadLine(), out price);
        //                    }
        //                    _conn.Execute("update products set Price = @price where ProductID = @id;",
        //                    new { id = product.ProductID, price = product.Price });
        //                    break;

        //                case 4:
        //                    Console.WriteLine("What is the new Sale Status?");
        //                    string onSale = Console.ReadLine();
        //                    bool onSaleStatus;
        //                    onSale.ToLower();
        //                    if (onSale == "yes" || onSale == "true" || onSale == "1")
        //                    {
        //                        onSaleStatus = true;
        //                        _conn.Execute("update products set OnSale = @onSaleStatus where ProductID = @id;",
        //                         new { id = product.ProductID, onSaleStatus = product.OnSale });
        //                    }
        //                    else
        //                    {
        //                        onSaleStatus = false;
        //                        _conn.Execute("update products set OnSale = @onSaleStatus where ProductID = @id;",
        //                        new { id = product.ProductID, onSaleStatus = product.OnSale });
        //                    }
        //                    break;

        //                case 5:
        //                    Console.WriteLine("How much is in Stock?");
        //                    // Add more statements to execute for case 2
        //                    break;

        //                // Add more cases as needed...

        //                default:
        //                    Console.WriteLine($"You entered an unrecognized number: {number}.");
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Invalid input: '{numberString}' is not a valid number.");
        //        }
        //    }
        //}



        //// Will work on later.
        //// public void CreateProduct(string name, double price, int categoryID)
        //// {
        ////    _conn.Execute("INSERT INTO products (Name) = (@productName);",
        ////        new { productName = name });
        ////    _conn.Execute("INSERT INTO products (Price) = (@productPrice);",
        ////        new { productPrice = price });
        ////    _conn.Execute("INSERT INTO products (CategoryID) = (@newCategoryID);",
        ////        new { newCategoryID = categoryID });
        //// }

        #endregion
        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("select * from products;");
        }
    }
}
