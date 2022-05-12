using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Tools;

namespace Курсовая.NewFolder1
{
        [Table("group")]
        public class Group : Base
        {
            [Column("title")]
            public string Title { get; set; }
            [Column("year")]
            public int Year { get; set; }

        [Column("curator_id")]
        public int curator_id { get; set; }
    }
}
