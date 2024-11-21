using System;
using System.Collections.Generic;

namespace tgBotOrder_v11.DbBot;

public partial class Wallet
{
    public int Id { get; set; }

    public string Adress { get; set; } = null!;

    public float Balance { get; set; }

    public int UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
