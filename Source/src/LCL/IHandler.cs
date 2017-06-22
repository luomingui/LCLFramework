using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    public interface IHandler<in T>
    {
        void Handle(T message);
    }
}
