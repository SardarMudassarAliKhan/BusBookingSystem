using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    class PushService
    {
        public static bool Send()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
