using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Model
{
    public class SenderObject
    {

        private Dictionary<string, object> map;

        public SenderObject()
        {
            map = new Dictionary<string, object>();
        }

        public void putExtra(string key, object value)
        {
            map.Add(key, value);
        }

        public object getExtra(string key)
        {
            object o = null;
            map.TryGetValue(key, out o);
            return o;
        }


    }
}
