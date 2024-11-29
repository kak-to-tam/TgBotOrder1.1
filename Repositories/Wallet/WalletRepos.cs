
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Abstract;

namespace tgBotOrderV11.Repos;

public class WalletRepos : IRepos<WalletModel>
{
    TgBotOrderContext tgBotOrderContext;
    public WalletRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<WalletModel?> CreateModel(long tgID, string address, float balance = 0)
    {
        WalletModel walletModel = new WalletModel(tgID, address, balance);
        return walletModel;
    }
    public async Task<List<WalletModel>?> GetAllModel()
    {
        List<WalletModel> walletModels = new List<WalletModel>();
        return walletModels;
    }
    public async Task<WalletModel?> GetModel(long tgID)
    {
        try
        {
            WalletModel walletModel;

            await tgBotOrderContext.Wallets.LoadAsync();
            
            Wallet wallet = tgBotOrderContext.Wallets.Where(needful => needful.UserId == tgID).First();

            walletModel = new WalletModel(wallet.UserId, wallet.Adress, wallet.Balance);    

            var context = new ValidationContext(walletModel);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(walletModel, context, results, true))
            {
                Console.WriteLine("Не удалось создать объект User");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                return walletModel;
        }
        catch(Exception ex)
        {
            return null;
        }

        return null;
    }
    public async Task<WalletModel?> Add(WalletModel walletModels)
    {
        return walletModels;
    }
    public async Task<WalletModel?> Update(WalletModel walletModels)
    {
        return walletModels;
    }
}