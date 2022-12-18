using System;
using System.Collections.Generic;

namespace WebAppMVC.Tools
{
	public static class SizeToIdConverter
	{
        private static readonly Dictionary<string, int> Sizes = new Dictionary<string, int> {
            { "S", 1 },
            { "M", 2 },
            { "L", 3 },
            { "XL", 4 },
            { "O/S", 5 },

        };

        public static int ConvertSizeToId(string size)
        {
            return Sizes[size];
        }  
    }
}

