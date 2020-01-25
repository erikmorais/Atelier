using System;
using System.Data.SqlClient;

namespace AtelierEntertainment
{
    public class OrderDataContext
    {
        const string ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";

        public void CreateOrder(Order order)
        {
            var conn = new SqlConnection(ConnectionString);

            var cmd = conn.CreateCommand();

            cmd.CommandText = $"INSERT INTO dbo.Orders VALUES {order.Id}, {order.Customer.Id}, {order.Total}";

            cmd.ExecuteNonQuery();

            foreach (var item in order.Items)
            {
                cmd = conn.CreateCommand();

                cmd.CommandText = $"INSERT INTO dbo.OrderItems VALUES {order.Id}, {item.Code}, {item.Description}, {item.Price};";

                cmd.ExecuteNonQuery();
            }
        }

        public static Order LoadOrder(int id)
        {
            var conn = new SqlConnection(ConnectionString);

            var cmd = conn.CreateCommand();

            cmd.CommandText = $"SELECT * FROM dbo.Orders WHERE Id = {id}";

            var reader = cmd.ExecuteReader();
            
            var result = new Order { };

            result.Id = id;
            result.Total = reader.GetDecimal(2);

            cmd = conn.CreateCommand();

            cmd.CommandText = $"SELECT * FROM dbo.OrderItems WHERE OrderId = {id}";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Items.Add(new orderItem { Code = reader.GetString(1), Description = reader.GetString(2), Price = reader.GetFloat(2) });
            }
            
            return result;
        }
    }
}
