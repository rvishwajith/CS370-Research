/*
 * ConsoleServer.cs
 * 
 * Contains the console server class, which is the main class for running the console version of 
 * the server.
 * 
 * References:
 * https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

public class ConsoleServer
{
    public void Init()
    {
        try
        {
            var listener = CreateListener(port: 32000);
            listener.Start();
            Console.WriteLine("Listener awaiting web client.");
            PollWebClient(listener.AcceptTcpClient());
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to setup TCP listener.");
            Console.WriteLine("Init() Stack Trace: " + e.StackTrace);
            return;
        }
    }

    private void PollWebClient(TcpClient client)
    {
        Console.WriteLine("Listener accepted client from EP: {0}", client.Client.RemoteEndPoint);
        try
        {
            var stream = client.GetStream();
            var isCertified = false;
            while (true)
            {
                while (!stream.DataAvailable || client.Available == 0) { };

                var bytes = new byte[client.Available];
                stream.Read(bytes, 0, bytes.Length);
                Console.WriteLine("Recieved {0} bytes from client.", bytes.Length);

                if (isCertified)
                {
                    var s = HTTPUtility.DecodeClientData(bytes);
                    Console.WriteLine("Recieved HTTP websocket message from client:" +
                        "\n----------\n{0}\n----------\n", s);
                }
                else
                {
                    var s = Conversion.ToString(bytes);
                    if (Regex.IsMatch(s, "GET"))
                    {
                        Console.WriteLine("Recieved HTTP handshake request from client.");
                        stream.Write(Conversion.ToBytes(HTTPUtility.HandshakeResponse(s)));
                        Console.WriteLine("Sent HTTP handshake response to client.");
                        isCertified = true;
                    }
                }
                // Limit the speed at which events can be sent from the client to reduce thread's
                // performance impact. Low latency is not needed, so 20 packets/s (50 ms) is fine.
                Thread.Sleep(50);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("PollWebClient*() ERROR: Client was closed for an unknown reason.");
            Console.WriteLine("Stack Trace: " + e.StackTrace);
            return;
        }
    }

    private TcpListener CreateListener(string address = "127.0.0.1", int port = 0)
    {
        Console.WriteLine("Creating listener at {0}:{1}", address, port);
        return new TcpListener(IPAddress.Parse(address), port);
    }
}
