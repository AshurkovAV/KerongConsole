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

        public async void Status()
        {
            Send(_codesClass.GetStatusAll());
        }

        private async void Send(byte[] data)
        {
            using var mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await mySocket.ConnectAsync(_ipAdress, _port);

                using var stream = new NetworkStream(mySocket); // создаем сетевой поток
                Console.WriteLine($"Локальный адрес: {stream.Socket.LocalEndPoint}");// получаем локальный адрес
                Console.WriteLine($"Адрес сервера:   {stream.Socket.RemoteEndPoint}"); // получаем адрес сервера
                await stream.WriteAsync(data, 0, data.Length); // Асинхронная отправка
                Console.WriteLine($"Данные отправлены на сервер {_ipAdress}");


                // Даём серверу время на обработку (если нужно)
                await Task.Delay(100);


                // буфер для получения данных
                var responseData = new byte[1024];
                int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);

                if (bytesRead == 0)
                {
                    Console.WriteLine("Сервер закрыл соединение, не отправив данные.");
                    return;
                }

                // Выводим только реально полученные байты
                string test = string.Join(", ", responseData.Take(bytesRead).Select(b => "0x" + b.ToString("x2")));
                Console.WriteLine($"Получено {bytesRead} байт: {test}");
                //mySocket.Close();

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
            }           
        }        
    }

}
