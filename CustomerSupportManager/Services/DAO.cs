using CustomerSupportManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustomerSupportManager.Services
{
    public class DAO
    {
        string connectionString = @"Data Source=WINDOWZ-SECKIE\SQLEXPRESS;Initial Catalog=CustomerSupportManager;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<TicketModel> getTickets()
        {
            List<TicketModel> tickets = new List<TicketModel>();

            string queryString = "select * from Tickets";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TicketModel ticket = new TicketModel();
                            ticket.Id = reader.GetInt32(0);
                            ticket.CustomerId = reader.GetInt32(1);
                            ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(3);
                            ticket.Status = reader.GetString(4);

                            tickets.Add(ticket);
                        }
                    }
                }
                catch (Exception e )
                {
                    Console.WriteLine(e.Message);
                }
            }

            return tickets;
        }

        public bool authenticateAdmin(AdminUserModel user)
        {
            bool success = false;

            string queryString = "select * from Admins where username = @username and password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                success = tryAuthenticate(connection, command);
            }

            return success;

        }

        public bool authenticateUser(UserModel user)
        {
            bool success = false;

            string queryString = "select * from Admins where username = @username and password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                success = tryAuthenticate(connection, command);
            }

            return success;

        }

        public bool tryAuthenticate(SqlConnection connection, SqlCommand command)
        {
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}