﻿using System;
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

        public string Server { get; set; }
        public string User { get; set; }
        public string DB { get; set; }

        public CommandVM TestConnection { get; set; }
        public CommandVM SaveSettings { get; set; }

        public SettingsVM(PasswordBox passwordBox)
        {
            this.passwordBox = passwordBox;

            //помогите почему он красный!!!

            Server = Properties.Settings.Default.server;
            User = Properties.Settings.Default.user;
            DB = Properties.Settings.Default.db;
            passwordBox.Password = Properties.Settings.Default.pass;

            TestConnection = new CommandVM(() => {
                var db = MySqlDB.GetDB();
                db.InitConnection(Server, User, DB, passwordBox.Password);
                if (db.OpenConnection())
                {
                    db.CloseConnection();
                    System.Windows.MessageBox.Show("Соединение успешно!");
                }
            });

            SaveSettings = new CommandVM(() => {
                Properties.Settings.Default.user = User;
                Properties.Settings.Default.db = DB;
                Properties.Settings.Default.pass = passwordBox.Password;
                Properties.Settings.Default.server = Server;
                Properties.Settings.Default.Save();
                System.Windows.MessageBox.Show("Данные сохранены!");
            });
        }
    }
}
