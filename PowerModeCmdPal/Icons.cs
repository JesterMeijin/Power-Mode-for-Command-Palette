// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions.Toolkit;

namespace PowerModeCmdPal;

internal static class Icons
{
    internal static IconInfo PowerModeIcon { get; } = IconHelpers.FromRelativePath("Assets\\SmallTile.png");
    internal static IconInfo InfoIcon { get; } = new IconInfo("\uE946");
    internal static IconInfo SpeedIcon { get; } = new IconInfo("\uEC48");
    internal static IconInfo RadioBtnOnIcon { get; } = new IconInfo("\uECCB");
    internal static IconInfo RadioBtnOffIcon { get; } = new IconInfo("\uECCA");
    internal static IconInfo ActivePowerModeIcon(bool isActivePowerMode)
    => isActivePowerMode ? RadioBtnOnIcon : RadioBtnOffIcon;
}