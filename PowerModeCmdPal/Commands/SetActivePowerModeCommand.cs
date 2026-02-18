// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Helpers;
using System;
using System.ComponentModel;

namespace PowerModeCmdPal.Commands;

internal sealed partial class SetActivePowerModeCommand(Guid powerModeGuid, bool isFallback) : InvokableCommand
{
    public event EventHandler? PowerModeUpdated;

    public override CommandResult Invoke()
    {
        // Validate GUID before passing to Windows
        if (powerModeGuid == Guid.Empty)
        {
            throw new ArgumentException("Power mode GUID cannot be empty", nameof(powerModeGuid));
        }

        uint result = PowerModeManager.SetActivePowerModeGuid(powerModeGuid);
        if (result != 0)
            throw new Win32Exception((int)result);

        if (isFallback)
            return CommandResult.ShowToast("Successfully set to \"" + PowerModeHelper.ReadFriendlyName(powerModeGuid) + "\"");

        PowerModeUpdated?.Invoke(this, EventArgs.Empty);
        return CommandResult.KeepOpen();
    }
}