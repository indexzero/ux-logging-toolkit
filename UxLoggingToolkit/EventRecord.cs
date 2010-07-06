//-----------------------------------------------------------------------
// <copyright file="EventRecord.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the EventRecord class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data for tracking records of events.
    /// </summary>
    public class EventRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventRecord"/> class.
        /// </summary>
        public EventRecord()
        {
        }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the event time.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}
