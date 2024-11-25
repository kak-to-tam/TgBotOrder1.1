using System;
using System.Collections.Generic;

namespace tgBotOrder_v11.DbBot;

public partial class Referal
{
    public int Id { get; set; }

    public long? Father1Id { get; set; }

    public long? Father2Id { get; set; }

    public long? Father3Id { get; set; }

    public long UserId { get; set; }
}
