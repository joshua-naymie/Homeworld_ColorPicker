using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Exceptions
{
    public class InvalidRemasteredGameException : Exception
    {
        public InvalidRemasteredGameException()
        {
            ;
        }

        public InvalidRemasteredGameException(RemasteredGame game) : base(String.Format("Invalid Remastered game: {0}", game.ToString()))
        {
            ;
        }

        public InvalidRemasteredGameException(string message) : base(message)
        {
            ;
        }
        public InvalidRemasteredGameException(string message, Exception inner) : base(message, inner)
        {
            ;
        }
    }
}
