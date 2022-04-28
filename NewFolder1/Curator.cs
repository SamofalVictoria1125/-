using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Tools;

namespace Курсовая.NewFolder1
{
    [Table("curator")]
        public class Curator : Base
        {
            [Column("firstName")]
            public string FirstName { get; set; }
            [Column("lastName")]
            public string LastName { get; set; }
        }
}
