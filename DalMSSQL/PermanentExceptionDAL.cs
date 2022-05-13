using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalMSSQL
{
    public class PermanentExceptionDAL : Exception
    {
        public PermanentExceptionDAL()
        {
        }

        public PermanentExceptionDAL(string? message) : base(message)
        {
        }

        public PermanentExceptionDAL(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PermanentExceptionDAL(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
