using KerongConsole.Common;
using KerongConsole.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KerongConsole
{
    public class KerongService
    {
        private CodesClass _codesClass;
        public KerongService() {
            _codesClass = new CodesClass();
        }

        public async void Unlock()
        {            
            using var mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mySocket.Connect(ConstantKerong.IpAdress, ConstantKerong.Port);       // подключемся к удаленному серверу

            using var stream = new NetworkStream(mySocket); // создаем сетевой поток
            Console.WriteLine($"Локальный адрес: {stream.Socket.LocalEndPoint}");// получаем локальный адрес
            Console.WriteLine($"Адрес сервера: {stream.Socket.RemoteEndPoint}"); // получаем адрес сервера
            stream.Write(_codesClass.Unlock1());// отправляем массив байт на сервер 
            Console.WriteLine($"Данные отправлены на сервер {ConstantKerong.IpAdress}");

            // буфер для получения данных
            var responseData = new byte[18];
            var bytes = await stream.ReadAsync(responseData); // получаем данные
                                                              // преобразуем полученные данные в строку
            string test = string.Join(", ", responseData
              .Select(item => "0x" + item.ToString("x2")));
            Console.WriteLine(test);

            Console.WriteLine("Все сообщения отправлены");
        }
    }
}
