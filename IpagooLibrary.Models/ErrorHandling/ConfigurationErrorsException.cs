using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IpagooLibrary.Models.ErrorHandling
{
    [Serializable]
    public class ConfigurationErrorsException : Exception
    {
        public ConfigurationErrorsException()
        {
        }

        public ConfigurationErrorsException(string message) : base(message)
        {
        }

        public ConfigurationErrorsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
