using KerongConsole;
using KerongConsole.Common;

KerongService kerongService = new KerongService(
    ConstantKerong.IpAdress,
    ConstantKerong.Port,
    ConstantKerong.Ports);

Console.WriteLine($"Подключаемся к {ConstantKerong.IpAdress} через порты: {string.Join(", ", ConstantKerong.Ports)}");
Console.WriteLine("Введите номер ячейки для ее разблокировки или 'status' для проверки статуса.");

while (true)
{
    Console.Write("> ");
    string? input = Console.ReadLine();

    if (input == null)
    {
        continue;
    }

    if (input.ToLower() == "exit")
    {
        break;
    }

    if (input == "")
    {
        continue;
    }

    if (input.ToLower() == "status")
    {
        bool isOnline = kerongService.Status();
        Console.WriteLine(isOnline ? "Статус подтверждён." : "Статус не подтверждён.");
        continue;
    }

    if (int.TryParse(input, out int number))
    {
        Console.WriteLine($"Вы ввели ячейку: {number}");

        if (kerongService.Unlock(number))
        {
            Console.WriteLine($"Ячейка {number} отправлена на разблокировку.");
        }
        else
        {
            Console.WriteLine($"Не удалось отправить команду разблокировки ячейки {number}.");
        }

        continue;
    }

    Console.WriteLine("Ошибка: введите целое число, 'status' или 'exit'.");
}

Console.ReadKey();
