// Copyright(c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions.Toolkit;
using PowerModeCmdPal.Commands;
using PowerModeCmdPal.Helpers;
using System;
using System.Linq;

namespace PowerModeCmdPal.Items;

internal sealed partial class FallbackPowerModeCommandItem : FallbackCommandItem
{
    private readonly NoOpCommand _emptyCommand = new();
    private readonly PowerModeManager? _powerModeManager;

    public FallbackPowerModeCommandItem(PowerModeManager? powerModeManager)
        : base(new NoOpCommand(), "Set power mode", "jestermeijin.cmdpal.powermode.fallback")
    {
        Title = string.Empty;
        Subtitle = string.Empty;
        Icon = Icons.PowerModeIcon;
        Command = _emptyCommand;

        _powerModeManager = powerModeManager;
    }

    public override void UpdateQuery(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || _powerModeManager is null)
        {
            Title = string.Empty;
            Subtitle = string.Empty;
            Command = _emptyCommand;
            return;
        }

        var powerModes = _powerModeManager.PowerModeListItems.Where(w => !string.IsNullOrWhiteSpace(w.Title));
        var queryPowerMode = ListHelpers.FilterList(powerModes, query, (s, i) => ListHelpers.ScoreListItem(s, i)).FirstOrDefault();

        if (queryPowerMode is not null && !string.IsNullOrWhiteSpace(queryPowerMode.Title))
        {
            Title = $"Set power mode to \"{queryPowerMode.Title}\"";
            Subtitle = queryPowerMode.Subtitle ?? string.Empty;
            Icon = Icons.PowerModeIcon;
            Command = new SetActivePowerModeCommand(queryPowerMode.PowerModeGuid, true);
        }
        else
        {
            Title = string.Empty;
            Subtitle = string.Empty;
            Command = _emptyCommand;
        }
    }
}