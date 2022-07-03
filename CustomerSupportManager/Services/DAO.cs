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
                            ticket.Title = reader.GetString(4);

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
                            ticket.Title = reader.GetString(4);
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
            if (ticketModel.Id <= 0)
            {
                return createTicket(ticketModel);
            }
            else
            {
                return updateTicket(ticketModel);
            }
        }

        public int createTicket(TicketModel ticketModel)
        {
            string queryString = "INSERT INTO Tickets Values(@CustomerId, @Category, @Status, @Title); select CAST(scope_identity() AS int);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = ticketModel.CustomerId;
                command.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Category;
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Status;
                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Title;

                connection.Open();
                int newID = Convert.ToInt32(command.ExecuteScalar());

                return newID;
            }

        }

        public int updateTicket(TicketModel ticketModel)
        {
            string queryString = "Update Tickets SET CustomerId = @CustomerId, Category = @Category, Status = @Status WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = ticketModel.Id;
                command.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = ticketModel.CustomerId;
                command.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Category;
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Status;
                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Title;

                connection.Open();
                command.ExecuteNonQuery();

                return ticketModel.Id;
            }

        }

        internal void deleteTicket(int id)
        {
            string queryString = "DELETE FROM Tickets WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void addMessage(int ticketId, string message, string userType = "", int userId = 0)
        {
            // Add message to ticket

            string queryString = "INSERT INTO Messages Values(@TicketId, @Message, @UserType, @UserId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@TicketId", System.Data.SqlDbType.Int).Value = ticketId;
                command.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar, 2000).Value = message;
                command.Parameters.Add("@UserType", System.Data.SqlDbType.NVarChar, 50).Value = userType;
                command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = userId;

                connection.Open();
                command.ExecuteNonQuery();
            }

        }

        public List<string> getMessages(int ticketId)
        {
            List<string> messages = new List<string>();

            string queryString = "select * from Messages where TicketId = @ticketId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@ticketId", System.Data.SqlDbType.Int).Value = ticketId;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string newMessage = reader.GetString(2);
                            string userType = reader.GetString(3);
                            int UserId = reader.GetInt32(4);

                            string username = getUsername(userType, UserId);

                            string message = username + newMessage;

                            messages.Add(message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return messages;
        }

        public string getUsername(string userType, int userId)
        {
            return "USERNAME(placeholder)" + ": ";
        }

        public /*List<TicketModel>*/ void getTicketsByCustomerId(int CustomerId)
        {

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