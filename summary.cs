using System;
using System.Drawing;
using System.Windows.Forms;

namespace AiTextSummerizer
{
    public partial class summary : Form
    {
        public summary(string inputText, string summaryText, int wordCount)
        {
            InitializeComponent();

            // =========================
            // FORM STYLE (LIGHT BLUE UI)
            // =========================
            this.BackColor = Color.LightSkyBlue;
            this.Text = "AI Summary Result";

            // =========================
            // TEXTBOX 1 → ORIGINAL TEXT
            // =========================
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ReadOnly = true;
            textBox1.Font = new Font("Segoe UI", 10);
            textBox1.ForeColor = Color.DarkBlue;
            textBox1.Text = inputText;

            // =========================
            // TEXTBOX 2 → SUMMARY TEXT
            // =========================
            textBox2.Multiline = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.ReadOnly = true;
            textBox2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            textBox2.ForeColor = Color.DarkBlue;
            textBox2.Text = summaryText;

            button1.Text = $"Words: {wordCount}";
        }

        private void summary_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        // =========================
        // COPY BUTTON (SUMMARY ONLY)
        // =========================
        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
            MessageBox.Show("Summary copied to clipboard!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
            MessageBox.Show("Summary copied to clipboard!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}