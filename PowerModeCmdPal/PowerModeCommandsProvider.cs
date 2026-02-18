// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Commands;
using PowerModeCmdPal.Helpers;
using PowerModeCmdPal.Items;
using PowerModeCmdPal.Pages;
using System;
using System.ComponentModel;

namespace PowerModeCmdPal;

public partial class PowerModeCommandsProvider : CommandProvider
{
    private PowerModeManager? _powerModeManager;
    private readonly IFallbackCommandItem _fallbackCommandItem;
    private readonly ICommandItem _commandItem;

    public PowerModeCommandsProvider()
    {
        Id = "jestermeijin.cmdpal.powermode";
        DisplayName = "Power Mode"; // Name displayed in Extensions settings
        Icon = Icons.PowerModeIcon; // Icon displayed in Extensions settings

        try
        {
            _powerModeManager = new PowerModeManager();
        }
        catch (Win32Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to initialize PowerModeManager: {ex.Message}");
            _powerModeManager = null;
        }

        _fallbackCommandItem = new FallbackPowerModeCommandItem(_powerModeManager);
        _commandItem = new CommandItem(new PowerModePage(_powerModeManager))
        {
            MoreCommands = [
                new CommandContextItem(new OpenPowerModeSettingsCommand() { Name = "Open in Settings" }),
            ]
        };
    }

    public override ICommandItem[] TopLevelCommands() => [_commandItem];

    public override IFallbackCommandItem[] FallbackCommands() => [_fallbackCommandItem];
}
