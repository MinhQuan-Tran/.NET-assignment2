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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // get data from fields
            String username = UsernameTextBox.Text.Trim();
            String password = PasswordTextBox.Text;

            // find user
            List<string> lines = File.ReadAllLines("login.txt").ToList();

            Boolean userFound = false;
            String userName = null;
            UserType userType = UserType.View;
            foreach (string line in lines)
            {
                // compare username (first item) and password (second item)
                if (line.Split(',')[0].Equals(username) && line.Split(',')[1].Equals(password))
                {
                    userFound = true;
                    userName = line.Split(',')[0];
                    userType = (UserType) Enum.Parse(typeof(UserType), line.Split(',')[2]);
                    break;
                }
            }

            // if not found, show error message and stop
            if (!userFound)
            {
                MessageBox.Show("Unknow Username or Incorrect Password", "Invalid User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MessageBox.Show("User found! Login Success", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // hide this window, show editor and wait for it to close (using ShowDialog() instead of Show()) before showing this window again
            Editor editor = new Editor("*blank", userName, userType);
            this.Hide();
            editor.ShowDialog();
            this.Show();
        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();

            this.Hide();
            newUser.ShowDialog();

            // wait for New User form to close
            this.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
