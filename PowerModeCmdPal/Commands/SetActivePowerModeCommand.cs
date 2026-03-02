// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Helpers;
using System;

namespace PowerModeCmdPal.Commands;

internal sealed partial class SetActivePowerModeCommand(Guid powerModeGuid, bool isFallback) : InvokableCommand
{
    public event EventHandler? PowerModeUpdated;

    public override CommandResult Invoke()
    {
        if (powerModeGuid == Guid.Empty)
            return CommandResult.ShowToast("Failed to set power mode: invalid power mode.");

        uint result = PowerModeManager.SetActivePowerModeGuid(powerModeGuid);
        if (result != 0)
            return CommandResult.ShowToast($"Failed to set power mode (error {result}). This device may not support switching power plans.");

        if (isFallback)
            return CommandResult.ShowToast("Successfully set to \"" + PowerModeHelper.ReadFriendlyName(powerModeGuid) + "\"");

        PowerModeUpdated?.Invoke(this, EventArgs.Empty);
        return CommandResult.KeepOpen();
    }
}