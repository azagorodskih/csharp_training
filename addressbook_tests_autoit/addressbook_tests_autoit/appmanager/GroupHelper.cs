using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public void Add(GroupData group)
        {

        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> g = new List<GroupData>();
            return g;
        }
    }
}