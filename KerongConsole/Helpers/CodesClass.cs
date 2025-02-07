namespace KerongConsole.Helpers
{
    public class CodesClass
    {        
        /// <summary>
        /// Возвращает статусы всех ячеек
        /// </summary>
        /// <returns></returns>
        public byte[] GetStatusAll()
        {
            string sourceStatus = "0x02, 0x00, 0x00, 0x60, 0x03, 0x65";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

        /// <summary>
        /// Открыть 1 ячейку
        /// </summary>
        /// <returns></returns>
        public byte[] Unlock1()
        {
            string sourceStatus = "0x02, 0x00, 0x00, 0x61, 0x03, 0x66";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }


        /// <summary>
        /// Открыть 2 ячейку
        /// </summary>
        /// <returns></returns>
        public byte[] Unlock2()
        {
            string sourceStatus = "0x02, 0x01, 0x00, 0x61, 0x03, 0x67";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

        /// <summary>
        /// Открыть 3 ячейку
        /// </summary>
        /// <returns></returns>
        public byte[] Unlock3()
        {
            string sourceStatus = "0x02, 0x02, 0x00, 0x61, 0x03, 0x68";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

        /// <summary>
        /// Открыть 4 ячейку
        /// </summary>
        /// <returns></returns> 
        public byte[] Unlock4()
        {
            string sourceStatus = "0x02, 0x03, 0x00, 0x61, 0x03, 0x69";
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

    }
}
