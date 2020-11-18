﻿using AutoFixture;
using System;
using System.Globalization;
using System.Reflection;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper AutoFixture customization attribute that allows for per-test customizations
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class WithCustomizationAttribute : Attribute
    {
        /// <summary>
        /// Gets the customization.
        /// </summary>
        public virtual ICustomization Customization { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WithCustomizationAttribute"/> class.
        /// </summary>
        /// <param name="customizationType">Type of the customization.</param>
        /// <exception cref="ArgumentNullException">customizationType</exception>
        /// <exception cref="ArgumentException">customizationType or customizationType</exception>
        public WithCustomizationAttribute(Type customizationType)
        {
            if (customizationType == null)
                throw new ArgumentNullException(nameof(customizationType));
            if (!typeof(ICustomization).IsAssignableFrom(customizationType))
                throw new ArgumentException(string.Format(
                            CultureInfo.CurrentCulture,
                            "{0} is not compatible with ICustomization. Please supply a Type which implements ICustomization.",
                            customizationType),
                        nameof(customizationType));

            ConstructorInfo? ctor = customizationType.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} has no default constructor. Please supply a a Type that implements ICustomization and has a default constructor.",
                        customizationType),
                    nameof(customizationType));
            }

            Customization = (ICustomization)ctor.Invoke(null);
        }
    }
}