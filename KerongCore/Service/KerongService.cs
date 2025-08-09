using KerongCore.Helpers;
using System.Net.Sockets;


namespace KerongConsole
{
    public class KerongService : IKerongService
    {
        private CodesClass _codesClass;
        private string _ipAdress;
        private int _port;
        public KerongService(string ipAdress, int port)
        {
            _codesClass = new CodesClass();
            _ipAdress = ipAdress;
            _port = port;
        }

        public async void Unlock(int cell)
        {
           Send(_codesClass.Unlock(cell));
        }

        public void Status()
        {
            Send(_codesClass.GetStatusAll());
        }

        private async void Send(byte[] data)
        {
            using var mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                mySocket.Connect(_ipAdress, _port);       // подключемся к удаленному серверу

                using var stream = new NetworkStream(mySocket); // создаем сетевой поток
                Console.WriteLine($"Локальный адрес: {stream.Socket.LocalEndPoint}");// получаем локальный адрес
                Console.WriteLine($"Адрес сервера:   {stream.Socket.RemoteEndPoint}"); // получаем адрес сервера
                await stream.WriteAsync(data, 0, data.Length); // Асинхронная отправка
                Console.WriteLine($"Данные отправлены на сервер {_ipAdress}");

                // буфер для получения данных
                var responseData = new byte[18];
                var bytes = await stream.ReadAsync(responseData); // получаем данные
                                                                  // преобразуем полученные данные в строку
                string test = string.Join(", ", responseData
                  .Select(item => "0x" + item.ToString("x2")));                
                //mySocket.Close();

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
            }           
        }        
    }

}
