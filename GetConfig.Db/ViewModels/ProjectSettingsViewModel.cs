using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConfig.Db.ViewModels
{
    public class ProjectSettingsViewModel : ProjectViewModel
    {
        public List<UserAccessViewModel> UserAccesses { get; set; }
    }
}
