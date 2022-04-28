using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Tools
{
        internal class ColumnAttribute : Attribute
        {
            public string Column { get; }
            public ColumnAttribute(string column)
            {
                Column = column;
            }
        }
}
