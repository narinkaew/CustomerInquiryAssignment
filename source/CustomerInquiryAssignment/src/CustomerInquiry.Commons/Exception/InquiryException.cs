using System;

namespace CustomerInquiry.Commons
{
    public class InquiryException : Exception
    {
        public InquiryException()
        {
        }

        public InquiryException(string message)
            : base(message)
        {
        }

        public InquiryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
