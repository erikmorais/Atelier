using AtelierEntertainment.Entities;
using AtelierEntertainmentEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AtelierEntertainmentEntities
{
    public class OrderDataContext : IOrderRepository
    {
        const string ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";

        public void CreateOrder(Order order)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO dbo.Orders( id, customerId ,Total, TotalTax ) VALUES  (@orderId, @customerId, @total, @totalTax) ";

                    cmd.Parameters.AddWithValue("@orderId", order.Id);
                    cmd.Parameters.AddWithValue("@customerId", order.Customer.Id);
                    cmd.Parameters.AddWithValue("@total", order.Total);
                    cmd.Parameters.AddWithValue("@totalTax", order.TotaTax);

                    cmd.ExecuteNonQuery();

                    foreach (var item in order.Items)
                    {
                        using (var cmdItem = conn.CreateCommand())
                        {

                            cmdItem.CommandText = $"INSERT INTO dbo.OrderItems VALUES @orderId, @itemCode, @itemDescription, @itemPrice;";
                            cmdItem.Parameters.AddWithValue("@orderId", order.Id);
                            cmdItem.Parameters.AddWithValue("@itemCode", item.Code);
                            cmdItem.Parameters.AddWithValue("@itemDescription", item.Description);
                            cmdItem.Parameters.AddWithValue("@itemPrice", item.Price);
                            cmdItem.ExecuteNonQuery();

                        }

                    }
                }
            }
        }

        public Order GetSingleOrder(int id)
        {
            var result = new Order { };
            using (var conn = new SqlConnection(ConnectionString))
            {

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                              Id
                                            ,Total
                                            ,Customer_Id
                                            ,TotalTax
                                     FROM dbo.Orders WHERE Id = @orderId";

                    cmd.Parameters.AddWithValue("@orderId", id);

                    var reader = cmd.ExecuteReader();

                    result.Id = id;
                    result.Total = reader.GetDouble(reader.GetOrdinal("Total"));
                    using (var cmdItem = conn.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT 
                                                 Code
                                                ,Quantity
                                                ,Price
                                                ,Description
                                                ,Order_Id
                                          FROM dbo.OrderItems WHERE OrderId = @orderId";
                        cmd.Parameters.AddWithValue("@orderId", id);

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            result.Items.Add(new orderItem
                            {
                                Code = reader.GetString(reader.GetOrdinal("Code")),
                                Quantity = reader.GetDouble(reader.GetOrdinal("Quantity")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Price = reader.GetFloat(reader.GetOrdinal("Price"))
                            });
                        }
                    }

                }
            }
            return result;
        }

        // TODO: implement GetOrderByCustomer
        public IList<Order> GetOrdersByCustomer(Customer customer)
        {
            var result = new List<Order>();
            //using (var conn = new SqlConnection(ConnectionString))
            //{

            //    using (var cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = @"SELECT 
            //                                  Id
            //                                ,Total
            //                                ,Customer_Id
            //                                ,TotalTax
            //                         FROM dbo.Orders WHERE Customer_Id  = @customerId";

            //        cmd.Parameters.AddWithValue("@customerId", customer.Id);

            //        var reader = cmd.ExecuteReader();

            //        result.Customer = customer;
            //        result.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            //        result.Total = reader.GetDouble(reader.GetOrdinal("Total"));
            //        using (var cmdItem = conn.CreateCommand())
            //        {

            //            cmd.CommandText = @"SELECT 
            //                                     Code
            //                                    ,Quantity
            //                                    ,Price
            //                                    ,Description
            //                                    ,Order_Id
            //                              FROM dbo.OrderItems WHERE OrderId = @orderId";
            //            cmd.Parameters.AddWithValue("@orderId", id);

            //            reader = cmd.ExecuteReader();

            //            while (reader.Read())
            //            {
            //                result.Items.Add(new orderItem
            //                {
            //                    Code = reader.GetString(reader.GetOrdinal("Code")),
            //                    Quantity = reader.GetDouble(reader.GetOrdinal("Quantity")),
            //                    Description = reader.GetString(reader.GetOrdinal("Description")),
            //                    Price = reader.GetFloat(reader.GetOrdinal("Price"))
            //                });
            //            }
            //        }

            //    }
            //}
            return result;
        }
    }
}
