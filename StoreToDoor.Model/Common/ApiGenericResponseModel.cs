using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model
{
    public class ApiGenericResponseModel<T>
    {
        public ApiGenericResponseModel()
        {
            ErrorMessage = new List<string>();
        }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
