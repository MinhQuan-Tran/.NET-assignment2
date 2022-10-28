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
    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();

            // Bind UserTypeComboBox with UserType enum 
            UserTypeComboBox.DataSource = Enum.GetValues(typeof(UserType));
            UserTypeComboBox.SelectedIndex = (int) UserType.View;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // get data from fields
            String username = UsernameTextBox.Text.Trim();
            String password = PasswordTextBox.Text;
            String fname = FnameTextBox.Text.Trim();
            String lname = LnameTextBox.Text.Trim();
            DateTime dob = DOBDateTimePicker.Value;
            UserType userType = (UserType) UserTypeComboBox.SelectedIndex;

            // check if user exists
            List<string> lines = File.ReadAllLines("login.txt").ToList();

            Boolean userFound = false;
            foreach (string line in lines)
            {
                // compare username (first item in line)
                if (line.Split(',')[0].Equals(username))
                {
                    userFound = true;
                    break;
                }
            }

            // if user found, show message and stop this function
            if (userFound)
            {
                MessageBox.Show("Username is already taken", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // validate fields
            if (fieldIsInvalid(username, "Username") || fieldIsInvalid(password, "Password") || fieldIsInvalid(fname, "First Name") || fieldIsInvalid(lname, "Last Name"))
            {
                return;
            }

            File.AppendAllText("login.txt", $"{username},{password},{Enum.GetName(userType)},{fname},{lname},{dob.ToString("dd-mm-yyyy")}\n");

            MessageBox.Show("New User added successfully!", "Add New User", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        // check if field contains ',' or is it empty (after Trim()) 
        private Boolean fieldIsInvalid(String field, String messageItem)
        {
            // if not valid, show message box
            if ((field.Contains(',') || field.Length == 0))
            {
                MessageBox.Show($"Invalid {messageItem}! Choose another {messageItem}.", $"Invalid {messageItem}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
        }

        private void ReEnterPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void CheckPassword()
        {
            if (PasswordTextBox.Text.Length == 0 || ReEnterPasswordTextBox.Text.Length == 0)
            {
                ErrorLabel.Visible = false;
                SubmitButton.Enabled = false;
            }
            else
            {
                if (!PasswordTextBox.Text.Equals(ReEnterPasswordTextBox.Text))
                {
                    ErrorLabel.Text = "Password not match";
                    ErrorLabel.ForeColor = Color.Red;
                    ErrorLabel.Visible = true;
                    SubmitButton.Enabled = false;
                }
                else
                {
                    ErrorLabel.Text = "Password match";
                    ErrorLabel.ForeColor = Color.Green;
                    ErrorLabel.Visible = true;
                    SubmitButton.Enabled = true;
                }
            }
        }
    }
}
