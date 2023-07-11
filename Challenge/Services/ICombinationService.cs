using Challenge.Models;
using System.Collections.Generic;

namespace Challenge.Services
{
    public interface ICombinationService
    {
        List<Combination> GetCombinationsInFull(IEnumerable<string> originalWordList, int maxAmountOfCombinations);
       
    }
}
