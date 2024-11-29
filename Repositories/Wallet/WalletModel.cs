using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos.Abstract;


namespace tgBotOrderV11.Repos;

public class WalletModel : Abstract.IModel
{
    [Required]
    public long TgID { get; private set; }
    [Required]
    public string Address { get; private set; }
    [Required]
    public string PrivateKey { get; private set; }
    [Required]
    public float Balance { get; private set; }
    
    public WalletModel(long tgID, string address, string privateKey, float balance)
    {
        TgID = tgID;
        Address = address;
        PrivateKey = privateKey;
        Balance = balance;
    } 
}