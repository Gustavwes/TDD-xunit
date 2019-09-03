using System;
using System.Collections.Generic;
using System.Text;

namespace TDDCORE.Operators
{
    public class TextAnalyzer
    {
        public int GetTotalWordCount(string inputText)
        {
            var wordCount = 0;
            var index = 0;

            while(index < inputText.Length)
            {
                while (index < inputText.Length && !char.IsWhiteSpace(inputText[index]))
                    index++;

                wordCount++;

                //skip whitespace until next word
                while (index < inputText.Length && char.IsWhiteSpace(inputText[index]))
                    index++;

            }

            return wordCount;
        }
    }
}
