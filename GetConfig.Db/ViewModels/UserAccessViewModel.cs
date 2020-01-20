using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConfig.Db.ViewModels
{
    public class UserAccessViewModel
    {
        public int ProjectId { get; set; }
        public string UserEmail { get; set; }
        public int AccessFlag { get; set; }
    }
}
