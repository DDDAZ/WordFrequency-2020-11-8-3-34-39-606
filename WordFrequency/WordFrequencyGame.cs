﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        public string GetResult(string inputStr)
        {
            var splitedInputsStringArray = Regex.Split(inputStr, @"\s+");
            if (splitedInputsStringArray.Length == 1)
            {
                return inputStr + " 1";
            }
            else
            {
                var inputList = SplitInputStringWithSpace(inputStr);

                //get the map for the next step of sizing the same word
                Dictionary<string, List<Input>> map = GetListMap(inputList);

                List<Input> list = new List<Input>();
                foreach (var entry in map)
                {
                    Input input = new Input(entry.Key, entry.Value.Count);
                    list.Add(input);
                }

                inputList = list;

                inputList.Sort((w1, w2) => w2.WordCount - w1.WordCount);

                var strList = RenderInputList(inputList);

                return string.Join("\n", strList.ToArray());
            }
        }

        private static List<string> RenderInputList(List<Input> inputList)
        {
            List<string> strList = new List<string>();
            foreach (Input w in inputList)
            {
                string s = w.Value + " " + w.WordCount;
                strList.Add(s);
            }

            return strList;
        }

        private static List<Input> SplitInputStringWithSpace(string inputStr)
        {
            string[] splitStrings = Regex.Split(inputStr, @"\s+");

            List<Input> inputList = new List<Input>();
            foreach (var splitString in splitStrings)
            {
                Input input = new Input(splitString, 1);
                inputList.Add(input);
            }

            return inputList;
        }

        private Dictionary<string, List<Input>> GetListMap(List<Input> inputList)
        {
            Dictionary<string, List<Input>> map = new Dictionary<string, List<Input>>();
            foreach (var input in inputList)
            {
                if (!map.ContainsKey(input.Value))
                {
                    List<Input> arr = new List<Input>();
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
