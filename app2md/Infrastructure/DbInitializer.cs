using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace app2md.Infrastructure
{
    public static class DbInitializer
    {
        private static readonly string _defaultConnectionString = "server=OEM; Integrated Security=SSPI;";
        private static string _dbConnectionString;

        public static void EnsureDbExists(IConfiguration configuration)
        {
            _dbConnectionString = configuration.GetConnectionString("DatabaseConnection");
            var pattern = "database=([a-zA-Z0-9]+);";
            var databaseName = Regex.Match(_dbConnectionString, pattern).Groups[1].Value;

            var dbExists = DatabaseExists(databaseName);
            if (!dbExists)
            {
                CreateDatabase();
                CreateTable();
                //CreateStoredProcedure();
            }
        }

        private static bool DatabaseExists(string databaseName)
        {
            bool exists = false;
            using (SqlConnection sqlConnection = new SqlConnection(_defaultConnectionString))
            using (SqlCommand command = sqlConnection.CreateCommand())
            {
                sqlConnection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM master.dbo.sysdatabases WHERE name ='" + databaseName + "'";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    exists = reader.HasRows;
                }
            }
            return exists;
        }

        private static void CreateDatabase()
        {
            var script = GetScript("CreateDatabase.sql");

            using (var sqlConnection = new SqlConnection(_defaultConnectionString))
            using (var command = sqlConnection.CreateCommand())
            {
                sqlConnection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
        private static void CreateTable()
        {
            var script = GetScript("CreateTable.sql");
            using (var sqlConnection = new SqlConnection(_dbConnectionString))
            using (var command = sqlConnection.CreateCommand())
            {
                sqlConnection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }

        private static string GetScript(string scriptName)
        {
            var executePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(executePath).Value;
            var filePath = Path.Combine(appRoot, @"Infrastructure\Scripts\" + scriptName);
            if (!File.Exists(filePath))
                throw new Exception();

            return File.ReadAllText(filePath);
        }
    }
}