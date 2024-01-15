
using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal sealed class NewtonJson : JsonInterface
    {
        public void Serialize()
        {

        }

        public T Deserialize<T>(string responseData)
        {
            T deserialized = JsonConvert.DeserializeObject<T>(responseData);

            return deserialized;
        }
    }
}
