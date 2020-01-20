using GetConfig.Db.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConfig.Db.ViewModels
{
    public class ConfigViewModel
    {
        public List<ConfigValueViewModel> ConfigValues { get; set; }
        public ProjectSettingsViewModel ProjectSettings { get; set; }

        public UserAccessRights AccessRights { get; set; }
    }
}
