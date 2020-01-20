using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConfig.Db.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ValueCount { get; set; }

        public string Color { get; set;  }

        public string BackgroundClass
        {
            get
            {
                switch (Color.Trim())
                {
                    case "blue":
                        return "bg-primary";
                    case "green":
                        return "bg-success";
                    case "red":
                        return "bg-danger";
                    case "yellow":
                        return "bg-warning";
                    case "purple":
                        return "bg-info";
                    default:
                        return "bg-primary";
                }
            }
        }
    }
}
