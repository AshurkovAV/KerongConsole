namespace KerongConsole.Helpers
{
    public class CodesClass
    {        
        private byte[] GetStatusAll()
        {
            string sourceStatus = "0x02, 0x00, 0x00, 0x60, 0x03, 0x65";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

        public byte[] Unlock1()
        {
            string sourceStatus = "0x02, 0x00, 0x00, 0x61, 0x03, 0x66";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

    }
}
