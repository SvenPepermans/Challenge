using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Challenge.Services
{
    public class ReadTxtFileStrategy : IReadFileStrategy
    {
        public List<string> ReadFile(string fileName)
        {
            try
            {
                return File.ReadAllLines(fileName).ToList();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
