using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

public static class HTTPUtility
{
    private static string SWKA_GUID = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
    private static string EOL = "\r\n";

    /// <summary>
    /// Generates a HTTP handshake response for a given request, assuming RegEx matching has already
    /// been validated.
    /// </summary>
    /// <param name="request">The HTTP websocket request.</param>
    /// <returns></returns>
    public static string HandshakeResponse(string request)
    {
        // 1. Obtain the trimmed Sec-WebSocket-Key from the request header.
        // 2. Concatenate it with the SWKA GUID specified by RFC 6455.
        // 3. Compute SHA-1 and Base64 hash of the new value.
        // 4. Generate the response.
        var swk = Regex.Match(request, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
        var swka = swk + SWKA_GUID;
        var swkaSHA1 = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
        var swkaSHA1Base64 = Convert.ToBase64String(swkaSHA1);
        return "HTTP/1.1 101 Switching Protocols" + EOL +
            "Connection: Upgrade" + EOL +
            "Upgrade: websocket" + EOL +
            "Sec-WebSocket-Accept: " + swkaSHA1Base64 + EOL + EOL;
    }

    public static string DecodeClientData(byte[] bytes)
    {
        bool fin = (bytes[0] & 0b10000000) != 0, mask = (bytes[1] & 0b10000000) != 0;
        int opCode = bytes[0] & 0b00001111;
        ulong messageLength = (ulong)(bytes[1] & 0b01111111), offset = 2;
        if (messageLength == 126)
        {
            // Bytes are reversed because websocket will print them in Big-Endian, whereas
            // BitConverter will want them arranged in little-endian on Windows.
            messageLength = BitConverter.ToUInt16(new byte[] { bytes[3], bytes[2] }, 0);
            offset = 4;
        }
        else if (messageLength == 127)
        {
            // To test the below code, we need to manually buffer larger messages — since the NIC's
            // autobuffering may be too latency-friendly for this code to run.
            messageLength = BitConverter.ToUInt64(new byte[] { bytes[9], bytes[8], bytes[7], bytes[6], bytes[5], bytes[4], bytes[3], bytes[2] }, 0);
            offset = 10;
        }
        if (messageLength == 0)
        {
            Console.WriteLine("msglen == 0");
            return "";
        }
        else if (!mask)
        {
            Console.WriteLine("DecodeClientData(): mask bit not set.");
            return "";
        }
        byte[] decoded = new byte[messageLength];
        byte[] masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
        offset += 4;
        for (ulong i = 0; i < messageLength; ++i)
            decoded[i] = (byte)(bytes[offset + i] ^ masks[i % 4]);
        var data = Encoding.UTF8.GetString(decoded);
        // Console.WriteLine("DecodeClientData(): Decoded message:\n{0}", data);
        return data;
    }
}

public static class Conversion
{
    public static string ToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }

    public static byte[] ToBytes(string s)
    {
        return Encoding.UTF8.GetBytes(s);
    }
}