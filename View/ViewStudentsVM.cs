using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.NewFolder1;
using Курсовая.Model;

namespace Курсовая.View
{
    public class ViewStudentsVM : BaseVM
    {
        private Group selectedGroup;
        private List<Student> studentsBySelectedGroup;

        public List<Group> Groups { get; set; }
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                StudentsBySelectedGroup = SqlModel.GetInstance().SelectStudentsByGroup(selectedGroup);
                Signal();
            }
        }
        public List<Student> StudentsBySelectedGroup
        {
            get => studentsBySelectedGroup;
            set
            {
                studentsBySelectedGroup = value;
                Signal();
            }
        }
        public Student SelectedStudent { get; set; }

        public ViewStudentsVM(Group selectedGroup)
        {
            SqlModel sqlModel = SqlModel.GetInstance();
            Groups = sqlModel.SelectGroupsRange(0, 100);
            SelectedGroup = Groups.FirstOrDefault(s => s.ID == selectedGroup?.ID);
        }
    }
}
