namespace KerongCore.Helpers
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
        /// Открыть ячейку
        /// </summary>
        /// <returns></returns> 
        public byte[] Unlock(int cell)
        {
          cell = cell - 1;
            string cell1 = "0x02";
            string cell3 = "0x00";
            string cell4 = "0x61";
            string cell5 = "0x03";
            string cellY = $"0x{cell:x2}";

            int cell_1 = Convert.ToInt32(cell1, 16);
            int cell_2 = Convert.ToInt32(cellY, 16);
            int cell_3 = Convert.ToInt32(cell3, 16);
            int cell_4 = Convert.ToInt32(cell4, 16);
            int cell_5 = Convert.ToInt32(cell5, 16);

            int sum = cell_1 + cell_2 + cell_3 + cell_4 + cell_5;
            string cellsumm = $"0x{sum:x2}";

            string sourceStatus = @$"{cell1}, {cellY}, {cell3}, {cell4}, {cell5}, {cellsumm}";
            Console.WriteLine(sourceStatus);
            return sourceStatus
              .Split(new char[] { ' ', ':', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();
        }

    }
}
