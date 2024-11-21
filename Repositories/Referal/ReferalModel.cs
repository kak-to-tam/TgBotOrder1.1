using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories.Abstract;


namespace tgBotOrder_v11.Resositories.Model;

public class ReferalModel : Abstract.IModel
{
    [Required]
    public int TgID { get; private set; }

    public int? Father1Id { get; private set; }
    public int? Father2Id { get; private set; }
    public int? Father3Id { get; private set; }
    
    public ReferalModel(int tgID, int? father1Id = null, int? father2Id = null, int? father3Id = null)
    {
        TgID = tgID;
        Father1Id = father1Id;
        Father2Id = father2Id;
        Father3Id = father3Id;
    } 
}