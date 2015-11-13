using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net
{
    public class CmdPacker
    {
        protected CmdPacker()
        {

        }

        public virtual List<byte> pack(Cmd cmd)
        {
            throw new Exception("no implementation.");
        }
    }

    public class RawCmdPacker : CmdPacker
    {
        public RawCmdPacker()
        {

        }

        public override List<byte> pack(Cmd cmd)
        {
            List<byte> buffer = cmd.to_byte();
            long size = buffer.Count;
            buffer.InsertRange(0, Cmd.get_bytes(size));
            return buffer;
        }
    }
}
