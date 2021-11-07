﻿using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class LocationTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestPointLocation()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Location).Returns(new Point(1, 2));

            var point = Actor.AsksFor(Location.Of(Locator));
            point.X.Should().Be(1);
            point.Y.Should().Be(2);
        }

        [Test]
        public void TestLocatorDoesNotExist()
        {
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement>().AsReadOnly());
            Actor.Invoking(x => x.AsksFor(Location.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
