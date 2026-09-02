using KerongCore.Helpers;
using System.Net.Sockets;

namespace KerongConsole
{
    public class KerongService : IKerongService
    {
        private readonly CodesClass _codesClass;
        private readonly string _ipAdress;
        private readonly List<int> _ports;

        public KerongService(string ipAdress, int port, IEnumerable<int>? ports = null)
        {
            _codesClass = new CodesClass();
            _ipAdress = ipAdress;
            _ports = new List<int>();

            foreach (var candidate in (ports ?? new[] { port }).Where(p => p > 0).Distinct())
            {
                if (!_ports.Contains(candidate))
                {
                    _ports.Add(candidate);
                }
            }

            if (!_ports.Contains(port))
            {
                _ports.Insert(0, port);
            }
        }

        public bool Unlock(int cell)
        {
            return GetUnlockData(cell) != null;
        }

        public byte[]? GetUnlockData(int cell)
        {
            return Send(_codesClass.Unlock(cell), $"unlock {cell}");
        }

        public bool Status()
        {
            var isOk = GetStatusData() != null;
            if (!isOk)
            {
                Console.WriteLine("Проверка статуса: устройство не ответило или ответ невалиден.");
            }

            return isOk;
        }

        public byte[]? GetStatusData()
        {
            return Send(_codesClass.GetStatusAll(), "status");
        }

        private byte[]? Send(byte[] data, string commandName)
        {
            foreach (var port in _ports)
            {
                var response = TrySendOnPort(data, port, commandName);
                if (response != null)
                {
                    return response;
                }
            }

            return null;
        }

        private byte[]? TrySendOnPort(byte[] data, int port, string commandName)
        {
            try
            {
                using var mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mySocket.ReceiveTimeout = 2000;
                mySocket.SendTimeout = 2000;
                mySocket.Connect(_ipAdress, port);

                using var stream = new NetworkStream(mySocket);
                stream.Write(data, 0, data.Length);

                var responseData = new byte[1024];
                int bytesRead = stream.Read(responseData, 0, responseData.Length);

                if (bytesRead <= 0)
                {
                    Console.WriteLine($"[{commandName}] Сервер на {_ipAdress}:{port} закрыл соединение без ответа.");
                    return null;
                }

                var payload = responseData.Take(bytesRead).ToArray();
                string payloadHex = string.Join(", ", payload.Select(b => "0x" + b.ToString("x2")));
                Console.WriteLine($"[{commandName}] Получено {bytesRead} байт с {_ipAdress}:{port}: {payloadHex}");

                if (!IsValidResponse(payload))
                {
                    Console.WriteLine($"[{commandName}] Ответ с {_ipAdress}:{port} не прошёл проверку контрольной суммы.");
                    return null;
                }

                Console.WriteLine($"[{commandName}] Пакет с {_ipAdress}:{port} принят и статус подтверждён.");
                return payload;
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[{commandName}] Ошибка подключения к {_ipAdress}:{port}: {ex.Message}");
                return null;
            }
        }

        private static bool IsValidResponse(byte[] response)
        {
            if (response.Length < 2 || response[0] != 0x02)
            {
                return false;
            }

            int checksum = 0;
            for (int i = 0; i < response.Length - 1; i++)
            {
                checksum += response[i];
            }

            return (checksum & 0xFF) == response[^1];
        }
    }
}
