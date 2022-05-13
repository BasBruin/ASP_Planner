using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalMSSQL
{
    public class TemporaryExceptionDAL : Exception
    {
        public TemporaryExceptionDAL()
        {
        }

        public TemporaryExceptionDAL(string? message) : base(message)
        {
        }

        public TemporaryExceptionDAL(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TemporaryExceptionDAL(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
