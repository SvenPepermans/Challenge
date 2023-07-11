using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Services
{
    public class CombinationService : ICombinationService
    {
        private IEnumerable<string> _possibleResults;
        private int _maxAmountOfCombinations;
        private IEnumerable<string> _wordsToCombine;
        private IEnumerable<string> _wordListWithoutFullWords;
        private List<Combination> _combinedWords = new List<Combination>();

        private Dictionary<string, bool> _verifiedWords = new Dictionary<string, bool>();

        public List<Combination> GetCombinationsInFull(IEnumerable<string> originalWordList, int maxAmountOfCombinations)
        {
            _maxAmountOfCombinations = maxAmountOfCombinations;
            _possibleResults = GetPossibleCombinations(originalWordList);
            _wordListWithoutFullWords = originalWordList.Except(_possibleResults);
            _wordsToCombine = _wordListWithoutFullWords.Where(x => _possibleResults.Any(pr => pr.StartsWith(x)));

            for (int i = 2; i <= _maxAmountOfCombinations; i++)
              {
                  CombineWords(i, new Combination());
              }
            return _combinedWords;
        }

        private IEnumerable<string> GetPossibleCombinations(IEnumerable<string> originalWordList)
        {
            return originalWordList.Where(w => w.Length.Equals(6));
        }

        private bool VerifyWord(string potentialWord)
        {
            if (_verifiedWords.TryGetValue(potentialWord, out bool result))
                return result;
            result = _possibleResults.Contains(potentialWord);
            _verifiedWords[potentialWord] = result;
            return result;           
        }

        private void CombineWords(int amountOfWordsInCombination, Combination currentCombination)
        {
           
           
            if (amountOfWordsInCombination == 0)
            {
                if (VerifyWord(currentCombination.CombinationWord) && currentCombination.CombinationWord.Length == 6)
                {
                     _combinedWords.Add(currentCombination);
                }
                return;
            }

            //// THIS CODE IS NOT CLEAN BUT:
            //// Im trying to reduce the execution time of the recursive part by filtering out loops that would never work out
            //// Example:
            //// "foo", "bar", "foobar"
            //// There is no point of starting a whole recursive loop for the word "bar" (and "foobar", but this is filtered out by default) since no "result word" starts with bar, for this reason if a new cycle is started,
            //// we will use a filtered list that only contains the "starting words"

            if (currentCombination.SubWords.Count == 0)
            {
                foreach (string wordToCombine in _wordsToCombine)
                {
                   NewCombination(currentCombination, wordToCombine, amountOfWordsInCombination);
                }
            }
            else
            {
                foreach (string wordToCombine in _wordListWithoutFullWords)
                {
                    NewCombination(currentCombination, wordToCombine, amountOfWordsInCombination);
                }
            }
        }

        private void NewCombination(Combination currentCombination, string wordToCombine, int amountOfWordsInCombination)
        {
            if (currentCombination.CombinationWord.Length + wordToCombine.Length <= 6)
            {
                //necessary to create new Combination each time otherwise will get f'ed up because of reference
                Combination newCombination = new Combination
                {
                    CombinationWord = currentCombination.CombinationWord + wordToCombine,
                    SubWords = new List<string>(currentCombination.SubWords)
                };
                newCombination.SubWords.Add(wordToCombine);

                CombineWords(amountOfWordsInCombination - 1, newCombination);
            }
        }
    }
}
