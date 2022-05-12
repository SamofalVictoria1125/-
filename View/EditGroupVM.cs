using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Tools;
using Курсовая.Pages;
using Курсовая.NewFolder1;
using Курсовая.Model;

namespace Курсовая.View
{
    public class EditGroupVM : BaseVM
    {
        public Group EditGroup { get; set; }
        public CommandVM SaveGroup { get; set; }

        public List<Curator> Curators { get; set; }

        public Curator GroupCurator
        {
            get => groupCurator;
            set
            {
                groupCurator = value;
                Signal();
            }
        }

        private Curator groupCurator;

        private CurrentPageControl currentPageControl;
        public EditGroupVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditGroup = new Group();
            InitCommand();
        }
        public EditGroupVM(Group editGroup, CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditGroup = editGroup;
            InitCommand();
        }

        private void InitCommand()
        {
            EditGroup.curator_id = GroupCurator.ID;
            SaveGroup = new CommandVM(() => {
                var model = SqlModel.GetInstance();
                if (EditGroup.ID == 0)
                    model.Insert(EditGroup);
                else
                    model.Update(EditGroup);
                currentPageControl.Back();
            });
        }



    }
}
