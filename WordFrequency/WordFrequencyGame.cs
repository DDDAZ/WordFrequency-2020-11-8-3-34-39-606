using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        public string GetResult(string inputStr)
        {
            var splitedInputsStringArray = Regex.Split(inputStr, @"\s+");

            var inputList = SplitInputStringWithSpace(inputStr);

            //get the map for the next step of sizing the same word
            Dictionary<string, List<WordCount>> map = GetListMap(inputList);

            List<WordCount> list = new List<WordCount>();
            foreach (var entry in map)
            {
                WordCount wordCount = new WordCount(entry.Key, entry.Value.Count);
                list.Add(wordCount);
            }

            inputList = list;

            inputList.Sort((wordCount1, wordCount2) => wordCount2.CountedWord - wordCount1.CountedWord);

            var strList = RenderInputList(inputList);

            return string.Join("\n", strList.ToArray());
        }

        private static List<string> RenderInputList(List<WordCount> inputList)
        {
            List<string> strList = new List<string>();
            foreach (WordCount w in inputList)
            {
                string s = w.Value + " " + w.CountedWord;
                strList.Add(s);
            }

            return strList;
        }

        private static List<WordCount> SplitInputStringWithSpace(string inputStr)
        {
            string[] splitStrings = Regex.Split(inputStr, @"\s+");

            List<WordCount> inputList = new List<WordCount>();
            foreach (var splitString in splitStrings)
            {
                WordCount wordCount = new WordCount(splitString, 1);
                inputList.Add(wordCount);
            }

            return inputList;
        }

        private Dictionary<string, List<WordCount>> GetListMap(List<WordCount> inputList)
        {
            Dictionary<string, List<WordCount>> map = new Dictionary<string, List<WordCount>>();
            foreach (var input in inputList)
            {
                if (!map.ContainsKey(input.Value))
                {
                    List<WordCount> arr = new List<WordCount>();
                    arr.Add(input);
                    map.Add(input.Value, arr);
                }
                else
                {
                    map[input.Value].Add(input);
                }
            }

            return map;
        }
    }
}
