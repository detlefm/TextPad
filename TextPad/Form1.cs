using System.Text.Json;


namespace TextPad
{

    public partial class Form1 : Form
    {

        //private readonly string tempFilePath = Path.Combine(Path.GetTempPath(), "textboxdata.txt");
        private const string dataFileName = "TextPad.txt";
        private readonly string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string documentsFileName = "";
        private const int initialFontSize = 10;
        private int currentFontSize = initialFontSize;
        private const int fontSizeIncrement = 2;
        private int tabSize = 4;


        public Form1()
        {
            InitializeComponent();
            documentsFileName = Path.Combine(documentsPath, dataFileName);
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
            textBox1.Font = new Font(textBox1.Font.FontFamily, currentFontSize);
        }


        private void TextBox1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Oemplus:
                        IncreaseFontSize();
                        break;
                    case Keys.OemMinus:
                        DecreaseFontSize();
                        break;
                    default:
                        return;
                }           
            } else
            {
                if (e.KeyCode == Keys.Tab)
                {
                    InsertTab();
                } else
                {
                    return;
                }
            }  
            e.SuppressKeyPress = true; 
        }

     

        private void IncreaseFontSize()
        {
            if (currentFontSize + fontSizeIncrement <= 24) // Begrenzung auf 24
            {
                currentFontSize += fontSizeIncrement;
                textBox1.Font = new Font(textBox1.Font.FontFamily, currentFontSize);
            }
        }

        private void DecreaseFontSize()
        {
            if (currentFontSize - fontSizeIncrement >= 8) // Begrenzung auf 8
            {
                currentFontSize -= fontSizeIncrement;
                textBox1.Font = new Font(textBox1.Font.FontFamily, currentFontSize);
            }
        }

        private void InsertTab()
        {
            int currentPosition = textBox1.SelectionStart;
            //int tabSize = tabSize; // Tabulatorgröße
            int nextTabStop = ((currentPosition / tabSize) + 1) * tabSize;
            int spacesToInsert = nextTabStop - currentPosition;

            string spaces = new String(' ', spacesToInsert);
            textBox1.Text = textBox1.Text.Insert(currentPosition, spaces);
            textBox1.SelectionStart = currentPosition + spacesToInsert;
        }


        private void TextBox1_LostFocus(object? sender, EventArgs e)
        {
            SaveData();
        }

        private void LoadData()
        {
            string tempFilePath = documentsFileName;
            if (File.Exists(tempFilePath))
            {
                string jsonData = File.ReadAllText(tempFilePath);
#pragma warning disable CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
                TextBoxData data = JsonSerializer.Deserialize<TextBoxData>(jsonData);
#pragma warning restore CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
                if (data != null)
                {
                    currentFontSize = data.CurrentFontSize;
                    textBox1.Font = new Font(textBox1.Font.FontFamily, currentFontSize);
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
                Content = textBox1.Text,
                CurrentFontSize = currentFontSize
            };

            string jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(documentsFileName, jsonData);
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            SaveData();
        }


        private void TextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true; 
            }
        }
    }
    public class TextBoxData
    {
        public int CursorPosition { get; set; }
        public int FirstVisibleLine { get; set; }
        public string Content { get; set; } = string.Empty;

        public int CurrentFontSize { get; set; } = 10;
    }
}
