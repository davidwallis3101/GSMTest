using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace GSMTest
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    GsmModem.Initialize("COM1");

        //    Console.WriteLine("Press any key");
        //    Console.ReadKey();
        //}
        public static void Main()
        {
            SerialPort UART = new SerialPort("COM1", 115200);
            int read_count = 0;
            byte[] tx_data;
            byte[] rx_data = new byte[10];
            tx_data = Encoding.UTF8.GetBytes("FEZ");
            UART.ReadTimeout = 100;
            UART.Open();

            while (true)
            {
                // flush all data
                //UART.Flush();
                // send some data
                UART.Write(tx_data, 0, tx_data.Length);
                // wait to make sure data is transmitted
                Thread.Sleep(100);
                // read the data
                read_count = UART.Read(rx_data, 0, rx_data.Length);
                if (read_count != 3)
                {
                    // we sent 3 so we should have 3 back
                    Debug.Print("Wrong size: " + read_count.ToString());
                }
                else
                {
                    // the count is correct so check the values
                    // I am doing this the easy way so the code is more clear
                    if (tx_data[0] == rx_data[0])
                    {
                        if (tx_data[1] == rx_data[1])
                        {
                            if (tx_data[2] == rx_data[2])
                            {
                                Debug.Print("Perfect data!");
                            }
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
