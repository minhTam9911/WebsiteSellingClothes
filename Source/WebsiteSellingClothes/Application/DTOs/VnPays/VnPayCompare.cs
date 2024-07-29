using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.VnPays;
public class VnPayCompare : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if(x==y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var vnpayCompare = CompareInfo.GetCompareInfo("en-US");
        return vnpayCompare.Compare(x, y,CompareOptions.Ordinal);
    }
}
