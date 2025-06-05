using HellowWord.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellowWord.Validation
{
    public interface IValidationContainer
    {
        bool IsError { get; }

        bool IsSuccess { get; }

        string Message { get; set; }

        //MessageTypeEnum MessageType { get; set; }


        void AddMessage(MessageTypeEnum type, string message);
    }
}
