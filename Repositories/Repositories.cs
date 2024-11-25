
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories.Repos;

namespace tgBotOrder_v11.Resositories;

public class Resositories
{
    public BlacklistRepos BlacklistRep { get; init; }
    public ReferalRepos ReferalRepos { get; init; }
    public UserRepos UserRepos { get; init; }
    public WalletRepos WalletRepos { get; init; }
    public Resositories(BlacklistRepos blacklist, ReferalRepos referal, UserRepos userRepos, WalletRepos walletRepos)
    {
        BlacklistRep = blacklist;
        ReferalRepos = referal;
        UserRepos = userRepos;
        WalletRepos = walletRepos;
    }
}