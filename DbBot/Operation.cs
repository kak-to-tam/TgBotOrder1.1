using System;
using System.Collections.Generic;

namespace tgBotOrderV11.DbBot;

public partial class Operation
{
    public int Id { get; set; }

    public long UserId { get; set; }
    public long TgId{ get; set; }

    public decimal Amount { get; set; }

    public int OpT { get; set; }

    public string To { get; set; }
}
