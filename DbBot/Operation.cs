using System;
using System.Collections.Generic;

namespace tgBotOrderV11.DbBot;

public partial class Operation
{
    public int Id { get; set; }

    public long UserId { get; set; }

    public float? From { get; set; }

    public float? To { get; set; }
}
