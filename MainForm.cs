using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AiTextSummerizer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // ==============================
        // GEMINI API FUNCTION
        // ==============================
        private async Task<string> GetSummaryFromGemini(string inputText)
        {
            string apiKey = "AIzaSyDbxTojAoAmdTqU6IMfaSc9Qe9bpBZKww4";

            using (HttpClient client = new HttpClient())
            {
                string url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=AIzaSyDbxTojAoAmdTqU6IMfaSc9Qe9bpBZKww4";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new
                                {
                                    text = "Summarize this text in simple bullet points:\n\n" + inputText
                                }
                            }
                        }
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                string result = await response.Content.ReadAsStringAsync();

                // ==============================
                // 🔍 DEBUG 1: SHOW RAW RESPONSE
                // ==============================
                Console.WriteLine("RAW API RESPONSE:\n" + result);

                // OPTIONAL: show popup (for debugging only)
                // MessageBox.Show(result);

                dynamic data = JsonConvert.DeserializeObject(result);

                try
                {
                    string summary = data?.candidates?[0]?.content?.parts?[0]?.text;

                    // ==============================
                    // 🔍 DEBUG 2: CHECK SUMMARY
                    // ==============================
                    

                    return summary;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ JSON Parsing Error: " + ex.Message);
                    return "Error parsing AI response";
                }
            }
        }
        private int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            return text.Split(new char[] { ' ', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries).Length;
        }

        // ==============================
        // BUTTON CLICK
        // ==============================
        private async void button1_Click(object sender, EventArgs e)
        {
            string userInput = textBox1.Text.Trim();

            // VALIDATION
            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Please enter text first!");
                return;
            }

            try
            {
                button1.Enabled = false;
                button1.Text = "Generating...";

                // ==============================
                // 🧠 STRICT PROMPT
                // ==============================
                string inputText = $@"
You are a professional summarization engine.

TASK:
Summarize the text below.

RULES (must follow strictly):
- Do NOT use bullet points
- Do NOT add titles like 'Here is the summary'
- Do NOT add introductions or explanations
- Output ONLY the summary text
- Write in ONE continuous paragraph
- Keep it around ONE THIRD of the original length
- Preserve meaning but remove unnecessary details

TEXT:
{userInput}
";

                // CALL API
                string summaryText = await GetSummaryFromGemini(inputText);

                // ==============================
                // 🧠 WORD COUNT (SUMMARY)
                // ==============================
                int wordCount = CountWords(summaryText);

                // OPEN SUMMARY FORM (SEND COUNT TOO)
                summary form = new summary(userInput, summaryText, wordCount);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                button1.Enabled = true;
                button1.Text = "Summarize";
            }
        }
    }
}