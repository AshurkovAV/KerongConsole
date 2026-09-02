namespace KerongConsole.Common
{
    public class ConstantKerong
    {
        public const int DefaultPort = 5000;

        public static int Port { get; set; } = DefaultPort;
        public static string IpAdress { get; set; } = "192.168.0.178";

        // В протоколе фактически используется один и тот же TCP-порт, но
        // оставляем список запасных портов для удобства перебора при сбоях.
        public static int[] Ports { get; } = { DefaultPort, 5001, 5002 };
    }
}
