using System;
using System.Data.SqlClient;
using System.Text;
using TestAzureAPI.Models;

namespace TestAzureAPI.Services {
    public static class DBService {
        public static SqlConnectionStringBuilder connectionString
            = new SqlConnectionStringBuilder();

        public static string GetConnectionString() {
            connectionString.DataSource = ApiSettings.DBServerName;
            connectionString.UserID = ApiSettings.DBUsername;
            connectionString.Password = ApiSettings.DBPassword;
            connectionString.InitialCatalog = ApiSettings.Catalog;
            return connectionString.ConnectionString;
        }

        public static bool AddUser(int userId, long chatId, out string message) {
            message = "";
            try {
                // Визначаємо до якої бази даних, яким користувачем,
                // з яким паролем і до якого сервера підключатись
                using (SqlConnection connectionRule = new SqlConnection(GetConnectionString())) {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT user_id FROM [dbo].[Users] ");
                    sb.Append($"WHERE user_id = {userId}"); // Created query (Створили запит до бази даних)

                    // Created command "SELECT user_id..." to database at connection
                    using (SqlCommand sql = new SqlCommand(sb.ToString(), connectionRule)) {

                        connectionRule.Open();

                        using (SqlDataReader reader = sql.ExecuteReader()) { // Виконуємо та читаємо відповідь
                            if (!reader.HasRows) {   // Unregistered, 0 rows affected, user is not noticed
                                connectionRule.Close(); // Stop Executing Select...
                                connectionRule.Open(); // Start new empty connection

                                sb = new StringBuilder();
                                sb.Append("INSERT INTO [dbo].[Users](user_id, chat_id)");
                                sb.Append($"VALUES ({userId}, {chatId})"); // Created new query

                                using (SqlCommand Addsql = new SqlCommand(sb.ToString(), connectionRule)) {
                                    Addsql.ExecuteReader(); // Executing new query
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            catch (Exception e) {
                message = e.Message;
                return false;
            }
        }
        public static bool AddUserBIO(int userId, string bio, out string message) {
            message = "";
            try {
                StringBuilder sb;
                using (SqlConnection connectionRule = new SqlConnection(GetConnectionString())) {
                        if (isUserRegistered(userId)) {
                            connectionRule.Open();

                            sb = new StringBuilder();
                            sb.Append("UPDATE Users ");
                            sb.Append($"SET bio = '{bio}' ");
                            sb.Append($"WHERE user_id = {userId}");

                            using (SqlCommand command = new SqlCommand(sb.ToString(), connectionRule)) {
                                command.ExecuteReader();
                            }
                        }
                        return true;
                        
                }
            }
            catch (Exception e) {
                message = e.Message;
                return false;
            }
        }
        public static bool AddUserDetails(int userId, string details, out string message) {
            message = "";
            try {
                StringBuilder sb;
                using (SqlConnection connectionRule = new SqlConnection(GetConnectionString())) {
                    if (isUserRegistered(userId)) {
                        connectionRule.Open();

                        sb = new StringBuilder();
                        sb.Append("UPDATE Users ");
                        sb.Append($"SET details = '{details}' ");
                        sb.Append($"WHERE user_id = {userId}");

                        using (SqlCommand command = new SqlCommand(sb.ToString(), connectionRule)) {
                            command.ExecuteReader();
                        }
                    }
                    return true;

                }
            }
            catch (Exception e) {
                message = e.Message;
                return false;
            }
        }

        public static bool AddUserAge(int userId, int age, out string message) {
            message = "";
            try {
                StringBuilder sb;
                using (SqlConnection connectionRule = new SqlConnection(GetConnectionString())) {
                    if (isUserRegistered(userId)) {
                        connectionRule.Open();

                        sb = new StringBuilder();
                        sb.Append("UPDATE Users ");
                        sb.Append($"SET age = '{age}' ");
                        sb.Append($"WHERE user_id = {userId}");

                        using (SqlCommand command = new SqlCommand(sb.ToString(), connectionRule)) {
                            command.ExecuteReader();
                        }
                    }
                    return true;

                }
            }
            catch (Exception e) {
                message = e.Message;
                return false;
            }
        }

        public static bool isUserRegistered(int userId) {
            try {
                using (SqlConnection connectionRule = new SqlConnection(GetConnectionString())) {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT user_id FROM [dbo].[Users] ");
                    sb.Append($"WHERE user_id = {userId}");
                    using (SqlCommand sql = new SqlCommand(sb.ToString(), connectionRule)) {

                        connectionRule.Open();

                        using (SqlDataReader reader = sql.ExecuteReader()) {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception e) {
                throw e;
            }
        }
        // public static void AddTopic(int UserId, )

    }
}
