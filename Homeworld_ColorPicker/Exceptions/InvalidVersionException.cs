using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Exceptions
{
    public class InvalidVersionException : Exception
    {
        public InvalidVersionException()
        {
            ;
        }

        public InvalidVersionException(HomeworldVersion version) : base(String.Format("Invalid Homeworld version: {0}", version.ToString()))
        {
            ;
        }

        public InvalidVersionException(string message) : base(message)
        {
            ;
        }
        public InvalidVersionException(string message, Exception inner) : base(message, inner)
        {
            ;
        }
    }
}
