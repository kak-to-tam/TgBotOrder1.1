using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories.Abstract;


namespace tgBotOrder_v11.Resositories.Model;

public class ReferalModel : Abstract.IModel
{
    [Required]
    public long TgID { get; private set; }

    public long? Father1Id { get; private set; }
    public long? Father2Id { get; private set; }
    public long? Father3Id { get; private set; }
    
    public ReferalModel(long tgID, long? father1Id = null, long? father2Id = null, long? father3Id = null)
    {
        TgID = tgID;
        Father1Id = father1Id;
        Father2Id = father2Id;
        Father3Id = father3Id;
    } 
}