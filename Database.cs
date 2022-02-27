using Npgsql;

namespace Rover
{
    public static class Database
    {
        public static string connString;

        private static string? databaseName;
        private static string? databaseHost;
        private static string? databaseUser;
        private static string? databasePass;
        private static string? databasePort;

        static Database()
        {
            try { databaseName = Environment.GetEnvironmentVariable("rover_dbName"); }
            catch (Exception e) when (databaseName == null) { Console.WriteLine(e); databaseName = "rover_db"; }
            
            try { databaseHost = Environment.GetEnvironmentVariable("rover_dbHost"); }
            catch (Exception e) when (databaseHost == null) { Console.WriteLine(e); databaseHost = "localhost"; }

            try { databaseUser = Environment.GetEnvironmentVariable("rover_dbUser"); }
            catch (Exception e) when (databaseUser == null) { Console.WriteLine(e); databaseUser = "rover_user"; }

            try { databasePass = Environment.GetEnvironmentVariable("rover_dbPass"); }
            catch (Exception e) when (databasePass == null) { Console.WriteLine(e); databasePass = "password"; }

            try { databasePort = Environment.GetEnvironmentVariable("rover_dbPort"); }
            catch (Exception e) when (databasePort == null) { Console.WriteLine(e); databasePort = "5432"; }

            connString =
                $"Host={databaseHost};" +
                $"Username={databaseUser};" +
                $"Password={databasePass};" +
                $"Database={databaseName};" +
                $"Port={databasePort};";
        }
    }
}