using System;
using DotNetEnv;

namespace SafeQuake.Infrastructure.Persistence
{
    public static class DatabaseConfiguration
    {
        static DatabaseConfiguration()
        {
            Env.Load();
        }

        public static string GetConnectionString()
        {
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "oracle.fiap.com.br";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "1521";
            var sid = Environment.GetEnvironmentVariable("DB_SID") ?? "ORCL";

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Database credentials not found in environment variables. Please check your .env file.");
            }

            return $"User Id={user};Password={password};Data Source={host}:{port}/{sid};";
        }
    }
} 