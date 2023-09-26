using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UdpSender
{
    public IPEndPoint destination;
    private UdpClient senderClient;

    public UdpSender(IPEndPoint destination)
    {
        senderClient = new();
        this.destination = destination;
    }

    /// <summary>
    /// Create and sned a UDP packet to the destination from this string.
    /// </summary>
    /// <param name="str">The data string.</param>
    public void Send(string str)
    {
        if (str != null)
            Send(bytes: System.Text.Encoding.ASCII.GetBytes(str));
    }

    /// <summary>
    /// Create and sned a UDP packet to the destination from this byte[].
    /// </summary>
    /// <param name="bytes"></param>
    public void Send(byte[] bytes)
    {
        if (bytes != null && bytes.Length > 0)
            senderClient.Send(bytes, bytes.Length, destination);
    }

    /// <summary>
    /// Kill all threads that are still running inside the controller.
    /// </summary>
    public void Kill()
    {
        senderClient.Close();
        Debug.Log("Closed UDP sender on kill.");
    }
}