using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConfig.Db.Flags
{
    [Flags]
    public enum UserAccessRights
    {
        ViewName = 1,
        ViewValue = 2,
        AddNew = 4,
        Edit = 8,
        Delete = 16,
        ProjectLevel = 32
    }
}
