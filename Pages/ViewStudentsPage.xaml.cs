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
using Курсовая.View;
using Курсовая.NewFolder1;

namespace Курсовая.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewStudents.xaml
    /// </summary>
    public partial class ViewStudentsPage : Page
    {
        public ViewStudentsPage(Group selectedGroup)
        {
            InitializeComponent();
            DataContext = new ViewStudentsVM(selectedGroup);
        }
    }
}
