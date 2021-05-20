using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;

namespace TcpListenerApp
{
    class Program
    {
        const int port =8080;
        
        static void Main(string[] args)
        {
            
            
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("37.252.87.66");
                server = new TcpListener(localAddr, port);

                // запуск слушателя
                server.Start();

                
                    Console.WriteLine("Ожидание подключений... ");

                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();
                if (stream.CanRead)
                {
                    //int num = stream.Read(name,0,(int)stream.Length);
                    //Console.WriteLine(num);
                    for (int i = 0; i < 17; i++)
                    {
                        
                        Console.Write(Convert.ToString(stream.ReadByte()));

                    }
                }
                Console.WriteLine();

              
                byte[] data = { 1, 0 };//BitConverter.GetBytes(n); 
              
                    stream.Write(data, 0, data.Length);
                int size = 0;
              string hex;
                    Console.WriteLine("Отправлено сообщение: {0}", data);
                if (stream.CanRead)
                {
                    using (StreamWriter writetext = new StreamWriter("write2.txt"))
                        while (size!=1225)
                    {
                        
                            hex = stream.ReadByte().ToString("X");
                            
                            if (hex.Length == 1)
                            {
                                writetext.Write( hex );
                              Console.Write(hex+"-");

                            }
                         
                            else
                            {
                                Console.Write( hex+"-" );
                             
                                writetext.Write(hex );
                             
                            }
                            size++;
                        }

             
                }
                Console.WriteLine(size);
                stream.Close();

                    client.Close();
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
           

        }
    }
}