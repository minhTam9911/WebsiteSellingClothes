using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers;
public class DateTimeHelper
{
    public static DateTime ConvertTimeStampToDateTime(long timeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddSeconds(timeStamp);
    }
}
