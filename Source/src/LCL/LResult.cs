using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections;

namespace LCL
{
    [DataContract, Serializable]
    [DebuggerDisplay("Success:{Success}, StatusCode:{StatusCode}, Message:{Message}")]
    public class LResult
    {
        public static string SuccessMessage = "操作成功！";

        public static string FailedMessage = "操作失败！";

        #region fields

        private bool _success;

        private int _statusCode;

        private string _message;

        #endregion

        #region Constructors
        public LResult(bool success)
        {
            _success = success;
            _statusCode = 0;
            _message = success ? SuccessMessage : FailedMessage;
            Attributes = new Hashtable();
        }
        public LResult(string message)
        {
            _success = false;
            _statusCode = 0;
            _message = message;
            Attributes = new Hashtable();
        }
        public LResult(int statusCode)
        {
            _success = false;
            _statusCode = statusCode;
            _message = FailedMessage;
            Attributes = new Hashtable();
        }
        public LResult(bool success, string message)
        {
            _success = success;
            _statusCode = 0;
            _message = message;
            Attributes = new Hashtable();
        }
        public LResult(bool success, int statusCode)
        {
            _success = success;
            _statusCode = statusCode;
            _message = success ? SuccessMessage : FailedMessage;
            Attributes = new Hashtable();
        }
        public LResult(int statusCode, string message)
        {
            _success = false;
            _statusCode = statusCode;
            _message = message;
            Attributes = new Hashtable();
        }
        public LResult(bool success, int statusCode, string message)
        {
            _success = success;
            _statusCode = statusCode;
            _message = message;
            Attributes = new Hashtable();
        }

        #endregion
        [DataMember]
        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }
        [DataMember]
        public int StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }
        [DataMember]
        public string Message
        {
            get
            {
                if (_message == null) { return string.Empty; }
                return _message;
            }
            set
            {
                if (value == null) value = string.Empty;
                _message = value;
            }
        }
        public object Data { get; set; }
        public Hashtable Attributes { get; set; }
        public void Reset()
        {
            _success = false;
            _statusCode = 0;
            _message = string.Empty;
        }
        public bool StatusEquals(LResult another)
        {
            return _success == another.Success && _statusCode == another.StatusCode;
        }

        #region implicit operators

        public static implicit operator LResult(bool value)
        {
            return new LResult(value);
        }

        public static implicit operator LResult(int statusCode)
        {
            return new LResult(statusCode);
        }

        public static implicit operator LResult(Enum statusCode)
        {
            return new LResult(Convert.ToInt32(statusCode));
        }

        public static implicit operator LResult(string error)
        {
            return new LResult(error);
        }

        public static implicit operator bool(LResult res)
        {
            return res.Success;
        }

        #endregion
    }
}