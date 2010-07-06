//-----------------------------------------------------------------------
// <copyright file="ISessionManager.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the ISessionManager interface.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// Event handler for a session state transition.
    /// </summary>
    /// <param name="manager">The session manager.</param>
    public delegate void StateChangedHandler(ISessionManager manager);

    /// <summary>
    /// Interface for a session manager.
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Event raised when a session has started.
        /// </summary>
        event EventHandler SessionStarted;

        /// <summary>
        /// Event raised when a session has ended.
        /// </summary>
        event EventHandler SessionEnded;

        /// <summary>
        /// Event raised when a session has timed out.
        /// </summary>
        event EventHandler SessionTimedOut;

        /// <summary>
        /// Event raised when the session state has changed.
        /// </summary>
        event StateChangedHandler StateChanged;

        /// <summary>
        /// Gets the previous state in the current session.
        /// </summary>
        ApplicationState PreviousState { get; }

        /// <summary>
        /// Gets the current state in the current session.
        /// </summary>
        ApplicationState CurrentState { get; }

        /// <summary>
        /// Gets the current session.
        /// </summary>
        Session Session { get; }

        /// <summary>
        /// Starts a new session.
        /// </summary>
        void StartSession();

        /// <summary>
        /// Logs an event in the current session.
        /// </summary>
        /// <param name="eventRecord">The event record.</param>
        void LogSessionEvent(EventRecord eventRecord);

        /// <summary>
        /// Ends the current session.
        /// </summary>
        void EndSession();

        /// <summary>
        /// Goes to specified state in the current session.
        /// </summary>
        /// <param name="targetState">The state to go to.</param>
        void GoToState(ApplicationState targetState);
    }
}
