//-----------------------------------------------------------------------
// <copyright file="JsonEventLogger.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the JsonEventLogger class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Xml;
    using System.Security.AccessControl;
    using UxLoggingToolkit.Interfaces;

    /// <summary>
    /// A simple event logger that persists event logs to JSON files.
    /// </summary>
    public class JsonEventLogger : IEventLogger
    {
        #region Fields 

        /// <summary>
        /// Additional known types for the DataContractJsonSerializer.
        /// </summary>
        private static List<Type> additionalKnownTypes;

        /// <summary>
        /// The application settings service global to the application.
        /// </summary>
        private IApplicationSettingsService applicationSettingsService;

        /// <summary>
        /// The set of observers who wish to participate in this instances log.
        /// </summary>
        private List<IEventLogAware> eventObservers = new List<IEventLogAware>();

        /// <summary>
        /// The serializer converting the logged events to json
        /// </summary>
        private DataContractJsonSerializer jsonSerializer;

        /// <summary>
        /// The current set of events that have been logged.
        /// </summary>
        private List<EventRecord> loggedEvents = new List<EventRecord>();

        /// <summary>
        /// The remote logger global to the application.
        /// </summary>
        private IRemoteLogger remoteLogger;

        #endregion Fields 

        /// <summary>
        /// Initializes static members of the <see cref="JsonEventLogger"/> class.
        /// </summary>
        static JsonEventLogger()
        {
            additionalKnownTypes = new List<Type>
            {
                typeof(UxEventRecord),
                typeof(ApplicationState)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonEventLogger"/> class.
        /// </summary>
        /// <param name="applicationSettingsService">The application settings service.</param>
        /// <param name="remoteLogger">The remote logger.</param>
        public JsonEventLogger(
            IApplicationSettingsService applicationSettingsService,
            IRemoteLogger remoteLogger)
        {
            this.applicationSettingsService = applicationSettingsService;
            this.remoteLogger = remoteLogger;

            this.jsonSerializer = new DataContractJsonSerializer(
                typeof(List<EventRecord>),
                additionalKnownTypes);
        }

        #region Methods 

        /// <summary>
        /// Adds the specified observer to this instance.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void AddObserver(IEventLogAware observer)
        {
            if (!this.eventObservers.Contains(observer))
            {
                this.eventObservers.Add(observer);
            }
        }

        /// <summary>
        /// Logs the given event.
        /// </summary>
        /// <param name="eventRecord">The event record.</param>
        public void LogEvent(EventRecord eventRecord)
        {
            foreach (IEventLogAware observer in this.eventObservers)
            {
                eventRecord = observer.Observe(eventRecord);
            }

            this.loggedEvents.Add(eventRecord);
        }

        /// <summary>
        /// Removes the specified observer from this instance.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void RemoveObserver(IEventLogAware observer)
        {
            if (this.eventObservers.Contains(observer))
            {
                this.eventObservers.Remove(observer);
            }
        }

        /// <summary>
        /// Resets the log.
        /// </summary>
        public void ResetLog()
        {
            try
            {
                string logPath = this.applicationSettingsService.GetSetting("LogPath");

                string logFileName = Path.Combine(logPath, "Session - " + DateTime.Now.ToString("d.M.yyyy HH-mm-ss") + ".json");

                // Create the log directory if it doesn't exist
                if(!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath); 
                }

                string jsonLogData = string.Empty;
                using (MemoryStream jsonStream = new MemoryStream())
                {
                    // Log the data remotely
                    XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(jsonStream);
                    this.jsonSerializer.WriteObject(jsonStream, this.loggedEvents);
                    jsonWriter.Flush();
                    string rawJson = Encoding.Default.GetString(jsonStream.GetBuffer());
                    this.remoteLogger.Log(rawJson);

                    // Log to the file system
                    using (FileStream logStream = File.Open(logFileName, FileMode.Create))
                    {
                        this.jsonSerializer.WriteObject(logStream, this.loggedEvents);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                this.loggedEvents.Clear();
            }
        }

        #endregion Methods 
    }
}
