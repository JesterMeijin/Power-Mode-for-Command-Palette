// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions.Toolkit;
using System;

namespace PowerModeCmdPal.Commands;

internal sealed partial class OpenPowerModeSettingsCommand : InvokableCommand
{
    public override CommandResult Invoke()
    {
        _ = Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:powersleep"));
        return CommandResult.Hide();
    }
}