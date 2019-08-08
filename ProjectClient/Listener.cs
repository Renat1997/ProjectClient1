using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace ProjectClient
{
    class Listener
    {
        public Listener(Form1 form)
        {
            form.Start += Start;

            form.Stop += Stop;

            form.Send += showMes;
        }


        private TcpClient client;
        private CancellationTokenSource tokenSource;



        //создание, ожидание клиента
        private void StartListener(int port)
        {

            Task.Run(() =>
            {
                TcpListener server = null;
                try
                {
                    int portInt = 0;
                    try
                    {
                        portInt = Convert.ToInt32(port); // конвертация из стринг в инт

                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine(ex.Message);
                    }

                    IPAddress adr = IPAddress.Parse("127.0.0.1");

                    server = new TcpListener(adr, portInt);

                    server.Start();

                    while (!tokenSource.Token.IsCancellationRequested)
                    {
                        if (client == null || !client.Connected)
                            client = server.AcceptTcpClient();

                        Thread.Sleep(1);
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
                finally
                {
                    server.Stop();
                }

            });


        }


        //проверка состояния
        private void showMes(string state)
        {
            if (!tokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    if (client != null && client.Connected == true)
                    {
                        NetworkStream stream = client.GetStream();
                        byte[] msg = System.Text.Encoding.Unicode.GetBytes(state);
                        stream.Write(msg, 0, msg.Length);
                    }
                }
                catch
                {

                }
            }
        }

        //старт листенера
        private void Start(int port)
        {
            tokenSource = new CancellationTokenSource();
            StartListener(port);
        }

        //стоп потоков
        private void Stop()
        {

            tokenSource.Cancel();
        }
    }
}

