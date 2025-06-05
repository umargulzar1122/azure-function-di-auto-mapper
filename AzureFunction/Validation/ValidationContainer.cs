using HellowWord.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellowWord.Validation
{
    public class ValidationContainer : IValidationContainer
    {
        public MessageTypeEnum MessageType;


        public ValidationContainer()
        {
            IsSuccess = true;
        }

        public bool IsError => !IsSuccess;

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        //public MessageTypeEnum MessageType { get; set; }

        public void AddMessage(MessageTypeEnum type, string message)
        {
            MessageType = type;
            Message = message;

            switch (type)
            {
                case MessageTypeEnum.Error:
                    IsSuccess = false;
                    break;


            }
        }
    }
}
