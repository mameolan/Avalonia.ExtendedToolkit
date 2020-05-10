using System;
using Avalonia.ExtendedToolkit.Controls;
using Xunit;

namespace Avalonia.ExtendedToolkit.Tests
{
    public class BadgedTest
    {
        [Fact]
        public void WhenInitialiseTheBadgeIsNull()
        {
            Badged badged = new Badged();
            Assert.Null(badged.Badge);
        }



    }
}
