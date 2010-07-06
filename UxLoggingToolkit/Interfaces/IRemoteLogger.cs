//-----------------------------------------------------------------------
// <copyright file="IRemoteLogger.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the IRemoteLogger class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A simple interface for storing logs remotely.
    /// </summary>
    public interface IRemoteLogger
    {
        /// <summary>
        /// Logs the specified data remotely.
        /// </summary>
        /// <param name="logData">The log data.</param>
        /// <returns>A value indicating the success of the operation.</returns>
        bool Log(string logData);
    }
}
