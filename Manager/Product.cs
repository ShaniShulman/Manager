using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class Product
    {


        public int AddNewProduct(string connectionString)
        {
            string category_id, product_name, description, price, picture, toContinue = "";
            int rowAffected = 0;
            while (toContinue != "n") { 
            Console.WriteLine("Enter category_id:");
            category_id = Console.ReadLine();
            Console.WriteLine("Enter product name:");
            product_name = Console.ReadLine();
            Console.WriteLine("Enter description:");
            description = Console.ReadLine();
            Console.WriteLine("Enter price:");
            price = Console.ReadLine();
            Console.WriteLine("Enter picture url:");
            picture = Console.ReadLine();

            string query = "INSERT INTO Product(category_id, product_name, description, price, picture)" +
                    "VALUES (@category_id, @product_name, @description, @price, @picture)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = int.Parse(category_id);
                cmd.Parameters.Add("@product_name", SqlDbType.NVarChar).Value = product_name;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = description;
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = int.Parse(category_id);
                cmd.Parameters.Add("@picture", SqlDbType.NVarChar).Value = picture;
                cn.Open();
                rowAffected += cmd.ExecuteNonQuery();
                cn.Close();

            }
                Console.WriteLine("Are you want to continue? (y/n)");
                toContinue = Console.ReadLine();
            }
            return rowAffected;
            
        }


        public void GetData(string connectionString)
        {
            string qury= "select p.id,p.product_name,p.description,p.price,p.picture,c.category_name\r\nfrom Product p join Category c\r\non p.category_id = c.id";
            using (SqlConnection cn = new SqlConnection(connectionString))
                using(SqlCommand cmd=new SqlCommand(qury,cn))
            {
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                    }
                    reader.Close();
                cn.Close();
  
            }
        }
    }
}
