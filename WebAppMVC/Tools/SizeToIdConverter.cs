using System;
using System.Collections.Generic;

namespace WebAppMVC.Tools
{
	public static class SizeToIdConverter
	{
        private static readonly Dictionary<string, int> Sizes = new Dictionary<string, int> {
            { "XS", 1 },
            { "S", 2 },
            { "M", 3 },
            { "L", 4 },
            { "XL", 5 },
            { "XXL", 6 },
            { "O/S", 7 },
            { "500 ML", 8 },
            { "750 ML", 9 },
            { "1 L", 10 },
        };

        public static int ConvertSizeToId(string size)
        {
            return Sizes[size];
        }  
    }
}

