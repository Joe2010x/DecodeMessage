using System;
using System.Linq;

namespace DecodeMessage
{
    internal class Program
    {
        public static string GenerateRndWord(string wordStr, int length) => new string(Enumerable.Range(0, length).Select(_ => wordStr[new Random().Next(wordStr.Length)]).ToArray()); 
        private static string CipherText(string wordStr, string codeWord) => string.Join("", codeWord.Select(c => wordStr.IndexOf(c).ToString()).ToList());
        public static string ConvertToWord(string wordStr, char charX) => wordStr[Convert.ToInt32(char.GetNumericValue(charX))].ToString();
        public static List<string> AddHeadToTail(string head, List<string> tail) => tail.Select(t => head + t).ToList();
        public static void PrintList(List<string> list) => list.ForEach(l => Console.WriteLine(l));

        private static List<string> DecipherText(string wordStr, string ciferText)
        {
            if (ciferText.Length == 0) return new List<string>() { "" };
            if (ciferText.Length == 1 ) return new List<string>() { ConvertToWord(wordStr, ciferText[0]) };
            var result = new List<string>();
            if (int.TryParse(ciferText.Substring(0, Math.Min(2, ciferText.Length)), out int index) && index < wordStr.Length)
            {
                var twoCharHead = wordStr[index].ToString();
                var twoCharTail = DecipherText(wordStr, ciferText.Length <= 2 ? "" : ciferText.Substring(2));
                result = AddHeadToTail(twoCharHead, twoCharTail);
            }
            var head = ConvertToWord(wordStr, ciferText[0]);
            var tail = DecipherText(wordStr, ciferText.Substring(1));
            result.AddRange(AddHeadToTail(head, tail));
            return result;
        }

        static void Main(string[] args)
        {
            string? targetWord = null;
            targetWord = "abba";
            var wordStr = " abcdefghijklmnopqrstuvwxyz";
            
            var wordLength = 5;
            var codeWord = targetWord ?? GenerateRndWord(wordStr, wordLength);
            Console.WriteLine("codeWord is " + codeWord);

            var ciferText = CipherText(wordStr, codeWord);
            Console.WriteLine("ciferedText is " + ciferText);

            var deciferText = DecipherText(wordStr, ciferText);
            PrintList(deciferText);
        }
    }
}