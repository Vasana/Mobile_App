// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Agent_App.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault("Username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Username", value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string jobRole
        {
            get
            {
                return AppSettings.GetValueOrDefault("jobRole", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("jobRole", value);
            }
        }

        public static string agentCode
        {
            get
            {
                return AppSettings.GetValueOrDefault("agentCode", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("agentCode", value);
            }
        }

        public static string orgTeamCode
        {
            get
            {
                return AppSettings.GetValueOrDefault("orgTeamCode", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("orgTeamCode", value);
            }
        }


        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static bool ProfileImageSet
        {
            get
            {
                return AppSettings.GetValueOrDefault("ProfileImageSet", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("ProfileImageSet", value);
            }
        }

    }
}
