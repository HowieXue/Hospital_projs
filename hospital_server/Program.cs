using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using net;

namespace hospital_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BitConverter.IsLittleEndian);

            List<byte> buffer = new List<byte>();
            buffer.AddRange(Cmd.get_bytes("123456780"));
            buffer.AddRange(Cmd.get_bytes("柳映辉"));
            buffer.AddRange(Cmd.get_bytes(null));
            buffer.AddRange(Cmd.get_bytes(""));
            buffer.AddRange(Cmd.get_bytes("天津市"));

            byte[] buf = buffer.ToArray();
            long pos = 0;
            string s = Cmd.to_string(buf, ref pos);
            Console.WriteLine(s);
            s = Cmd.to_string(buf, ref pos);
            Console.WriteLine(s);
            s = Cmd.to_string(buf, ref pos);
            Console.WriteLine(s);
            s = Cmd.to_string(buf, ref pos);
            Console.WriteLine(s);
            s = Cmd.to_string(buf, ref pos);
            Console.WriteLine(s);

            
            buffer.Clear();
            buffer.AddRange(Cmd.get_bytes(false));
            buffer.AddRange(Cmd.get_bytes(100.0d));
            buffer.AddRange(Cmd.get_bytes(100.0f));
            buffer.AddRange(Cmd.get_bytes((int)100));
            buffer.AddRange(Cmd.get_bytes((long)100));
            buffer.AddRange(Cmd.get_bytes((short)200));

            buf = buffer.ToArray();
            pos = 0;
            Console.WriteLine(Cmd.to_bool(buf, ref pos));
            Console.WriteLine(pos);
            Console.WriteLine(Cmd.to_double(buf, ref pos));
            Console.WriteLine(pos);
            Console.WriteLine(Cmd.to_float(buf, ref pos));
            Console.WriteLine(pos);
            Console.WriteLine(Cmd.to_int(buf, ref pos));
            Console.WriteLine(pos);
            Console.WriteLine(Cmd.to_long(buf, ref pos));
            Console.WriteLine(pos);
            Console.WriteLine(Cmd.to_short(buf, ref pos));
            Console.WriteLine(pos);

            Cmd cmd = new IdentityCmd();
            cmd.no = 100;
            buffer = new RawCmdPacker().pack(cmd);
            pos = 0;
            Cmd cc = new RawCmdUnpacker().unpack(buffer.ToArray(), ref pos);

            UdpClient client = new UdpClient(10000);
            for (int i = 0; i < 10;i++ )
            {
                client.Send(buffer.ToArray(), buffer.Count, new IPEndPoint(IPAddress.Parse("224.1.1.100"), 32001));
                Thread.Sleep(1000);
            }


            Console.ReadKey();
        }
    }
}
