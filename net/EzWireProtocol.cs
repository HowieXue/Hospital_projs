using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;

namespace net
{
    public class EzWireProtocol : IScsWireProtocol
    {
        private List<byte> _received_data = new List<byte>();

        private CmdUnpacker _unpacker = new RawCmdUnpacker();
        private CmdPacker _packer = new RawCmdPacker();

        public event EventHandler<byte[]> DataReceived;

        public EzWireProtocol()
        {

        }

        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            if (DataReceived != null)
                DataReceived(this, receivedBytes);

            _received_data.AddRange(receivedBytes);

            var msgs = new List<IScsMessage>();

            long pos = 0;
            byte[] buffer = _received_data.ToArray();
            Cmd cmd = _unpacker.unpack(buffer, ref pos);
            while (cmd != null)
            {
                msgs.Add(cmd);
                cmd = _unpacker.unpack(buffer, ref pos);
            }
            
            if (msgs.Count > 0)
            {
                long left_num = _received_data.LongCount() - pos;
                if (left_num > 0)
                {
                    byte[] copy = new byte[left_num];
                    Array.Copy(buffer, pos, copy, 0, copy.LongLength);
                    _received_data = new List<byte>(copy);
                }
                else
                {
                    _received_data.Clear();
                }
            }
            return msgs;
        }

        public byte[] GetBytes(IScsMessage message)
        {
            try
            {
                Cmd cmd = message as Cmd;
                return _packer.pack(cmd).ToArray();
            }
            catch
            {
                return null;
            }
        }

        public void Reset()
        {
            try { _received_data.Clear(); }
            catch { }
        }
    }

    public class EzWireProtocolFactory : IScsWireProtocolFactory
    {
        public IScsWireProtocol CreateWireProtocol()
        {
            return new EzWireProtocol();
        }
    }

    public class EzUdpWireProtocol : IScsWireProtocol
    {
        private CmdUnpacker _unpacker = new RawCmdUnpacker();
        private CmdPacker _packer = new RawCmdPacker();

        public EzUdpWireProtocol()
        {

        }

        public byte[] GetBytes(IScsMessage message)
        {
            try
            {
                Cmd cmd = message as Cmd;
                return _packer.pack(cmd).ToArray();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            var msgs = new List<IScsMessage>();

            long pos = 0;
            byte[] buffer = receivedBytes;
            Cmd cmd = _unpacker.unpack(buffer, ref pos);
            if (cmd != null)
                msgs.Add(cmd);
            
            return msgs;
        }

        public void Reset()
        {
            
        }
    }


}
