using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Client
{
    private static TcpClient client;
    private static NetworkStream stream;

    public static void Main()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 5000);
            stream = client.GetStream();

            Task.Run(() => ReceiveMessages());

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
        finally
        {
            client?.Close();
        }
    }

    private static void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("[Servidor]: " + message);
        }
    }
}
