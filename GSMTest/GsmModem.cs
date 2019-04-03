using System;
using System.IO.Ports;
using System.Text;

namespace GSMTest
{
    public static class GsmModem
    {
        private static SerialPort sp;

        public static void Initialize(string portName)
        {
            Log($"Initialising GSM Modem on port {portName}");
            sp = new SerialPort(portName)
            {
                Encoding = Encoding.UTF8,
                BaudRate = 9600,
                // sp.Handshake = Handshake.XOnXOff;

                //DataBits = 8,
                //Parity = Parity.None,
                //StopBits = StopBits.One
            };

            //sp.ReadTimeout = 1000;
            //sp.WriteTimeout = 1000;
            sp.Open();

            
            Log("Turning local echo off");
            sp.Write("ATE0"); // Local Echo off
            System.Threading.Thread.Sleep(100); // make sure data sent
          
            int totalBytes = sp.BytesToRead;

            if (totalBytes > 0)
            {

                Console.WriteLine(sp.ReadLine());
            }
            else
            {
                Console.WriteLine("No Bytes");
            }

            Log("Switching to data mode");
            sp.Write("AT+CMGF=1"); // SMS Mode
            //System.Threading.Thread.Sleep(500);
            Console.WriteLine(sp.ReadLine());
        }


        public static bool SendSMS(string number, string msg)
        {
            // Add 44 to start and trim leading 0 if not present (UK)
            if (!number.StartsWith("+44")) { number = "+44" + number.TrimStart(new char[] { '0' }); }

            Log(String.Format("Attempting to send text message to {0}", number));

            // Trim message if it exceeds 160 characters
            if (msg.Length > 160)
            {
                Log("Message exceeds permitted length, trimming");
                msg = msg.Substring(0, 160);
            }

      
            // Send SMS
            sp.Write("AT+CMGS=\"" + number + "\"\r\n");

            Log(string.Format("Sending SMS: {0}", msg.Trim() + (Char)26));
            sp.Write(msg.Trim() + (Char)26);
            return true;
        }

        private static void Log(string messageText)
        {
            Console.WriteLine(messageText);
        }
    }
}
