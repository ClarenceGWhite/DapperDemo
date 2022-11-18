using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;

namespace DapperDemo
{
    public class DapperProductRepository : IProductRepository
    {

        private readonly IDbConnection _conn;
        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateProduct(string name, double price, int CategoryID)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@productName, @price, @categoryID);",
                new { name = name, price = price, categoryID = CategoryID });
        }

        public IEnumerable<Product> GetAllProducts() 
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        
        
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", 
            new { id = id });
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products" +
                          " SET name = @name," + 
                          " Price = @price," +
                          " CategoryID = @catID," +
                          " OnSale = @onSale," +
                          " StockLevel = @stock" +
                          " WHERE ProductID = @id;",
                          new {
                                id = product.ProductID,
                                name = product.Name,
                                price = product.Price,
                                catID = product.CategoryID,
                                onSale = product.OnSale,
                                stock = product.StockLevel
                               });

        }
    }
}
