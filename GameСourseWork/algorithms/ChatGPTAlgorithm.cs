using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Drawing;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace GameСourseWork.algorithms
{
    public class ChatGPTAlgorithm : IArtificialIntelligence
    {
        private readonly string apiKey = "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";
        private readonly string apiUrl = "https://api.openai.com/v1/engines/davinci/completions";
        private string model = "text-davinci-003";
        string prompt;
        string promptEnd = "Print your answer.";
        string promptStart = "Let's play a game. You should respond with one of the following characters: {'U', 'L', 'R', 'D'}, which will indicate where you will go in the game. Game Rules:\\nThe playing field is divided into 81 cells (9 in each row). The squares of the field consist of several layers. The number of cell layers of the field is symmetrical relative to the center. \\nThe first player occupies the upper left cell with an indentation of one cell from the walls, the second, similarly, occupies the lower right cell. Players take turns making moves; the first player starts. Each move is a move of a player to one of the adjacent cells. Two players cannot be in the same cell at the same time. After a player's move, one layer disappears from the cell where the player was. If the layer was the last, the players will know that the cell no longer exists. \\nThe game ends when some player fails to make a move or makes a wrong move: goes to a cell that does not exist, or beyond the boundaries of the field. \\nThe goal of the game is to make as many moves as possible.\\nI will send you a 9 by 9 playing field with information about the cell(1 - there are layers, 0 - no layers), and then in new lines your and your opponent's coordinates. Example: \\nInput:\\n1 1 1 1 1 1 0 0 1\\n1 0 1 1 1 1 0 0 1\\n1 1 1 1 1 1 0 0 1\\n1 1 1 1 1 1 0 0 1\\n1 1 0 1 1 1 0 0 1\\n1 1 1 1 1 1 0 0 1\\n1 1 1 0 1 1 0 0 1\\n1 1 1 1 1 1 0 0 1\\n1 1 1 1 1 1 0 0 1\\n{X=0,Y=0} \\n{X=8,Y=8} \\nYour Answer:\\nR\\nAnd the game is on.";
        private int maxTokens = 50;
        private string temperature = "0.5";
        int stepCount = 0;

        public ChatGPTAlgorithm()
        {
            prompt = promptStart;
            
            //prompt += "Your previos steps in Game:\\n";
        }

        public string Name { get => "ChatGPT"; set => throw new NotImplementedException(); }

        public async Task<string> GenerateCompletion(string prompt)
        {
            string requestBody = $@"
            {{
                ""prompt"": ""{prompt}"",
                ""max_tokens"": {maxTokens},
                ""temperature"": {temperature},
                ""stop"": [""Input:""]
            }}";
            //""stop"": [""R"", ""L"",""U"",""D"","".""]""model"": ""{model}"",
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"OpenAI API request failed: {responseContent}");
                    throw new Exception($"OpenAI API request failed: {responseContent}");
                    
                }

                return responseContent;
            }
        }

        public void Reset()
        {
        }

       

        public void EndGame(bool win)
        {
            if (win)
            {
                prompt += "You won. Good for you. Next game.\\n";
            }
            else
            {
                prompt += "You lost. Try harder. Next game.\\n";
            }
        }

        public char step(int[,] board, Point player, Point opponent)
        {
            string boardString = ToPromt(board, player, opponent);
            try
            {
                
                string response = Task.Run(async () => await GenerateCompletion(prompt+boardString+"Your Answer:\\n")).GetAwaiter().GetResult();
                //Console.WriteLine(response);
                var json =JsonConvert.DeserializeObject<ChatGPTAnswer>(response);
                var text = json.choices[0].text;
                //Console.WriteLine(text);
                //prompt += text[text.Length - 2] + ", ";
                stepCount++;
                return text[text.Length-2];
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ' ';
            }
        }


        private string ToPromt(int[,] board, Point player, Point opponent)
        {
            string s = "Input:\\n";
            for (int i=0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    s += board[i, j] + " ";
                }
                s += "\\n";
            }
            s += player.ToString() + "\\n";
            s += opponent.ToString() + "\\n";
            return s;
        }

        public void ReportGameEnd(bool win)
        {
            EndGame(win);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }

}
