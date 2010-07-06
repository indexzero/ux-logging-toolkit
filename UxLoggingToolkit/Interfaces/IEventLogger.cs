//-----------------------------------------------------------------------
// <copyright file="IEventLogger.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the IEventLogger class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// Interface for simple logging of events.
    /// </summary>
    public interface IEventLogger
    {
        /// <summary>
        /// Resets the log.
        /// </summary>
        void ResetLog();

        /// <summary>
        /// Logs the given event.
        /// </summary>
        /// <param name="eventRecord">The event record.</param>
        void LogEvent(EventRecord eventRecord);

        /////// <summary>
        /////// Loads and deserializes the specified log file.
        /////// </summary>
        /////// <param name="fileName">Name of the log file.</param>
        /////// <returns>The set of event records within the log file.</returns>
        ////IList<EventRecord> LoadLogFile(string fileName);

        /// <summary>
        /// Adds the specified observer to this instance.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void AddObserver(IEventLogAware observer);

        /// <summary>
        /// Removes the specified observer from this instance.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void RemoveObserver(IEventLogAware observer);
    }
}
