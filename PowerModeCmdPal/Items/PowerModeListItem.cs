// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Commands;
using PowerModeCmdPal.Helpers;
using System;

namespace PowerModeCmdPal.Items;

internal sealed partial class PowerModeListItem : ListItem
{
    public PowerModeListItem(Guid powerModeGuid, bool isActivePowerMode) : base(new NoOpCommand())
    {
        Title = PowerModeHelper.ReadFriendlyName(powerModeGuid);
        Subtitle = PowerModeHelper.ReadFriendlyDescription(powerModeGuid);
        Icon = Icons.ActivePowerModeIcon(isActivePowerMode);

        SetActivePowerModeCommand = new SetActivePowerModeCommand(powerModeGuid, false);
        Command = SetActivePowerModeCommand;

        PowerModeGuid = powerModeGuid;
    }

    public SetActivePowerModeCommand SetActivePowerModeCommand { get; }

    public Guid PowerModeGuid { get; }
}
