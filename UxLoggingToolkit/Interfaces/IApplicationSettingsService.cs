//-----------------------------------------------------------------------
// <copyright file="IApplicationSettingsService.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the IApplicationSettingsService class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Inteface for storing simple application settings.
    /// </summary>
    public interface IApplicationSettingsService
    {
        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <value>All settings.</value>
        Dictionary<string, string> AllSettings { get; }

        /// <summary>
        /// Adds a setting to this instance.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The value of the setting.</param>
        void AddSetting(string name, string value);

        /// <summary>
        /// Removes a setting from this instance.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        void RemoveSetting(string name);

        /// <summary>
        /// Gets the specified setting from this instance.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns>The value of the setting</returns>
        string GetSetting(string name);

        /// <summary>
        /// Loads the settings from the specified settings path.
        /// </summary>
        /// <param name="settingsPath">The settings path.</param>
        void Load(string settingsPath);

        /// <summary>
        /// Saves the settings to the specified settings path.
        /// </summary>
        /// <param name="settingsPath">The settings path.</param>
        void Save(string settingsPath);
    }
}
