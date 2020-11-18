﻿using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Provides auto-generated data specimens generated by AutoFixture with AutoMoq to xUnit's Theory attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Gets the data needed to test the given method.
        /// </summary>
        /// <param name="methodUnderTest">The method under test.</param>
        /// <returns>Returns the data needed for the method</returns>
        /// <exception cref="ArgumentNullException">methodUnderTest</exception>
        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest)
        {
            if (methodUnderTest == null)
                throw new ArgumentNullException(nameof(methodUnderTest));

            //loop through the method's attributes and apply any customizations to the fixture before supplying
            //the specimens for the method parameters
#pragma warning disable 618
            foreach (WithCustomizationAttribute att in methodUnderTest.GetCustomAttributes<WithCustomizationAttribute>(false))
            {
                Fixture.Customize(att.Customization);
            }

            Fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
#pragma warning restore 618

            return base.GetData(methodUnderTest);
        }
    }
}