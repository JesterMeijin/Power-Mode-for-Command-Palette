// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PowerModeCmdPal.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace PowerModeCmdPal.Helpers;

internal sealed class PowerModeManager
{
    private readonly List<PowerModeListItem> _powerModesListItems = new();
    
    public PowerModeManager()
    {
        UpdatePowerModeListItems();
    }

    public IReadOnlyList<PowerModeListItem> PowerModeListItems => _powerModesListItems;

    internal void UpdatePowerModeListItems()
    {
        var activeGuid = GetActivePowerModeGuid();
        var items = GetPowerModeGuidList()
            .Select(guid => new PowerModeListItem(guid, guid == activeGuid))
            .ToList();

        _powerModesListItems.Clear();
        _powerModesListItems.AddRange(items);
    }

    internal static uint SetActivePowerModeGuid(Guid powerModeGuid)
    {
        return NativeMethods.PowerSetActiveScheme(IntPtr.Zero, ref powerModeGuid);
    }

    internal static Guid GetActivePowerModeGuid()
    {
        if (NativeMethods.PowerGetActiveScheme(IntPtr.Zero, out var ptr) != 0)
            throw new Win32Exception();

        try
        {
            return Marshal.PtrToStructure<Guid>(ptr);
        }
        finally
        {
            if (ptr != IntPtr.Zero)
                NativeMethods.LocalFree(ptr);
        }
    }

    internal static IEnumerable<Guid> GetPowerModeGuidList()
    {
        Guid powerPlanGuid = Guid.Empty;
        uint index = 0;
        uint result;

        while (true)
        {
            // Reset size for each iteration to ensure we pass the correct buffer size
            uint size = (uint)Marshal.SizeOf<Guid>();
            result = NativeMethods.PowerEnumerate(
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero,
                (uint)AccessCodes.Scheme,
                index,
                ref powerPlanGuid,
                ref size);

            if (result != 0)
                break;

            yield return powerPlanGuid;
            index++;
        }

        // 259 = ERROR_NO_MORE_ITEMS
        if (result != 259)
            throw new Win32Exception((int)result);
    }
}