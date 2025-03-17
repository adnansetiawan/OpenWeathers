using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeather.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        private string _code;
        public string Code
        {
            get
            {
                return _code;
            }
            private set
            {
                _code = value;
            }
        }
        public UnauthorizedException(string code, string message) : base(message)
        {
            _code = code;
        }
    }
    public class BadRequestException : Exception
    {
        private string _code;
        public string Code
        {
            get
            {
                return _code;
            }
            private set
            {
                _code = value;
            }
        }
        public BadRequestException(string code, string message) : base(message) 
        {
            _code = code;
        }
    }
    public class DataNotFoundException : Exception
    {
        private string _code;
        public string Code
        {
            get
            { 
                return _code;
            }
            private set
            {
                _code = value;
            }
        }
        public DataNotFoundException(string code, string message) : base(message)
        {
            _code = code;
        }
    }
    public class InternalServerErrorRequestException : Exception
    {
        public InternalServerErrorRequestException(string message) : base(message)
        {
        }
    }
}
