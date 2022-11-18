using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var productRepository = new DapperProductRepository(conn);

            

            var productToUpdate = productRepository.GetProduct(887);

            productToUpdate.Name = "BelVita Cinnamon Brown Sugar Breakfast Biscuits";
            productToUpdate.Price = 1.89;
            productToUpdate.CategoryID = 10;
            productToUpdate.OnSale = false;
            productToUpdate.StockLevel = 380;

            productRepository.UpdateProduct(productToUpdate);


            var products = productRepository.GetAllProducts();
            foreach (var product in products) 
            {
                Console.WriteLine($"ProductID:   {product.ProductID}");
                Console.WriteLine($"Name:        {product.Name}");
                Console.WriteLine($"Price:       {product.Price}");
                Console.WriteLine($"Category ID: {product.CategoryID}");
                Console.WriteLine($"OnSale:      {product.OnSale}");
                Console.WriteLine($"Stock Level: {product.StockLevel}");
                Console.WriteLine();
                Console.WriteLine();
            }
           



            
          




        }
    }
}