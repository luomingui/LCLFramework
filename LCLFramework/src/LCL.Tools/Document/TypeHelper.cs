using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Document
{
    public class TypeHelper
    {
        private static readonly List<Type> typemap;
        static TypeHelper()
		{
            typemap = new List<Type>();
			typemap.Add(typeof(string));
			typemap.Add(typeof(int));
			typemap.Add(typeof(bool));
			typemap.Add(typeof(DateTime));
            typemap.Add(typeof(Nullable<DateTime>));
			typemap.Add(typeof(double));
			typemap.Add(typeof(long));
			typemap.Add(typeof(short));
			typemap.Add(typeof(byte));
			typemap.Add(typeof(char));
			typemap.Add(typeof(decimal));
			typemap.Add(typeof(float));
			typemap.Add(typeof(uint));
			typemap.Add(typeof(ulong));
			typemap.Add(typeof(ushort));
			typemap.Add(typeof(sbyte));
			typemap.Add(typeof(Guid));
			typemap.Add(typeof(byte[]));
		}

        public static bool IsExist(Type type)
        {
            foreach (var item in typemap)
            {
                if (item == type)
                    return true;
            }
            return false;
        }
    }
}
