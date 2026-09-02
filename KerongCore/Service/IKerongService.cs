namespace KerongConsole
{
    public interface IKerongService
    {
        bool Status();
        bool Unlock(int cell);
    }
}