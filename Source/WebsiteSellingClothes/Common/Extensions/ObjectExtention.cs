﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions;
public static class ObjectExtention
{
    public static string ToQueryString(this object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
                         where p.GetValue(obj, null) != null
                         select p.Name + "=" + WebUtility.UrlEncode(p.GetValue(obj, null)!.ToString());
        var queryString = string.Join("&", properties.ToArray());
        return queryString;
    } 
}
