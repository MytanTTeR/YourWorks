using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourWorks.Models;

namespace YourWorks.Core.AchivementFactory
{
    interface IAchivementFactory
    {
        AchivementContext db { get; set; }
        AchivementCollection Collection { get; set; }

        string PreviewClass { get; }
        string ActionDetails { get; }
        string ActionEdit { get; }
        string ActionCreate { get; }
        string ActionDelete { get; }
        string Controller{ get; }

        IEnumerable<AbstractAchivement> GetAchivements();
        int GetCount();
    }
}
