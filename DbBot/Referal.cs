using System;
using System.Collections.Generic;

namespace tgBotOrder_v11.DbBot;

public partial class Referal
{
    public int Id { get; set; }

    public int? Father1Id { get; set; }

    public int? Father2Id { get; set; }

    public int? Father3Id { get; set; }

    public int UserId { get; set; }

    public virtual User? Father1 { get; set; }

    public virtual User? Father2 { get; set; }

    public virtual User? Father3 { get; set; }

    public virtual User User { get; set; } = null!;
}
