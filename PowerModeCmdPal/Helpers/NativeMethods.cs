using System;
using System.Runtime.InteropServices;

namespace PowerModeCmdPal.Helpers;

public static class NativeMethods
{
    [DllImport("kernel32.dll")]
    internal static extern IntPtr LocalFree(IntPtr hMem);

    [DllImport("powrprof.dll")]
    internal static extern uint PowerEnumerate(
        IntPtr rootPowerKey,
        IntPtr schemeGuid,
        IntPtr subGroupOfPowerSettingGuid,
        uint accessFlags,
        uint index,
        ref Guid buffer,
        ref uint bufferSize);

    [DllImport("powrprof.dll")]
    internal static extern uint PowerReadFriendlyName(
        IntPtr rootPowerKey,
        ref Guid schemeGuid,
        IntPtr subGroupOfPowerSettingGuid,
        IntPtr powerSettingGuid,
        IntPtr buffer,
        ref uint bufferSize);

    [DllImport("powrprof.dll")]
    internal static extern uint PowerReadDescription(
        IntPtr rootPowerKey,
        ref Guid schemeGuid,
        IntPtr subGroupOfPowerSettingGuid,
        IntPtr powerSettingGuid,
        IntPtr buffer,
        ref uint bufferSize);

    [DllImport("powrprof.dll")]
    internal static extern uint PowerGetActiveScheme(
        IntPtr userRootPowerKey,
        out IntPtr activePolicyGuid);

    [DllImport("powrprof.dll")]
    internal static extern uint PowerSetActiveScheme(
        IntPtr userRootPowerKey,
        ref Guid schemeGuid);
}
public enum AccessCodes : uint
{
    Scheme = 16,
}