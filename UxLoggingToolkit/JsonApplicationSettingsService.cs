//-----------------------------------------------------------------------
// <copyright file="JsonApplicationSettingsService.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the JsonApplicationSettingsService class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System.Collections.Generic;
    using System.IO;
    using JsonFx.Json;
    using UxLoggingToolkit.Interfaces;

    /// <summary>
    /// A simple service the persists application settings to and from JSON files.
    /// </summary>
    public class JsonApplicationSettingsService : IApplicationSettingsService
    {
        /// <summary>
        /// The mapping of all settings managed by this instance.
        /// </summary>
        private Dictionary<string, string> allSettings = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonApplicationSettingsService"/> class.
        /// </summary>
        public JsonApplicationSettingsService()
        {
        }

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <value>All settings.</value>
        public Dictionary<string, string> AllSettings
        {
            get { return this.allSettings; }
        }

        /// <summary>
        /// Adds a setting to this instance.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The value of the setting.</param>
        public void AddSetting(string name, string value)
        {
            string existingSetting;

            if (!this.allSettings.TryGetValue(name, out existingSetting))
            {
                this.allSettings.Add(name, value);
            }
            else
            {
                this.allSettings[name] = value;
            }
        }

        /// <summary>
        /// Removes a setting from this instance.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        public void RemoveSetting(string name)
        {
            if (this.allSettings.ContainsKey(name))
            {
                this.allSettings.Remove(name);
            }
        }

        /// <summary>
        /// Gets the specified setting from this instance.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the setting</returns>
        public string GetSetting(string name)
        {
            if (this.allSettings.ContainsKey(name))
            {
                return this.allSettings[name];
            }

            return null;
        }

        /// <summary>
        /// Loads the settings from the specified settings path.
        /// </summary>
        /// <param name="settingsPath">The settings path.</param>
        public void Load(string settingsPath)
        {
            this.allSettings.Clear();

            using (StreamReader settingsReader = File.OpenText(settingsPath))
            {
                string rawSettings = settingsReader.ReadToEnd();
                JsonReader settingsJson = new JsonReader(rawSettings);
                Dictionary<string, object> settings = settingsJson.Deserialize() as Dictionary<string, object>;
                
                foreach (KeyValuePair<string, object> setting in settings)
                {
                    this.AddSetting(setting.Key, (string)setting.Value);
                }
            }
        }

        /// <summary>
        /// Saves the settings to the specified settings path.
        /// </summary>
        /// <param name="settingsPath">The settings path.</param>
        public void Save(string settingsPath)
        {
            string settingsJson = JsonWriter.Serialize(this.allSettings);
            using (FileStream settingsFile = new FileStream(settingsPath, FileMode.Create))
            {
                using (StreamWriter settingsWriter = new StreamWriter(settingsFile))
                {
                    settingsWriter.Write(settingsJson);
                }
            }
        }
    }
}
