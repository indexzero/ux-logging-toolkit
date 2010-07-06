//-----------------------------------------------------------------------
// <copyright file="IEventLogAware.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the IEventLogAware class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Interfaces
{
    /// <summary>
    /// Interface for adding metadata to event logs.
    /// </summary>
    public interface IEventLogAware
    {
        /// <summary>
        /// Observes the specified event info.
        /// </summary>
        /// <param name="eventRecord">The event record.</param>
        /// <returns>A modified version of the event record.</returns>
        EventRecord Observe(EventRecord eventRecord);
    }
}
