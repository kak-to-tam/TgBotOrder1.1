using System;
using System.Collections.Generic;

namespace tgBotOrderV11.DbBot;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public long TgId { get; set; }
}
