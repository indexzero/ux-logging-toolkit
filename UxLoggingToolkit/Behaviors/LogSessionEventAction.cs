//-----------------------------------------------------------------------
// <copyright file="LogSessionEventAction.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the LogSessionEventAction class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Behaviors
{
    using System;
    using System.Windows;
    using UxLoggingToolkit.Interfaces;

    /// <summary>
    /// Action that logs an event with the available ISessionManager instance.
    /// </summary>
    public class LogSessionEventAction : LogEventAction
    {
        #region Dependency Properties 

        /// <summary>
        /// Backing store for the SessionManager property.
        /// </summary>
        public static readonly DependencyProperty SessionManagerProperty = DependencyProperty.Register(
            "SessionManager",
            typeof(ISessionManager),
            typeof(LogSessionEventAction),
            new FrameworkPropertyMetadata(null));

        #endregion Dependency Properties 

        /// <summary>
        /// Initializes a new instance of the <see cref="LogSessionEventAction"/> class.
        /// </summary>
        public LogSessionEventAction()
        {
        }

        #region Properties 

        /// <summary>
        /// Gets or sets the session manager.
        /// </summary>
        public ISessionManager SessionManager
        {
            get { return (ISessionManager)GetValue(SessionManagerProperty); }
            set { SetValue(SessionManagerProperty, value); }
        }

        #endregion Properties 

        #region Methods 

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the Action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            base.Invoke(parameter);
            if (this.SessionManager != null)
            {
                // TODO: Deal with non-Routed events
                if (parameter is RoutedEventArgs)
                {
                    RoutedEventArgs eventArgs = parameter as RoutedEventArgs;
                    this.SessionManager.LogSessionEvent(new UxEventRecord
                    {
                        EventName = this.CustomEventName ?? eventArgs.RoutedEvent.Name,
                        ElementName = this.CustomElementName ?? ((FrameworkElement)eventArgs.Source).Name,
                        EventTime = DateTime.Now
                    });
                }
            }
        }

        #endregion Methods 
    }
}
