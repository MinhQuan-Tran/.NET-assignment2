using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Editor : Form
    {
        private String userName;
        private UserType userType;
        private FontStyle bold = FontStyle.Regular;
        private FontStyle italic = FontStyle.Regular;
        private FontStyle underline = FontStyle.Regular;
        private float fontSize;

        public Editor(String title, String userName, UserType userType)
        {
            InitializeComponent();

            this.userName = userName;
            this.userType = userType;

            this.fontSize = float.Parse(fontSizeToolStripComboBox.Text);

            // set title of this window
            this.Text = title;

            if (!title.Equals("*blank"))
            {
                editorRichTextBox.LoadFile(title);
            }

            // set User Name
            userNameToolStripLabel.Text = userName;

            // set read-only property based on User-Type
            if (userType == UserType.Edit)
            {
                editorRichTextBox.ReadOnly = false;
                editorRichTextBox.Enabled = true;
            }
            else
            {
                editorRichTextBox.ReadOnly = true;
                editorRichTextBox.Enabled = false;
            }

            changeFont();
        }

        // About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAbout();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            showAbout();
        }

        private void showAbout()
        {
            MessageBox.Show("MyTextEditor\nVersion 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createNewFile();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            createNewFile();
        }

        private void createNewFile()
        {
            Editor editor = new Editor("*blank", this.userName, this.userType);
            editor.Show();
        }

        // Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Editor editor = new Editor(openFileDialog.FileName, this.userName, this.userType);
                editor.Show();
            }
        }

        // Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveFile()
        {
            if (this.Text.Equals("*blank"))
            {
                saveAsFile();
            }
            else
            {
                editorRichTextBox.SaveFile(this.Text);
            }
        }

        // Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        private void saveAsFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.rtf";
            saveFileDialog.Filter = "RTF Files|*.rtf|Text File|*.txt";

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (DialogResult.OK == dialogResult)
            {
                editorRichTextBox.SaveFile(saveFileDialog.FileName);
                this.Text = saveFileDialog.FileName;
            }
        }

        // Logout
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Cut
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorRichTextBox.Cut();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            editorRichTextBox.Cut();
        }

        // Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorRichTextBox.Copy();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            editorRichTextBox.Copy();
        }

        // Paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorRichTextBox.Paste();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            editorRichTextBox.Paste();
        }

        // Bold
        private void boldToolStripButton_Click(object sender, EventArgs e)
        {
            if (boldToolStripButton.Checked)
            {
                this.bold = FontStyle.Bold;
            }
            else
            {
                this.bold = FontStyle.Regular;
            }
            changeFont();
        }

        // Italic
        private void italicToolStripButton_Click(object sender, EventArgs e)
        {
            if (italicToolStripButton.Checked)
            {
                this.italic = FontStyle.Italic;
            }
            else
            {
                this.italic = FontStyle.Regular;
            }
            changeFont();
        }

        // Underline
        private void underlineToolStripButton_Click(object sender, EventArgs e)
        {
            if (underlineToolStripButton.Checked)
            {
                this.underline = FontStyle.Underline;
            }
            else
            {
                this.underline = FontStyle.Regular;
            }
            changeFont();
        }

        // Font Size
        private void fontSizeToolStripComboBox_Leave(object sender, EventArgs e)
        {
            if (!float.TryParse(fontSizeToolStripComboBox.Text, out float f))
            {
                fontSizeToolStripComboBox.Text = fontSize.ToString();
            }
            else
            {
                changeFont();
            }
        }

        // Change Font
        private void changeFont()
        {
            editorRichTextBox.SelectionFont = new Font(
                    editorRichTextBox.SelectionFont.FontFamily,
                    float.Parse(fontSizeToolStripComboBox.Text),
                    bold | italic | underline);
        }
    }
}
