//-----------------------------------------------------------------------
// <copyright file="UxEventRecord.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the UxEventRecord class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data for tracking ux events.
    /// </summary>
    public class UxEventRecord : EventRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UxEventRecord"/> class.
        /// </summary>
        public UxEventRecord()
        {
        }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Gets or sets the element visual.
        /// </summary>
        public object ElementVisual { get; set; }
    }
}
