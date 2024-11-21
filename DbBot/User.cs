using System;
using System.Collections.Generic;

namespace tgBotOrder_v11.DbBot;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public int TgId { get; set; }

    public int? WalletId { get; set; }

    public virtual ICollection<Blacklist> Blacklists { get; set; } = new List<Blacklist>();

    public virtual ICollection<Referal> ReferalFather1s { get; set; } = new List<Referal>();

    public virtual ICollection<Referal> ReferalFather2s { get; set; } = new List<Referal>();

    public virtual ICollection<Referal> ReferalFather3s { get; set; } = new List<Referal>();

    public virtual Referal? ReferalUser { get; set; }

    public virtual Wallet? Wallet { get; set; }

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
