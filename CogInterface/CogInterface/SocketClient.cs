using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CogInterface
{
    class SocketClient
    {

        //  http://weblogs.asp.net/dwahlin/archive/2008/06/08/creating-a-silverlight-2-client-access-policy-socket-server.aspx

        static string _PolicyRequestString = "<policy-file-request/>";

        //static void Main(string[] args)
        public static void startSocketClient()
        {
            TcpClient client = new TcpClient();

            IPEndPoint serverEndPoint =
               new IPEndPoint(IPAddress.Parse("127.0.0.1"), 943);

            client.Connect(serverEndPoint);

            NetworkStream clientStream = client.GetStream();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_PolicyRequestString);
            clientStream.Write(bytes, 0, bytes.Length);
            StreamReader reader = new StreamReader(clientStream);
            while (true)
            {
                try
                {
                    char[] chars = new char[1024];
                    int i = 0;
                    while ((i = reader.Read(chars, 0, 1024)) > 0)
                    {
                        string val = new String(chars, 0, i);
                        Console.WriteLine(val);
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    break;
                }
            }
            System.Diagnostics.Debug.WriteLine("");
            //Console.Read();
        }



    }//end class SocketClient
}// end namespace
