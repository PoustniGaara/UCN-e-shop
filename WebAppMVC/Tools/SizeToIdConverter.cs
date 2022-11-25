using System;
using System.Collections.Generic;

namespace WebAppMVC.Tools
{
	public static class SizeToIdConverter
	{

        public static int ConvertSizeToId(string size)
        {
            int sizeId;

            Dictionary<size, sizeId> sizes = new Dictionary<size, sizeId>();
            sizes.Add("XS", 0);
            sizes.Add("S", 1);
            sizes.Add("M", 2);
            sizes.Add("L", 3);
            sizes.Add("XL", 4);
            sizes.Add("O/S", 5);
            sizes.Add("500 ml", 6);

            foreach size in sizes
                return sizeId;
        }

        
    }
}

