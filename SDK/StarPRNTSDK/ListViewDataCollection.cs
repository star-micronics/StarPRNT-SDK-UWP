using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarPRNTSDK
{
    public class ListViewDataCollection : System.Collections.Generic.List<ListViewItemData>
    {
        public ListViewDataCollection()
        {

        }
    }

    public class ListViewItemData
    {
        public string Item { get; set; }
    }
}
