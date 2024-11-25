using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories.Abstract;


namespace tgBotOrder_v11.Resositories.Model;

public class BlacklistModel : Abstract.IModel
{
    [Required]
    public long TgID { get; private set; }
    
    public BlacklistModel(long tgID)
    {
        TgID = tgID;
        
    } 
}