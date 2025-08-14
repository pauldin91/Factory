using Factory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Tests.Implementors
{
    internal class ConcreteImplB : IImplementor
    {
        public string GetMsg()
        {
            throw new NotImplementedException();
        }
    }
}
