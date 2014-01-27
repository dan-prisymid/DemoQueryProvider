using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DemoQueryProvider
{
    // http://blogs.msdn.com/b/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx

    //public class Customers
    //{
    //    public string CustomerID;
    //    public string ContactName;
    //    public string Phone;
    //    public string City;
    //    public string Country;
    //}

    //public class Orders
    //{
    //    public int OrderID;
    //    public string CustomerID;
    //    public DateTime OrderDate;
    //}

    //public class Northwind
    //{
    //    public Query<Customers> Customers;
    //    public Query<Orders> Orders;

    //    public Northwind(DbConnection connection)
    //    {
    //        QueryProvider provider = new DbQueryProvider(connection);
    //        this.Customers = new Query<Customers>(provider);
    //        this.Orders = new Query<Orders>(provider);
    //    }
    //}

    public class Product
    {
        public string Cfn;
        public int Version;
    }

    public class Order
    {
        public string OrderNumber;
        public string Cfn;
    }

    public class ProductLabelling
    {
        public Query<Product> Products;
        public Query<Order> Orders;

        public ProductLabelling(DbConnection connection)
        {
            QueryProvider provider = new DbQueryProvider(connection);
            this.Products = new Query<Product>(provider);
            this.Orders = new Query<Order>(provider);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string constr = @"";
                using (var con = new OracleConnection(constr))
                {
                    con.Open();
                    var db = new ProductLabelling(con);

                    IQueryable<Product> query =
                         db.Products.Where(p => p.Cfn == "X0404011");

                    Console.WriteLine("Query:\n{0}\n", query);

                    var list = query.ToList();
                    foreach (var item in list)
                    {
                        Console.WriteLine("Cfn: {0} v{1}", item.Cfn, item.Version);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
