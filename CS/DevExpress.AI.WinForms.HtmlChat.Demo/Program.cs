using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using DevExpress.Data.Utils;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using Microsoft.Extensions.AI;

namespace DevExpress.AI.WinForms.HtmlChat.Demo {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(AzureOpenAIEndpoint, AzureOpenAIKey, new AzureOpenAIClientOptions() {
                Transport = new PromoteHttpStatusErrorsPipelineTransport()
            });
            IChatClient chatClient = azureOpenAIClient.GetChatClient("gpt-4o-mini").AsIChatClient();
            var container = AIExtensionsContainerDesktop.Default;
            container.RegisterChatClient(chatClient);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        class PromoteHttpStatusErrorsPipelineTransport : HttpClientPipelineTransport {
            protected override void OnReceivedResponse(PipelineMessage message, HttpResponseMessage httpResponse) {
                if(!httpResponse.IsSuccessStatusCode) {
                    if((int)httpResponse.StatusCode == 429)
                        throw new Exception("You have reached demo request limit. Further requests are temporarily suspended. Please try again in a few minutes. Thank you for your patience and understanding.");
                    throw new HttpRequestException("HTTP request failed with status code: " + httpResponse.StatusCode);
                }
                base.OnReceivedResponse(message, httpResponse);
            }
        }
        static Uri AzureOpenAIEndpoint {
            get {
                string azureOpenAIEndpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT", IsDeveloperMode);
                if(string.IsNullOrEmpty(azureOpenAIEndpoint))
                    azureOpenAIEndpoint = "https://public-api.devexpress.com/demo-openai";//DevExpress proxy-server            
                return new Uri(azureOpenAIEndpoint);
            }
        }
        static System.ClientModel.ApiKeyCredential AzureOpenAIKey {
            get {
                string azureOpenAIKey = GetEnvironmentVariable("AZURE_OPENAI_API_KEY", IsDeveloperMode);
                if(string.IsNullOrEmpty(azureOpenAIKey))
                    azureOpenAIKey = "DEMO";//Demo key
                return new System.ClientModel.ApiKeyCredential(azureOpenAIKey);
            }
        }
        static bool IsDeveloperMode {
            get {
                return string.Equals(AssemblyInfo.Version, $"{AssemblyInfo.VersionShort}.0.0", StringComparison.InvariantCultureIgnoreCase);
            }
        }
        static string GetEnvironmentVariable(string variableName, bool allowSetNewEnvironmentVariable = false) {
            string environmentVariable = SafeEnvironment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);
            if(string.IsNullOrEmpty(environmentVariable) && allowSetNewEnvironmentVariable) {
                environmentVariable = XtraInputBox.Show($"Please enter {variableName} variable.", variableName, string.Empty);
                if(string.IsNullOrEmpty(environmentVariable))
                    Application.Exit();
                SafeEnvironment.SetEnvironmentVariable(variableName, environmentVariable, EnvironmentVariableTarget.User);
            }
            return environmentVariable;
        }
    }
}
