//-----------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Charlie Robbins">
//     Copyright (c) Charlie Robbins.  All rights reserved.
// </copyright>
// <summary>Contains the EnumHelper class.</summary>
//-----------------------------------------------------------------------

namespace UxLoggingToolkit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Helper methods for the Enum class.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the set of descriptions for the specified enum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The set of descriptions for the specified enum.</returns>
        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
