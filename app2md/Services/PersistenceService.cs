using app2md.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace app2md.Services
{
    public static class PersistenceService
    {
        public static int InsertAndReturnID(ContactFormViewModel model, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            var command =
                $"INSERT INTO ContactForm (FirstName, LastName, EmailAddress, PhoneNumber, AreaOfInterests, Message1) " +
                $"VALUES ('{model.FirstName}', '{model.LastName}', '{model.EmailAddress}', '{model.PhoneNumber}', '{model.AreaOfInterests}', '{model.Message}')";

            using (var sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
            }

            // retrieve last record's ID
            // use scalar for performance
            int lastRecordID = default;
            string command1 = "SELECT TOP 1 ID FROM ContactForm ORDER BY 1 DESC";
            using (var sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand(command1, sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.CommandType = CommandType.Text;
                lastRecordID = (int)sqlCommand.ExecuteScalar();
            }
            return lastRecordID;
        }
    }
}