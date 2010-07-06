//-----------------------------------------------------------------------
// <copyright file="Session.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the Session class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// Relevant data for user sessions. 
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session()
        {
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        public IList<ApplicationState> States { get; set; }

        /// <summary>
        /// Gets or sets the event records.
        /// </summary>
        public IList<EventRecord> EventRecords { get; set; }
    }
}
