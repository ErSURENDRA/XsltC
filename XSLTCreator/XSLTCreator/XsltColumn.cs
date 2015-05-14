using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSLTCreator
{
    class XsltColumn:IComparable
    {

            string _value;

            public string Value { get { return _value; } set { _value = value; } }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                XsltColumn other = obj as XsltColumn;
                if (other == null) return false;
                return other._value == this._value;
            }

            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }


            public int CompareTo(object obj)
            {
                 return string.Compare(this._value,(obj as XsltColumn)._value);
            }
    }
}
