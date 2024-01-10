using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface JsonInterface
    {
        void Serialize();

        T Deserialize<T>(string responseData);
    }
}
