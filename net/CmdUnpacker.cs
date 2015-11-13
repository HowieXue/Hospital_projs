using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net
{
    public class CmdUnpacker
    {
        protected CmdUnpacker()
        {

        }

        public virtual Cmd unpack(byte[] buffer, ref long pos)
        {
            throw new Exception("no implementation.");
        }

        public static Cmd parse_cmd(byte[] buffer, long start_index, long length)
        {
            if (start_index + length > buffer.Count())
                throw new Exception("命令长度不足.");
            long pos = start_index;
            CmdType type = (CmdType)Cmd.to_int(buffer, ref pos);
            string type_name = type.ToString();
            type_name = string.Join("", type_name.Split(new char[]{'_'}, StringSplitOptions.RemoveEmptyEntries));
            Type class_type = Type.GetType(type.GetType().Namespace + "." + type_name);
            Cmd cmd = Activator.CreateInstance(class_type) as Cmd;
            cmd.from_byte(buffer, ref start_index);
            return cmd;
        }
    }

    public class RawCmdUnpacker : CmdUnpacker
    {
        public RawCmdUnpacker()
        {

        }

        public override Cmd unpack(byte[] buffer, ref long pos)
        {
            long tmp_pos = pos;
            try
            {
                long size = Cmd.to_long(buffer, ref tmp_pos);
                Cmd cmd = CmdUnpacker.parse_cmd(buffer, tmp_pos, size);
                pos = cmd == null ? pos : tmp_pos + size;
                return cmd;
            }
            catch
            {
            	return null;
            }
        }
    }
}
