using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories.Abstract;


namespace tgBotOrder_v11.Resositories.Model;

public class WalletModel : Abstract.IModel
{
    [Required]
    public int TgID { get; private set; }
    [Required]
    public string Address { get; private set; }
    [Required]
    public float Balance { get; private set; }
    
    public WalletModel(int tgID, string address, float balance)
    {
        TgID = tgID;
        Address = address;
        Balance = balance;
    } 
}