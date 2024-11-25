
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;

namespace tgBotOrderV11.Resos;

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