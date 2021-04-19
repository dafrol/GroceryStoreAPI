using System;

namespace GroceryStoreAPI.Utils
{
    public static class Guard
    {
        public static void VerifyArgumentNotNull(object obj, string objName)
        {
            if (obj == null)
            {
                Exception ex = new ArgumentNullException(objName);
                throw (ex);
            }
        }

    }
}
