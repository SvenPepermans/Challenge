using Challenge.Models;
using Challenge.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Challenge.Tests
{
    public class CombinationServiceTests
    {
        [Fact]
        public void CanMakeCombinationWith2Words()
        {
            var words = new List<string>() { "foo", "bar", "foobar" };
            var completeWords = new List<string>() {"foobar"};
            var combinationService = new CombinationService();
            var expectedResults = new List<Combination>() {
                    new Combination() {
                        SubWords = words.Except(completeWords).ToList(),
                        CombinationWord = "foobar"
                    }};
                          
            var result = combinationService.GetCombinationsInFull(words, 2);
            Assert.Equal("foobar", result.First().CombinationWord);
        }

        [Fact]
        public void CanMakeCombinationWithMoreThen2Words()
        {
            var words = new List<string>() { "fo","o", "b", "ar", "foobar" };
            var completeWords = new List<string>() { "foobar" };
            var combinationService = new CombinationService();
            var expectedResults = new List<Combination>() {
                    new Combination() {
                        SubWords = words.Except(completeWords).ToList(),
                        CombinationWord = "foobar"
                    }};

            var result = combinationService.GetCombinationsInFull(words, 4);
            Assert.Equal("foobar", result.First().CombinationWord);
        }
    }
}