using AtelierEntertainment.Entities;
using AtelierEntertainmentEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AtelierEntertainmentEntities
{
    public class OrderDataContext
    {
        private readonly string _connectionString;// = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";
        const string ConnectionString = "Server=DESKTOP-SFC808U;Database=Atelier;Integrated Security=true;";
        public OrderDataContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        /// <summary>
        /// Method refactored
        /// fixed Memory leak
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Order LoadOrder(int id)
        {
            var result = new Order { };

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    // cmd.CommandText = $"SELECT * FROM dbo.Orders WHERE Id = {id}";
                    cmd.CommandText = @"SELECT 
                                              Id
                                            ,Total
                                            ,Customer_Id
                                            ,TotalTax
                                     FROM dbo.Orders WHERE Id = @orderId";

                    cmd.Parameters.AddWithValue("@orderId", id);

                    var reader = cmd.ExecuteReader();
                    reader.Read();

                    result.Id = id;
                    result.Total = reader.GetDecimal(reader.GetOrdinal("Total")); //reader.GetDecimal(2);
                    conn.Close();
                    conn.Open();
                    using (var cmdItem = conn.CreateCommand())
                    {
                        //cmd.CommandText = $"SELECT * FROM dbo.OrderItems WHERE OrderId = {id}";
                        cmdItem.CommandText = @"SELECT 
                                                 Code
                                                ,Quantity
                                                ,Price
                                                ,Description
                                                ,Order_Id
                                          FROM dbo.OrderItems WHERE Order_Id = @orderId";

                        cmdItem.Parameters.AddWithValue("@orderId", id);

                        var readerItems = cmdItem.ExecuteReader();
                        result.Items = new List<orderItem>();

                        while (readerItems.Read())
                        {
                            result.Items.Add(new orderItem
                            {
                                Code = readerItems.GetString(readerItems.GetOrdinal("Code")),
                                Quantity = readerItems.GetDecimal(readerItems.GetOrdinal("Quantity")),
                                Description = readerItems.GetString(readerItems.GetOrdinal("Description")),
                                Price = readerItems.GetDecimal(readerItems.GetOrdinal("Price"))
                            });
                        }
                    }
                    conn.Close();
                }
            }
            return result;
        }

        public void CreateOrder(Order order)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"INSERT INTO dbo.Orders( id, Customer_Id ,Total, TotalTax ) VALUES  (@orderId, @customerId, @total, @totalTax) ";

                    cmd.Parameters.AddWithValue("@orderId", order.Id);
                    cmd.Parameters.AddWithValue("@customerId", order.Customer.Id);
                    cmd.Parameters.AddWithValue("@total", order.Total);
                    cmd.Parameters.AddWithValue("@totalTax", order.TotaTax);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    foreach (var item in order.Items)
                    {
                        using (var cmdItem = conn.CreateCommand())
                        {
                            conn.Open();
                            cmdItem.CommandText = $"INSERT INTO dbo.OrderItems(Code ,Price ,Description ,Order_Id ,Quantity ) VALUES (@itemCode  , @itemPrice, @itemDescription, @orderId , @quantity )";
                            cmdItem.Parameters.AddWithValue("@orderId", order.Id);
                            cmdItem.Parameters.AddWithValue("@itemCode", item.Code);
                            cmdItem.Parameters.AddWithValue("@itemDescription", item.Description);
                            cmdItem.Parameters.AddWithValue("@itemPrice", item.Price);
                            cmdItem.Parameters.AddWithValue("@quantity", item.Quantity);
                            cmdItem.ExecuteNonQuery();
                            conn.Close();

                        }

                    }
                }
            }
        }

        public async Task CreateOrderAsync(Order order)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();
                    cmd.CommandText = $"INSERT INTO dbo.Orders( id, Customer_Id ,Total, TotalTax ) VALUES  (@orderId, @customerId, @total, @totalTax) ";

                    cmd.Parameters.AddWithValue("@orderId", order.Id);
                    cmd.Parameters.AddWithValue("@customerId", order.Customer.Id);
                    cmd.Parameters.AddWithValue("@total", order.Total);
                    cmd.Parameters.AddWithValue("@totalTax", order.TotaTax);

                    await cmd.ExecuteNonQueryAsync();

                    conn.Close();
                    foreach (var item in order.Items)
                    {
                        using (var cmdItem = conn.CreateCommand())
                        {
                            await conn.OpenAsync();
                            cmdItem.CommandText = $"INSERT INTO dbo.OrderItems(Code ,Price ,Description ,Order_Id ,Quantity ) VALUES (@itemCode  , @itemPrice, @itemDescription, @orderId , @quantity )";
                            cmdItem.Parameters.AddWithValue("@orderId", order.Id);
                            cmdItem.Parameters.AddWithValue("@itemCode", item.Code);
                            cmdItem.Parameters.AddWithValue("@itemDescription", item.Description);
                            cmdItem.Parameters.AddWithValue("@itemPrice", item.Price);
                            cmdItem.Parameters.AddWithValue("@quantity", item.Quantity);
                            await cmdItem.ExecuteNonQueryAsync();

                            conn.Close();

                        }
                    }
                }
            }
        }

        public async Task<Order> GetSingleOrder(int id)
        {
            var result = new Order { };

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();
                    // cmd.CommandText = $"SELECT * FROM dbo.Orders WHERE Id = {id}";
                    cmd.CommandText = @"SELECT 
                                              Id
                                            ,Total
                                            ,Customer_Id
                                            ,TotalTax
                                     FROM dbo.Orders WHERE Id = @orderId";

                    cmd.Parameters.AddWithValue("@orderId", id);

                    var reader = await cmd.ExecuteReaderAsync();

                    await reader.ReadAsync();

                    result.Id = id;
                    result.Total = reader.GetDecimal(reader.GetOrdinal("Total")); //reader.GetDecimal(2);

                    conn.Close();

                    await conn.OpenAsync();

                    using (var cmdItem = conn.CreateCommand())
                    {
                        //cmd.CommandText = $"SELECT * FROM dbo.OrderItems WHERE OrderId = {id}";
                        cmdItem.CommandText = @"SELECT 
                                                 Code
                                                ,Quantity
                                                ,Price
                                                ,Description
                                                ,Order_Id
                                          FROM dbo.OrderItems WHERE Order_Id = @orderId";

                        cmdItem.Parameters.AddWithValue("@orderId", id);

                        var readerItems = await cmdItem.ExecuteReaderAsync();

                        result.Items = new List<orderItem>();

                        while (readerItems.Read())
                        {
                            result.Items.Add(new orderItem
                            {
                                Code = readerItems.GetString(readerItems.GetOrdinal("Code")),
                                Quantity = readerItems.GetDecimal(readerItems.GetOrdinal("Quantity")),
                                Description = readerItems.GetString(readerItems.GetOrdinal("Description")),
                                Price = readerItems.GetDecimal(readerItems.GetOrdinal("Price"))
                            });
                        }
                    }
                    conn.Close();
                }
            }
            return result;
        }


        public async Task<IList<orderItem>> GetOrderItems(int orderId)
        {
            var items = new List<orderItem>();
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmdItem = conn.CreateCommand())
                {
                    cmdItem.CommandText = @"SELECT 
                                                 Code
                                                ,Quantity
                                                ,Price
                                                ,Description
                                                ,Order_Id
                                          FROM dbo.OrderItems WHERE Order_Id = @orderId";

                    cmdItem.Parameters.AddWithValue("@orderId", orderId);

                    var readerItems = await cmdItem.ExecuteReaderAsync();

                    while (await readerItems.ReadAsync())
                    {
                        items.Add(new orderItem
                        {
                            Code = readerItems.GetString(readerItems.GetOrdinal("Code")),
                            Quantity = readerItems.GetDecimal(readerItems.GetOrdinal("Quantity")),
                            Description = readerItems.GetString(readerItems.GetOrdinal("Description")),
                            Price = readerItems.GetDecimal(readerItems.GetOrdinal("Price"))
                        });
                    }
                }

                conn.Close();
            }
            return items;
        }
        // TODO: Move it to another service
        public async Task<Customer> GetCustumer(int id)
        {
            var customer = new Customer();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();
                    cmd.CommandText = @"SELECT 
                                              Id
                                            ,Country
                                     FROM dbo.Customer WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    customer.Id = id;
                    customer.Country = reader.GetString(reader.GetOrdinal("Country"));

                }
            }
            return customer;
        }
        // TODO: Implement error handling
        // 
        public async Task<IList<Order>> GetOrdersByCustomer(Customer customer)
        {
            var result = new List<Order>();
            var orderItemsTasks = new List<Task<List<orderItem>>>();

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                              Id
                                            ,Total
                                            ,Customer_Id
                                            ,TotalTax
                                     FROM dbo.Orders WHERE Customer_Id = @orderId";

                    cmd.Parameters.AddWithValue("@orderId", customer.Id);

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var order = new Order
                        {
                            Customer = customer,
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Total = reader.GetDecimal(reader.GetOrdinal("Total")) //reader.GetDecimal(2);
                        };

                        result.Add(order);
                    }

                }
                conn.Close();
            }

            foreach (var item in result)
            {
                var orderItems = await this.GetOrderItems(item.Id);
                item.Items = orderItems.ToList<orderItem>();
            }

            return result;
        }
    }
}
