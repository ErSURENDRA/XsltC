using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSLTCreator
{
    class CacheHandler
    {
        List<XsltColumn> listofcolumns;

        internal List<XsltColumn> Listofcolumns
        {
            get { return listofcolumns; }
            set { listofcolumns = value; }
        }

        Dictionary<String, String> filecolumns;

        public Dictionary<String, String> FileColumns
        {
            get { return filecolumns; }
            set { filecolumns = value; }
        }


        static CacheHandler obj;

        private CacheHandler()
        {
            listofcolumns = new List<XsltColumn>();
            // FileColumns = new Dictionary<string, string>();
        }

        public static CacheHandler GetInstance()
        {
            if (obj == null)
            {
                obj = new CacheHandler();
            }
            return obj;

        }

    }



}
