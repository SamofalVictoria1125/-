using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсовая.DB;
using Курсовая.Model;
using Курсовая.View;

namespace Курсовая
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (SqlModel.GetInstance().OpenConnection())
            {
                CuratorModel curatorModel = new CuratorModel();
                var curators = curatorModel.GetCuratorByFIO("п");
                SqlModel.GetInstance().CloseConnection();

                DataContext = new SettingsVM(passwordBox);
            }
        }
    }
}
