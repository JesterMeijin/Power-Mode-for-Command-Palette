// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PowerModeCmdPal.Helpers;

internal static class PowerModeHelper
{
    internal static string ReadFriendlyName(Guid powerPlanGuid)
        => ReadPowerString(NativeMethods.PowerReadFriendlyName, powerPlanGuid);

    internal static string ReadFriendlyDescription(Guid powerPlanGuid)
        => ReadPowerString(NativeMethods.PowerReadDescription, powerPlanGuid);

    private static string ReadPowerString(PowerReadStringDelegate api, Guid powerPlanGuid)
    {
        uint size = 0;

        // First call to query buffer size
        uint sizeQueryResult = api(IntPtr.Zero, ref powerPlanGuid, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, ref size);
        if (sizeQueryResult != 0)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to query power string size: {sizeQueryResult}");
            return string.Empty;
        }

        if (size == 0)
            return string.Empty;

        IntPtr buffer = Marshal.AllocHGlobal((int)size);

        try
        {
            uint res = api(IntPtr.Zero, ref powerPlanGuid, IntPtr.Zero, IntPtr.Zero, buffer, ref size);
            if (res != 0)
                throw new Win32Exception((int)res);

            return Marshal.PtrToStringUni(buffer) ?? string.Empty;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private delegate uint PowerReadStringDelegate(
        IntPtr rootPowerKey,
        ref Guid powerPlanGuid,
        IntPtr subGroupOfPowerSettingGuid,
        IntPtr powerSettingGuid,
        IntPtr buffer,
        ref uint bufferSize);
}