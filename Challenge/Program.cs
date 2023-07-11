using Challenge.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Challenge
{
    public static class Program
    {
        public static IServiceProvider ServiceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            Console.Write("Max amount of combinations (number): ");
            int maxAmount = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input the filepath: ");
            string? filepath = Console.ReadLine();
            string? fileExtension = Path.GetExtension(filepath);

            //Used this "specific" Strategy context in assumption that different source = different filetype (that's why i let user choose file) and f.e. not a database.
            //If this would be the case, then a more generalized strategycontext would be used something like "DataSourceStrategy"
            ReadFileContext context = new ReadFileContext();
            context.SetReadFileStrategy(fileExtension);
            var listOfWords = context.ReadFile("input.txt");
           
            var combinationService = ServiceProvider.GetService<ICombinationService>();
            var sw = new Stopwatch();
            sw.Start();
            var allCombinations = combinationService.GetCombinationsInFull(listOfWords, maxAmount);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms");
            Console.WriteLine(allCombinations.Count + " combinations");
            foreach (var combination in allCombinations)
            {
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < combination.SubWords.Count; i++)
                {
                    sb.Append(combination.SubWords[i]);
                    if(i < combination.SubWords.Count-1)
                        sb.Append(" + ");
                }
                sb.Append($" = {combination.CombinationWord}");
                Console.WriteLine(sb.ToString());
            }



        } 
        
        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<ICombinationService, CombinationService>();
            ServiceProvider = services.BuildServiceProvider();
            
        }
    }
}

