using Microsoft.Extensions.Configuration;

namespace HospitalManagementSystem.Config
{
    /// <summary>
    /// Central, read-only access to application configuration (appsettings.json).
    /// The connection string is defined ONLY in appsettings.json - never in code.
    ///
    /// Optional override (useful so a real password never sits in the file):
    /// set the environment variable HMS_DB_CONNECTION to a full connection string.
    /// </summary>
    public static class AppConfig
    {
        private const string ConfigFileName = "appsettings.json";
        private const string ConnectionOverrideVariable = "HMS_DB_CONNECTION";

        private static IConfigurationRoot? _configuration;
        private static string? _connectionString;

        /// <summary>Loads appsettings.json. Call once at startup (Program.cs).</summary>
        public static void Load()
        {
            string path = Path.Combine(AppContext.BaseDirectory, ConfigFileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"Configuration file '{ConfigFileName}' was not found next to the application.\n\n" +
                    "In Visual Studio: select appsettings.json -> Properties -> " +
                    "Copy to Output Directory = 'Copy if newer'.",
                    path);
            }

            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(ConfigFileName, optional: false, reloadOnChange: false)
                .Build();

            string? fromEnvironment = Environment.GetEnvironmentVariable(ConnectionOverrideVariable);
            _connectionString = !string.IsNullOrWhiteSpace(fromEnvironment)
                ? fromEnvironment
                : _configuration.GetConnectionString("HospitalDb");

            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string 'HospitalDb' is missing in {ConfigFileName}.");
            }
        }

        /// <summary>The MySQL connection string.</summary>
        public static string ConnectionString
        {
            get
            {
                EnsureLoaded();
                return _connectionString!;
            }
        }

        /// <summary>Application display name (window titles, dialogs).</summary>
        public static string AppName
        {
            get
            {
                if (_configuration == null) return "Hospital Management System";
                return _configuration["Application:Name"] ?? "Hospital Management System";
            }
        }

        /// <summary>Application version text.</summary>
        public static string AppVersion
        {
            get
            {
                if (_configuration == null) return "1.0";
                return _configuration["Application:Version"] ?? "1.0";
            }
        }

        /// <summary>Connection string with the password masked - safe to show on screen or in logs.</summary>
        public static string SafeConnectionString
        {
            get
            {
                EnsureLoaded();
                var parts = _connectionString!.Split(';', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    string key = parts[i].Split('=')[0].Trim();
                    if (key.Equals("Password", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("Pwd", StringComparison.OrdinalIgnoreCase))
                    {
                        parts[i] = key + "=********";
                    }
                }
                return string.Join(";", parts);
            }
        }

        private static void EnsureLoaded()
        {
            if (_connectionString == null) Load();
        }
    }
}