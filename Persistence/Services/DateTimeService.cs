using Application.Common.Interfaces;

namespace Persistence.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}