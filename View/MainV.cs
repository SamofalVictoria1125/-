using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Курсовая.Tools;
using Курсовая.Pages;

namespace Курсовая.View
{
    class MainV : BaseVM
    {

        CurrentPageControl currentPageControl;

        public Page CurrentPage
        {
            get => currentPageControl.Page;
        }

        public CommandVM ViewGroups { get; set; }

        public MainV()
        {
            currentPageControl = new CurrentPageControl();
            currentPageControl.PageChanged += CurrentPageControl_PageChanged;

            ViewGroups = new CommandVM(() => {
                currentPageControl.SetPage(new ViewGroupsPage());
            });
        }

        private void CurrentPageControl_PageChanged(object sender, EventArgs e)
        {
            Signal(nameof(CurrentPage));
        }

    }
}
