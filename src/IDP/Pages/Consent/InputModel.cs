// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace FW.IDP.Pages.Consent;

public sealed class InputModel
{
    public string? Button { get; set; }

    public IEnumerable<string> ScopesConsented { get; set; } = new List<string>();

    public bool RememberConsent { get; set; } = false;

    public string? ReturnUrl { get; set; }
}