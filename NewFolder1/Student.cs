using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Tools;

namespace Курсовая.NewFolder1
{
    [Table("student")]
    public class Student : Base
    {
        [Column("name")]
        public string name { get; set; }
        [Column("curator")]
        public string curator { get; set; }
        [Column("surname")]
        public string surname { get; set; }
        [Column("student_id")]
        public int student_id { get; set; }
        [Column("group_id")]
        public int GroupId { get; set; }
    }
}
