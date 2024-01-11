

namespace ConsoleApp1
{
    internal interface JsonInterface
    {
        void Serialize();

        T Deserialize<T>(string responseData);
    }
}
