using Azure;
using Azure.AI.OpenAI;

using OpenAI_API;
using OpenAI_API.Completions;
using System;

namespace SpeechContinuousRecognition.OpenAI
{
	 public class OpenAIHelper
	{
		static public async Task<string> GetResultAsync(string apiKey, string prompt)
		{
			var api = new OpenAIAPI(apiKey);
			api.Completions.DefaultCompletionRequestArgs.MaxTokens = 1000;
            OpenAI_API.Models.Model model = new OpenAI_API.Models.Model("text-davinci-003");
            CompletionResult result;
            try
			{
                result = await api.Completions.CreateCompletionAsync(prompt, "text-davinci-003", 1000);
            }
			catch (Exception exception)
			{
                await Console.Out.WriteLineAsync(exception.Message);
                throw;
			}
			Console.WriteLine(result.Completions[0].Text);
			return result.Completions[0].Text;
		}
		  static public async Task<string> GetResultAzureAsync(string apiKey, string prompt, string systemPrompt)
		{
			var clientOptions = new OpenAIClientOptions();
			var client = new OpenAIClient(apiKey, clientOptions);
			//var completionsOptions = new CompletionsOptions() { Prompts=  new List<string>() { systemPrompt,prompt} };
			CompletionsOptions completionsOptions = new CompletionsOptions()
			{
				Prompts = { systemPrompt,prompt }
			};

			completionsOptions.MaxTokens = 100;
			completionsOptions.User =  $"{systemPrompt} \n\n {prompt}";
			completionsOptions.Temperature = 0.9f;
			completionsOptions.ChoicesPerPrompt = 1;
			 var  response = await client.GetCompletionsAsync(
					deploymentOrModelName: "text-davinci-003",
					completionsOptions);
			  var result= response.Value.Choices[0].Text;
			 return result ?? "";
		}
	}
}