using System.Collections.Generic;

namespace Challenge.Services
{
    public interface IReadFileStrategy
    {
        List<string> ReadFile(string fileName);
    }
}
