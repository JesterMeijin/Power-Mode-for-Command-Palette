// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Commands;
using PowerModeCmdPal.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PowerModeCmdPal.Pages;

internal sealed partial class PowerModeCmdPalPage : ListPage
{
    private PowerModeManager? _powerModeManager;

    public PowerModeCmdPalPage(PowerModeManager? powerModeManager)
    {
        Icon = Icons.PowerModeIcon;
        Title = "Power Mode";
        Name = "Power Mode";

        _powerModeManager = powerModeManager;
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
        if (_powerModeManager is not null)
        {
            try
            {
                _powerModeManager.UpdatePowerModeListItems();
                if (_powerModeManager.PowerModeListItems.Count > 0)
                {
                    foreach (var powerModeListItem in _powerModeManager.PowerModeListItems)
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
            }
            catch (Win32Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to update power modes: {ex.Message}");
                itemList.Add(new ListItem(new NoOpCommand())
                {
                    Title = "Error loading power modes",
                    Subtitle = "Failed to query Windows power settings",
                    Icon = Icons.InfoIcon
                });
            }
        }
        else
        {
            itemList.Add(new ListItem(new NoOpCommand())
            {
                Title = "Power Mode extension not available",
                Subtitle = "Failed to initialize Windows power settings",
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
