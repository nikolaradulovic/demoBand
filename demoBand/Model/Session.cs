using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.Model
{
    public class Session
    {
        private Dictionary<string, Object> map;

        private Session()
        {
            map = new Dictionary<string, object>();
        }

        private static Session instance;

        public static Session GetInstance()
        {
            if (instance == null)
                instance = new Session();
            return instance;
        }

        public Object getValueAt(string key)
        {
            Object obj;
            bool result =  map.TryGetValue(key, out obj);
            if (result)
                return obj;
            else
                return null;
        }

        public void insertValue(string key, Object value)
        {
            Object outObject;
            bool exist = map.TryGetValue(key, out outObject);
            if (exist)
            {
                map.Remove(key);
            }


            map.Add(key, value);
        }

        public void reset()
        {
            map = new Dictionary<string, object>();
        }

    }

    public enum Choice
    {
        solo,
        collaborator
    }


}
