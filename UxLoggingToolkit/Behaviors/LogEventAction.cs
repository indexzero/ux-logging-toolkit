//-----------------------------------------------------------------------
// <copyright file="LogEventAction.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the LogEventAction class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Interactivity;
    using UxLoggingToolkit.Interfaces;

    /// <summary>
    /// Action that logs a given UX event.
    /// </summary>
    public class LogEventAction : TargetedTriggerAction<FrameworkElement>
    {
        #region Dependency Properties 

        /// <summary>
        /// Backing store for the EventLogger property.
        /// </summary>
        public static readonly DependencyProperty EventLoggerProperty = DependencyProperty.Register(
            "EventLogger",
            typeof(IEventLogger),
            typeof(LogEventAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Backing store for the CustomEventName property.
        /// </summary>
        public static readonly DependencyProperty CustomEventNameProperty = DependencyProperty.Register(
            "CustomEventName",
            typeof(string),
            typeof(LogEventAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Backing store for the CustomElementName property.
        /// </summary>
        public static readonly DependencyProperty CustomElementNameProperty = DependencyProperty.Register(
            "CustomElementName",
            typeof(string),
            typeof(LogEventAction),
            new FrameworkPropertyMetadata(null));

        #endregion Dependency Properties 

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventAction"/> class.
        /// </summary>
        public LogEventAction()
        {
        }

        #region Properties 

        /// <summary>
        /// Gets or sets the event logger.
        /// </summary>
        /// <value>The event logger.</value>
        public IEventLogger EventLogger
        {
            get { return (IEventLogger)GetValue(EventLoggerProperty); }
            set { SetValue(EventLoggerProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the custom event.
        /// </summary>
        /// <value>The name of the custom event.</value>
        public string CustomEventName
        {
            get { return (string)GetValue(CustomEventNameProperty); }
            set { SetValue(CustomEventNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the custom element.
        /// </summary>
        /// <value>The name of the custom element.</value>
        public string CustomElementName
        {
            get { return (string)GetValue(CustomElementNameProperty); }
            set { SetValue(CustomElementNameProperty, value); }
        }

        #endregion Properties 

        #region Methods 

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the Action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            if (this.EventLogger != null)
            {
                // TODO: Deal with non-Routed events
                if (parameter is RoutedEventArgs)
                {
                    RoutedEventArgs eventArgs = parameter as RoutedEventArgs;
                    this.EventLogger.LogEvent(new UxEventRecord
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
