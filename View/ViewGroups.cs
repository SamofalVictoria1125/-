﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.NewFolder1;
using Курсовая.Tools;
using Курсовая.Model;

namespace Курсовая.View
{

    //я знаю что тут делать, но пока другим занята
    class ViewGroupsVM : Base
    {
        private List<Group> groups;
        private List<int> pageIndexes;
        private int selectedIndex;
        private int viewRowsCount;

        public List<Group> Groups
        {
            get => groups;
            set
            {
                groups = value;
                Signal();
            }
        }
        public CommandVM ViewBack { get; set; }
        public CommandVM ViewForward { get; set; }
        public List<int> PageIndexes
        {
            get => pageIndexes;
            set
            {
                pageIndexes = value;
                Signal();
            }
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                Groups = SqlModel.GetInstance().SelectGroupsRange((selectedIndex - 1) * ViewRowsCount, ViewRowsCount);
                Signal();
            }
        }
        public int[] RowsCountVariants { get; set; }
        public int ViewRowsCount
        {
            get => viewRowsCount;
            set
            {
                viewRowsCount = value;
                InitPages();
                Signal();
            }
        }

        public ViewGroupsVM()
        {
            RowsCountVariants = new int[] { 2, 5, 10 };
            ViewRowsCount = 5;

            ViewBack = new CommandVM(() =>
            {
                if (SelectedIndex > 1)
                    SelectedIndex--;
            });

            ViewForward = new CommandVM(() =>
            {
                if (SelectedIndex < PageIndexes.Last())
                    SelectedIndex++;
            });
        }

        private void InitPages()
        {
            var sqlModel = SqlModel.GetInstance();
            int pageCount = (sqlModel.GetNumRows(typeof(Group)) / ViewRowsCount) + 1;
            PageIndexes = new List<int>(Enumerable.Range(1, pageCount));
            SelectedIndex = 1;
        }
    }
}