﻿using FluentAssertions;
using Sandbox.Api.Common.Helpers;

namespace Sandbox.Api.Common.Tests.Helpers;

public class DateTimeHelperTests : IDisposable
{
    private bool _disposed;

    [Fact]
    public void DateTimeHelper_ShouldReturnConfiguredDate_WhenDateIsSet()
    {
        var estOffset = new TimeSpan(4, 0, 0);
        var testDateTime = new DateTimeOffset(2020, 01, 02, 3, 4, 5, estOffset);
        
        DateTimeHelper.Set(testDateTime);

        var now = DateTimeHelper.Now();

        now.Should().Be(testDateTime);
    }
    
    [Fact]
    public void DateTimeHelper_ShouldNotReturnConfiguredDate_WhenDateIsReSet()
    {
        var testDateTime = new DateTimeOffset(2020, 01, 02, 3, 4, 5, new TimeSpan(0));
        
        // Set the time to testDateTime
        DateTimeHelper.Set(testDateTime);
        
        // Reset to DateTimeOffset.Now
        DateTimeHelper.Reset();
        
        var now = DateTimeHelper.Now();

        now.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(1));
    }

    ~DateTimeHelperTests()
        => Dispose(false);
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;
            
        if (disposing)
        {
            DateTimeHelper.Reset();
        }

        _disposed = true;
    }
}