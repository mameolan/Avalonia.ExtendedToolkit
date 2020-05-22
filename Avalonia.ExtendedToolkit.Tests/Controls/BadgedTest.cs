using System;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls;
using Xunit;

namespace Avalonia.ExtendedToolkit.Tests
{
    public class BadgedTest
    {
        [Fact]
        public void WhenInitialiseThePropertiesAreInitializedCorrectly()
        {
            Badged badged = new Badged();
            Assert.Null(badged.Badge);
            Assert.Null(badged.BadgeBackground);
            Assert.Null(badged.BadgeForeground);
            Assert.True(badged.BadgePlacementMode == BadgePlacementMode.TopLeft);
            Assert.True(badged.BadgeMargin == new Thickness(0));
            Assert.False(badged.IsBadgeSet);    
        }

        [Fact]
        public void WhenSetABadgeIsBadgeSetIsTrue()
        {
            Badged badged = new Badged();
            badged.Badge = new Button();
            Assert.NotNull(badged.Badge);
            Assert.True(badged.IsBadgeSet);
        }

        [Fact]
        public void WhenSetABadgeBadgeChangedIsExecuted()
        {
            Badged badged = new Badged();
            bool isExecuted = false;
            badged.BadgeChanged += (o, e) =>
            {
                isExecuted = true;
            };
            badged.Badge = new Button();

            Assert.True(isExecuted);

        }




    }
}
