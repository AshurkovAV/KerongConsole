using KerongConsole;
using KerongConsole.Common;
using System;

KerongService kerongService = new KerongService(ConstantKerong.IpAdress, ConstantKerong.Port);

Console.WriteLine("Введите номер ячейки для ее разблокировки");

while (true) // Бесконечный цикл
{
    Console.Write("> "); // Приглашение для ввода
    string input = Console.ReadLine(); // Ожидание ввода

    if (input?.ToLower() == "exit") // Проверка на выход
        break;
    if (input == "") // Проверка на выход
        continue;

    // Пытаемся преобразовать ввод в число
    if (int.TryParse(input, out int number))
    {        
        Console.WriteLine($"Вы ввели ячейку : {input}");
        kerongService.Unlock(Convert.ToInt32(input));
    }
    else
    {
        Console.WriteLine("Ошибка: введите целое число или 'exit' для выхода.");
    }
    // kerongService.Status();
}




Console.ReadKey();
