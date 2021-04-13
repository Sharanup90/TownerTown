﻿using TownerTown.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TownerTown.Helpers
{
    public static class DeviceIdentofication
    {
        private static string[] mobileDevices = new string[] {"iphone","ppc",
                                                      "windows ce","blackberry",
                                                      "opera mini","mobile","palm",
                                                      "portable","opera mobi" };

        public static bool IsMobileDevice(string userAgent)
        {
            // TODO: null check
            userAgent = userAgent.ToLower();
            return mobileDevices.Any(x => userAgent.Contains(x));
        }

    }
}
