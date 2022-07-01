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
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);

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

        public TicketModel getTicket(int Id)
        {
            string queryString = "select * from Tickets WHERE Id = @id";

            TicketModel ticket = new TicketModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ticket.Id = reader.GetInt32(0);
                            ticket.CustomerId = reader.GetInt32(1);
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return ticket;
        }

        public int createOrUpdateTicket(TicketModel ticketModel)
        {
            //string queryString = "INSERT INTO Tickets Values(@CustomerId, @Product, @Category, @Status)";
            string queryString = "";

            if (ticketModel.Id <= 0)
            {
                queryString = "INSERT INTO Tickets Values(@CustomerId, @Category, @Status)";
            }
            else
            {
                queryString = "Update Tickets SET CustomerId = @CustomerId, Category = @Category, Status = @Status WHERE Id = @Id";
            }

            TicketModel ticket = new TicketModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = ticketModel.Id;
                command.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = ticketModel.CustomerId;
                //command.Parameters.Add("@Product", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Product;
                command.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Category;
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Status;

                connection.Open();
                int newID = command.ExecuteNonQuery();

                return newID;
            }

        }

        public void addMessage(int ticketId, string message)
        {
            // Add message to ticket
        }

        public void changeCategory(int ticketId, string category)
        { 
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