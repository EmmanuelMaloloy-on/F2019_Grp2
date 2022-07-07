using CustomerSupportManager.Models;
using Microsoft.AspNet.Identity;
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
                            ticket.CustomerId = reader.GetString(1);
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                            ticket.Title = reader.GetString(4);
                            ticket.Date = reader.GetDateTime(5);

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

        public List<TicketModel> getTicketsByCategory(string category)
        {
            List<TicketModel> tickets = new List<TicketModel>();

            string queryString = "select * from Tickets WHERE Category = @category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@category", System.Data.SqlDbType.NVarChar, 50).Value = category;

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
                            ticket.CustomerId = reader.GetString(1);
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                            ticket.Title = reader.GetString(4);
                            ticket.Date = reader.GetDateTime(5);

                            tickets.Add(ticket);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return tickets;
        }

        internal List<TicketModel> getTicketsByStatus(string status)
        {
            List<TicketModel> tickets = new List<TicketModel>();

            string queryString = "select * from Tickets WHERE Status = @status";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@status", System.Data.SqlDbType.NVarChar, 50).Value = status;

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
                            ticket.CustomerId = reader.GetString(1);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                            ticket.Title = reader.GetString(4);
                            ticket.Date = reader.GetDateTime(5);

                            tickets.Add(ticket);
                        }
                    }
                }
                catch (Exception e)
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
                            ticket.CustomerId = reader.GetString(1);
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                            ticket.Title = reader.GetString(4);
                            ticket.Date = reader.GetDateTime(5);

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

        public List<StatusCountModel> getTicketCountByStatus()
        {
            List<StatusCountModel> statusCount = new List<StatusCountModel>();

            string queryString = "SELECT (select count(*) from Tickets where Status = 'new') as new_count, (select count(*) from Tickets where Status = 'unresolved') as unresolved_count, (select count(*) from Tickets where Status = 'solved') as solved_count, (select count(*) from Tickets where Status = 'error') as error_count";

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
                            string[] names = { "New", "Unresolved", "Solved", "Error" };
                            int i = 0;

                            foreach(string name in names)
                            {
                                StatusCountModel sc = new StatusCountModel();
                                sc.Status = name;
                                sc.Count = reader.GetInt32(i);

                                statusCount.Add(sc);

                                i++;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return statusCount;
        }

        public List<CategoryCountModel> getTicketCountByCategory()
        {
            List<CategoryCountModel> categoryCount = new List<CategoryCountModel>();

            string queryString = "SELECT (select count(*) from Tickets where Category = 'Technical'), (select count(*) from Tickets where Category = 'Sales'), (select count(*) from Tickets where Category = 'Inquiry')";

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
                            string[] names = { "Technical", "Sales", "Inquiry" };
                            int i = 0;

                            foreach (string name in names)
                            {
                                CategoryCountModel cc = new CategoryCountModel();
                                cc.Category = name;
                                cc.Count = reader.GetInt32(i);

                                categoryCount.Add(cc);

                                i++;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return categoryCount;
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
            string queryString = "INSERT INTO Tickets Values(@CustomerId, @Category, @Status, @Title, @Date); select CAST(scope_identity() AS int);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@CustomerId", System.Data.SqlDbType.NVarChar, 128).Value = ticketModel.CustomerId;
                command.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 50).Value = "";
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Status;
                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Title;
                command.Parameters.Add("@Date", System.Data.SqlDbType.DateTime).Value = ticketModel.Date;

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
                command.Parameters.Add("@CustomerId", System.Data.SqlDbType.NVarChar, 128).Value = ticketModel.CustomerId;
                command.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Category;
                command.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Status;
                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50).Value = ticketModel.Title;
                //command.Parameters.Add("@Date", System.Data.SqlDbType.DateTime).Value = ticketModel.Date;

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

        internal List<TicketModel> searchForTicket(string searchPhrase)
        {
            List<TicketModel> tickets = new List<TicketModel>();

            string queryString = "select * from Tickets WHERE Title LIKE @searchPhrase";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@searchPhrase", System.Data.SqlDbType.NVarChar).Value = "%" + searchPhrase + "%";

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
                            ticket.CustomerId = reader.GetString(1);
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                            ticket.Title = reader.GetString(4);
                            ticket.Date = reader.GetDateTime(5);

                            tickets.Add(ticket);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return tickets;
        }

        public void addMessage(MessageModel messageModel)
        {
            // Add message to ticket

            string queryString = "INSERT INTO Messages Values(@TicketId, @Message, @UserId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@TicketId", System.Data.SqlDbType.Int).Value = messageModel.TicketId;
                command.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar, 2000).Value = messageModel.Message;
                command.Parameters.Add("@UserId", System.Data.SqlDbType.NVarChar, 128).Value = messageModel.UserId;

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
                            string UserId = reader.GetString(3);

                            string username = getName(UserId);

                            string message = username + ": " + newMessage;

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

        public List<TicketModel> getTicketsByCustomerId(string customerId)
        {
            List<TicketModel> tickets = new List<TicketModel>();

            string queryString = "select * from Tickets WHERE CustomerId = @customerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@customerId", System.Data.SqlDbType.NVarChar, 128).Value = customerId;

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
                            ticket.CustomerId = reader.GetString(1);
                            //ticket.Product = reader.GetString(2);
                            ticket.Category = reader.GetString(2);
                            ticket.Status = reader.GetString(3);
                            ticket.Title = reader.GetString(4);
                            ticket.Date = reader.GetDateTime(5);

                            tickets.Add(ticket);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return tickets;
        }

        public void changeCategory(int ticketId, string category)
        { 
}

        public string getName(string userId)
        {
            string queryString = "SELECT Name FROM AspNetUsers WHERE Id = @userId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.NVarChar, 128).Value = userId;

                string name = "Anonymous";

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            name = reader.GetString(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return name;
            }

        }

        public void changeName(string userID, string newName)
        {
            string queryString = "Update AspNetUsers SET Name = @newName WHERE Id = @userID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@userID", System.Data.SqlDbType.NVarChar, 128).Value = userID;
                command.Parameters.Add("@newName", System.Data.SqlDbType.NVarChar, 256).Value = newName;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<UserModel> getUsers()
        {
            List<UserModel> users = new List<UserModel>();

            string queryString = "select Id,Email,Name from AspNetUsers";

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
                            UserModel user = new UserModel();
                            user.Id = reader.GetString(0);
                            user.Email = reader.GetString(1);
                            user.Name = reader.GetString(2);

                            user.Role = getRoleById(user.Id);

                            if (user.Role != "Customer")
                            {
                                users.Add(user);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return users;
        }

        public List<UserModel> getCustomers()
        {
            List<UserModel> users = new List<UserModel>();

            string queryString = "select Id,Email,Name from AspNetUsers";

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
                            UserModel user = new UserModel();
                            user.Id = reader.GetString(0);
                            user.Email = reader.GetString(1);
                            user.Name = reader.GetString(2);

                            user.Role = getRoleById(user.Id);

                            if (user.Role == "Customer")
                            {
                                users.Add(user);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return users;
        }

        public string getRoleById(string userId)
        {
            string queryString = "select RoleId from AspNetUserRoles where UserId = @userId";
            string queryStringRole = "select Name from AspNetRoles where Id = @roleId";

            string roleId = "";
            string role = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@userID", System.Data.SqlDbType.NVarChar, 128).Value = userId;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    roleId = reader.GetString(0);
                }
                connection.Close();

                SqlCommand commandsecond = new SqlCommand(queryStringRole, connection);

                commandsecond.Parameters.Add("@roleId", System.Data.SqlDbType.NVarChar, 128).Value = roleId;

                connection.Open();
                SqlDataReader readertwo = commandsecond.ExecuteReader();

                if (readertwo.HasRows)
                {
                    readertwo.Read();
                    role = readertwo.GetString(0);
                }
            }

            return role;
        }




        //public bool authenticateAdmin(AdminUserModel user)
        //{
        //    bool success = false;

        //    string queryString = "select * from Admins where username = @username and password = @password";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);

        //        command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
        //        command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

        //        success = tryAuthenticate(connection, command);
        //    }

        //    return success;

        //}

        //public bool authenticateUser(UserModel user)
        //{
        //    bool success = false;

        //    string queryString = "select * from Admins where username = @username and password = @password";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);

        //        command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
        //        command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

        //        success = tryAuthenticate(connection, command);
        //    }

        //    return success;

        //}

        //public bool tryAuthenticate(SqlConnection connection, SqlCommand command)
        //{
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            reader.Close();
        //            return true;
        //        }
        //        else
        //        {
        //            reader.Close();
        //            return false;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false;
        //    }
        //}

    }
}