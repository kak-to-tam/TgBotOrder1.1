using System;
using System.Collections.Generic;

namespace tgBotOrderV11.DbBot;

public partial class Wallet
{
    public int Id { get; set; }

    public string Adress { get; set; } = null!;

    public float Balance { get; set; }

    public long UserId { get; set; }

    public string? PrivateKey { get; set; }
}
