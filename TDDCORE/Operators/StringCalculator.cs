using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TDDCORE.Operators
{
    public class StringCalculator
    {
        //can take numbers, seperated by "," "\n" or custom delimiters by adding "//[DELIMITER1][DELIMITER2]" and add them together
        public int Add(string numbers)
        {
            var total = 0;
            if (string.IsNullOrEmpty(numbers))
                return total;

            
             var arrayToIterate = GetStringArrayToIterateThrough(numbers);
            

            foreach (var number in arrayToIterate)
            {
                if (int.TryParse(number.Trim(), out int parsedNumber))
                {
                    if (parsedNumber < 0)
                    {                        
                        throw new InvalidOperationException("Negatives not allowed: " + GetAllNegativeNumbersInFormattedString(arrayToIterate));
                    }
                    if (parsedNumber > 1000)
                        continue;
                    total += parsedNumber;
                }
            }

            return total;
        }

        private string GetAllNegativeNumbersInFormattedString(string[] input)
        {
            var listOfStrings = new List<string>(input);
            var negativeNumbers = listOfStrings.FindAll(x => x.Contains("-"));
            return string.Join(",", negativeNumbers.ToArray());

        }
        private string[] GetStringArrayToIterateThrough(string numbers)
        {
            var stringsToReplaceWithChar = new List<string>() { ",", "\n" };
            if (numbers.StartsWith("//"))
            {
                var delimitersInBrackets = GetStringsWithinSquareBrackets(numbers);
                if (delimitersInBrackets.Count > 0)
                    stringsToReplaceWithChar.AddRange(delimitersInBrackets);
                else
                {
                    var delimiterString = numbers.Split('\n')[0];
                    stringsToReplaceWithChar.Add(delimiterString.Substring(2));
                }
            }
            foreach (var item in stringsToReplaceWithChar)
            {
                numbers = numbers.Replace(item, "|");

            }
            var arrayToIterate = numbers.Split('|');
            return arrayToIterate;
        }

        private List<string> GetStringsWithinSquareBrackets(string input)
        {
            var pattern = new Regex(@"\[(.*?)\]");
            var result = Regex.Matches(input, pattern.ToString());
            var returnList = new List<string>();
            foreach (Match item in result)
            {
                if(!string.IsNullOrWhiteSpace(item.Groups[1].ToString()))
                    returnList.Add(item.Groups[1].ToString());
            }

            return returnList;
        }
    }

}
