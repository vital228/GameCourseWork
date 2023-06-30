using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    internal class ChatGPTAnswer
    {
        [JsonProperty("choices")] // Set the variable below to represent the json attribute 
        public Choice[] choices;

        public ChatGPTAnswer(Choice[] choices)
        {
            this.choices = choices;
        }
    }
    class Choice
    {
        [JsonProperty("text")] // Set the variable below to represent the json attribute 
        public string text;

        public Choice(string text)
        {
            this.text = text;
        }
    }
}
