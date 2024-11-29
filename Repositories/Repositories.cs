
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;

namespace tgBotOrderV11.Resos;

public class Resositories
{
    public OperationsRepos OperationsRepos { get; init; }
    public ReferalRepos ReferalRepos { get; init; }
    public UserRepos UserRepos { get; init; }
    public WalletRepos WalletRepos { get; init; }
    public Resositories(OperationsRepos operationsReposacklist, ReferalRepos referal, UserRepos userRepos, WalletRepos walletRepos)
    {
        OperationsRepos = operationsReposacklist;
        ReferalRepos = referal;
        UserRepos = userRepos;
        WalletRepos = walletRepos;
    }
}