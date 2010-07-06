//-----------------------------------------------------------------------
// <copyright file="SessionManager.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the SessionManager class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Threading;
    using UxLoggingToolkit.Interfaces;

    /// <summary>
    /// A simple session manager for tracking data in user sessions.
    /// </summary>
    public class SessionManager : ISessionManager
    {
        #region Fields 

        /// <summary>
        /// The current state in the current session.
        /// </summary>
        private ApplicationState currentState;

        /// <summary>
        /// The event logger for the application.
        /// </summary>
        private IEventLogger eventLogger;

        /// <summary>
        /// The previous state in the current session.
        /// </summary>
        private ApplicationState previousState;

        /// <summary>
        /// The current session being managed.
        /// </summary>
        private Session session;

        /// <summary>
        /// Timer that waits for session timeout.
        /// </summary>
        private DispatcherTimer timeOutTimer;

        #endregion Fields 

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        /// <param name="eventLogger">The event logger.</param>
        public SessionManager(
            IEventLogger eventLogger)
        {
            this.eventLogger = eventLogger;

            this.timeOutTimer = new DispatcherTimer();
            this.timeOutTimer.Interval = TimeSpan.FromMinutes(5);
            this.timeOutTimer.Tick += this.TimeOutTimerTick;
        }

        #region Events 

        /// <summary>
        /// Event raised when a session has ended.
        /// </summary>
        public event EventHandler SessionEnded;

        /// <summary>
        /// Event raised when a session has started.
        /// </summary>
        public event EventHandler SessionStarted;

        /// <summary>
        /// Event raised when a session has timed out.
        /// </summary>
        public event EventHandler SessionTimedOut;

        /// <summary>
        /// Event raised when the session state has changed.
        /// </summary>
        public event StateChangedHandler StateChanged;

        #endregion Events 

        #region Properties 

        /// <summary>
        /// Gets the current state in the current session.
        /// </summary>
        public ApplicationState CurrentState
        {
            get { return this.currentState; }
        }

        /// <summary>
        /// Gets the previous state in the current session.
        /// </summary>
        public ApplicationState PreviousState
        {
            get { return this.previousState; }
        }

        /// <summary>
        /// Gets the current session.
        /// </summary>
        public Session Session
        {
            get { return this.session; }
        }

        #endregion Properties 

        #region Methods 

        /// <summary>
        /// Ends the current session.
        /// </summary>
        public void EndSession()
        {
            EventRecord endRecord = new EventRecord
            {
                EventName = "SessionEnded",
                EventTime = DateTime.Now
            };

            this.LogSessionEvent(endRecord);
            this.session.EndTime = endRecord.EventTime;
            this.timeOutTimer.Stop();
            this.eventLogger.ResetLog();
            this.OnSessionEnded();
        }

        /// <summary>
        /// Goes to specified state in the current session.
        /// </summary>
        /// <param name="targetState">The state to go to.</param>
        public void GoToState(ApplicationState targetState)
        {
            EventRecord stateChangeRecord = new EventRecord
            {
                EventName = "StateChanged",
                EventTime = DateTime.Now,
                Metadata = new Dictionary<string, object>
                {
                    { "PreviousState", this.currentState.Name },
                    { "CurrentState", targetState.Name } 
                }
            };

            this.LogSessionEvent(stateChangeRecord);
            this.previousState = this.currentState;
            this.currentState = targetState;

            this.OnStateChanged();
        }

        /// <summary>
        /// Logs an event in the current session.
        /// </summary>
        /// <param name="eventRecord">The event record.</param>
        public void LogSessionEvent(EventRecord eventRecord)
        {
            this.timeOutTimer.Stop();
            this.timeOutTimer.Start();
            this.eventLogger.LogEvent(eventRecord);
        }

        /// <summary>
        /// Starts a new session.
        /// </summary>
        public void StartSession()
        {
            EventRecord startRecord = new EventRecord
            {
                EventName = "SessionStarted",
                EventTime = DateTime.Now
            };

            this.LogSessionEvent(startRecord);
            this.session = new Session { StartTime = DateTime.Now };
            this.timeOutTimer.Start();
            this.OnSessionStarted();

            this.currentState = new ApplicationState { Name = "StartState" };
        }

        /// <summary>
        /// Raises the SessionEnded event.
        /// </summary>
        protected virtual void OnSessionEnded()
        {
            EventHandler sessionEnded = this.SessionEnded;
            if (sessionEnded != null)
            {
                sessionEnded(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the SessionStarted event.
        /// </summary>
        protected virtual void OnSessionStarted()
        {
            EventHandler sessionStarted = this.SessionStarted;
            if (sessionStarted != null)
            {
                sessionStarted(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the SessionTimedOut event.
        /// </summary>
        protected virtual void OnSessionTimedOut()
        {
            EventHandler sessionTimedOut = this.SessionTimedOut;
            if (sessionTimedOut != null)
            {
                sessionTimedOut(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the StateChanged event.
        /// </summary>
        protected virtual void OnStateChanged()
        {
            StateChangedHandler stateChanged = this.StateChanged;
            if (stateChanged != null)
            {
                stateChanged(this);
            }
        }

        /// <summary>
        /// Occurs when the timeOutTimer has fired (after 5 minutes of inactivity).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TimeOutTimerTick(object sender, EventArgs args)
        {
            EventRecord timeoutRecord = new EventRecord
            {
                EventName = "SessionTimedOut",
                EventTime = DateTime.Now
            };

            this.LogSessionEvent(timeoutRecord);
            this.session.EndTime = timeoutRecord.EventTime;
            this.timeOutTimer.Stop();
            this.eventLogger.ResetLog();
            this.OnSessionTimedOut();
        }

        #endregion Methods 
    }
}
