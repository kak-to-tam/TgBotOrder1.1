using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos.Abstract;


namespace tgBotOrderV11.Repos.Model;

public class BlacklistModel : Abstract.IModel
{
    [Required]
    public long TgID { get; private set; }
    
    public BlacklistModel(long tgID)
    {
        TgID = tgID;
        
    } 
}