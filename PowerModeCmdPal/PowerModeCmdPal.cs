// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace PowerModeCmdPal;

[Guid("47691d48-7642-4ad2-9682-f02d460bf671")]
public sealed partial class PowerModeCmdPal : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;

    private readonly PowerModeCmdPalCommandsProvider _provider = new();

    public PowerModeCmdPal(ManualResetEvent extensionDisposedEvent)
    {
        this._extensionDisposedEvent = extensionDisposedEvent;
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => this._extensionDisposedEvent.Set();
}
