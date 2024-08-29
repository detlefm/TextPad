using System.Text.Json;


namespace TextPad
{

    public partial class Form1 : Form
    {

        private readonly string tempFilePath = Path.Combine(Path.GetTempPath(), "textboxdata.txt");


        public Form1()
        {
            InitializeComponent();
            LoadData();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromArgb(30, 30, 30);
            //this.ForeColor = Color.White;
            textBox1.BackColor = Color.FromArgb(30, 30, 30);
            textBox1.ForeColor = Color.White;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.LostFocus += TextBox1_LostFocus;


        }

        private void TextBox1_LostFocus(object? sender, EventArgs e)
        {
            SaveData();
        }

        private void LoadData()
        {
            if (File.Exists(tempFilePath))
            {
                string jsonData = File.ReadAllText(tempFilePath);
#pragma warning disable CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
                TextBoxData data = JsonSerializer.Deserialize<TextBoxData>(jsonData);
#pragma warning restore CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
                if (data != null)
                {
                    textBox1.Text = data.Content;
                    textBox1.SelectionStart = data.CursorPosition;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    textBox1.Select(data.CursorPosition, 0);
                }
            }
        }

        private void SaveData()
        {
            TextBoxData data = new TextBoxData
            {
                CursorPosition = textBox1.SelectionStart,
                FirstVisibleLine = textBox1.GetLineFromCharIndex(textBox1.SelectionStart),
                Content = textBox1.Text
            };

            string jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(tempFilePath, jsonData);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            SaveData();
        }
    }
    public class TextBoxData
    {
        public int CursorPosition { get; set; }
        public int FirstVisibleLine { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
