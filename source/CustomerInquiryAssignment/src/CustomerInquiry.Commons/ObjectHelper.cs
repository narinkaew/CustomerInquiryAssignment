using System;
using System.Reflection;

namespace CustomerInquiry.Commons
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Check all object properties is not null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns>true if all least 1 property has value</returns>
        public static bool IsValidRequest<T>(T req)
        {
            bool isValid = false;

            try
            {
                isValid = !IsAllNullOrEmpty(req);
            }
            catch (Exception)
            {
                isValid = false;
            }

            return isValid;
        }

        private static bool IsAllNullOrEmpty<T>(T obj)
        {
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                object value = pi.GetValue(obj);
                if (value != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
