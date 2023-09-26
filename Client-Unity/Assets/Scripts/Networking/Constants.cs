using System;
using System.Net;

public static class Addresses
{
    public static IPAddress Local = IPAddress.Parse("127.0.0.1");
}

public static class Identifiers
{
    private static System.Random RandomGenerator = new();

    /// <summary>
    /// A value generated once at runtime, which is used by the server to identify server-side
    /// objects associated with this player.
    /// NOTE: May be changed later as authentication systems are implemented.
    /// </summary>
    public static long PlayerID;

    static Identifiers()
    {
        RegeneratePlayerID();
    }

    public static void RegeneratePlayerID()
    {
        PlayerID = RandomGenerator.NextLong();
    }
}