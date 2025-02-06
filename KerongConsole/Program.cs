using KerongConsole;
using KerongConsole.Common;





KerongService kerongService = new KerongService(ConstantKerong.IpAdress, ConstantKerong.Port);

kerongService.Unlock();

Console.ReadKey();
