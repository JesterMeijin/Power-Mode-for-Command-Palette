// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Helpers;
using PowerModeCmdPal.Commands;
using PowerModeCmdPal.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerModeCmdPal.Pages;

internal sealed partial class PowerModePage : ListPage
{
    private PowerModeManager _powerModeManager;

    public PowerModePage(PowerModeManager powerModeManager)
    {
        Icon = Icons.PowerModeIcon;
        Title = "Power Mode";
        Name = "Power Mode";

        _powerModeManager = powerModeManager ?? throw new ArgumentNullException(nameof(powerModeManager));
    }

    public override IListItem[] GetItems()
    {
        var itemList = new List<IListItem>();

        // Add an item to open Power Mode in Windows Settings
        itemList.Add(new ListItem
        {
            Title = "Open Power Mode in Settings",
            Subtitle = "Optimise your device based on power use and performance",
            Icon = Icons.SpeedIcon,
            Command = new OpenPowerModeSettingsCommand(),
        });

        // Add items for available Power Modes
        _powerModeManager.UpdatePowerModeListItems();
        if (_powerModeManager?.PowerModeListItems.Count > 0 )
        {
            foreach (var powerModeListItem in _powerModeManager?.PowerModeListItems)
            {
                powerModeListItem.SetActivePowerModeCommand.PowerModeUpdated -= HandlePowerModeUpdated;
                powerModeListItem.SetActivePowerModeCommand.PowerModeUpdated += HandlePowerModeUpdated;
                itemList.Add(powerModeListItem);
            }
        }
        else
        {
            itemList.Add(new ListItem(new NoOpCommand())
            {
                Title = "No power plan available",
                Icon = Icons.InfoIcon
            });
        }

        return [.. itemList];
    }

    private void HandlePowerModeUpdated(object? sender, EventArgs e)
    {
        RaiseItemsChanged();
    }
}
