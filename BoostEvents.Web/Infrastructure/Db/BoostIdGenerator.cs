namespace BoostEvents.Web.Infrastructure.Db;

public class BoostIdGenerator : IBoostIdGenerator
{
    public Guid New()
    {
        var guidArray = Guid.NewGuid().ToByteArray();

        var now = DateTime.UtcNow;

        // Days since 1900-01-01
        var baseDate = new DateTime(1900, 1, 1);
        var days = BitConverter.GetBytes((short)(now - baseDate).Days);

        // Milliseconds of the day (scaled to avoid overflow)
        var msecs = BitConverter.GetBytes((int)(now.TimeOfDay.TotalMilliseconds / 3.333333));

        // Reverse for endian compatibility
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(days);
            Array.Reverse(msecs);
        }

        // Overwrite the last 6 bytes of the GUID
        guidArray[10] = days[0];
        guidArray[11] = days[1];
        guidArray[12] = msecs[0];
        guidArray[13] = msecs[1];
        guidArray[14] = msecs[2];
        guidArray[15] = msecs[3];

        return new Guid(guidArray);
    }
}