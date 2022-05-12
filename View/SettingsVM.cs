using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Курсовая.Tools;
using Курсовая.Model;

namespace Курсовая.View
{
    class SettingsVM
    {
        PasswordBox passwordBox;

        public string FIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public CommandVM SaveSettings { get; set; }

        public SettingsVM(PasswordBox passwordBox)
        {
            this.passwordBox = passwordBox;


            FIO = Properties.Settings.Default.server;
            Login = Properties.Settings.Default.user;
            Password = Properties.Settings.Default.db;
            passwordBox.Password = Properties.Settings.Default.pass;


            SaveSettings = new CommandVM(() => {
                Properties.Settings.Default.user = Login;
                Properties.Settings.Default.db = Password;
                Properties.Settings.Default.pass = passwordBox.Password;
                Properties.Settings.Default.server = FIO;
                Properties.Settings.Default.Save();
                System.Windows.MessageBox.Show("Вы успешно вошли!");
            });
        }
    }
}
