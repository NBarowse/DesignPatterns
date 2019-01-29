namespace DesignPatterns.WebDriver
{
    using System.Configuration;

    public class Configuration
    {
        /// <summary>
        /// Gets value specified in App.config of var parameter if exists or uses default value
        /// </summary>
        /// <param name="var"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Value specified in App.config or default value</returns>
        public static string GetEnviromentVar(string var, string defaultValue)
        {
            return ConfigurationManager.AppSettings[var] ?? defaultValue;
        }

        public static string ElementTimeout => GetEnviromentVar("ElementTimeout", "30");

        public static string Browser => GetEnviromentVar("Browser", "Firefox");

        public static string StartUrl => GetEnviromentVar("StartUrl", "https://www.gmail.com/");

        public static string Mail => GetEnviromentVar("mail", "");

        public static string Pswrd => GetEnviromentVar("pswrd", "");
    }
}
