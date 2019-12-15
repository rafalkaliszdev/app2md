using app2md.Enums;
using app2md.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace app2md.Services
{
    public class PersistenceService : IPersistenceService
    {
        private readonly IConfiguration configuration;

        public PersistenceService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int InsertAndFetchId(ContactFormViewModel model)
        {
            try
            {
                var command =
                    "INSERT INTO ContactForm (FirstName, LastName, EmailAddress, PhoneNumber, AreaOfInterests, Message1) " +
                    "VALUES (@FirstName, @LastName, @EmailAddress, @PhoneNumber, @AreaOfInterests, @Message)";

                using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("DatabaseConnection")))
                using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@FirstName", model.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", model.LastName);
                    sqlCommand.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@AreaOfInterests", model.AreaOfInterests);
                    sqlCommand.Parameters.AddWithValue("@Message", model.Message);

                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
                }

                // retrieve last record's ID
                // use scalar for performance
                int lastRecordID = default;
                string command1 = "SELECT TOP 1 ID FROM ContactForm ORDER BY 1 DESC";
                using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("DatabaseConnection")))
                using (SqlCommand sqlCommand = new SqlCommand(command1, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    lastRecordID = (int)sqlCommand.ExecuteScalar();
                }
                return lastRecordID;
            }
            catch
            {
                throw; // logger advised
            }
        }

        public bool IsNewName(string name, NameType nameType)
        {
            string command = $"SELECT TOP 1 [ID] FROM [ContactForm] WHERE [{nameType.ToString()}] LIKE @Name";

            bool isNew = default;
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("DatabaseConnection")))
            using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Name", name);
                sqlCommand.CommandType = CommandType.Text;
                isNew = sqlCommand.ExecuteScalar() == null ? true : false;
            }

            return isNew;
        }
    }
}