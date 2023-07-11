using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Services
{
    public class ReadFileContext
    {
        private IReadFileStrategy _readFileStrategy;

        public void SetReadFileStrategy(string extension)
        {
            if (extension == ".txt")
                _readFileStrategy = new ReadTxtFileStrategy();
            else
            {
                var exception = new ArgumentException($"{extension} is not yet supported.");
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        public List<string> ReadFile(string fileName)
        {
           return _readFileStrategy.ReadFile(fileName);
        }
    }
}
