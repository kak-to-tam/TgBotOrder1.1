using System;
using System.Collections.Generic;

namespace tgBotOrder_v11.DbBot;

public partial class Blacklist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
