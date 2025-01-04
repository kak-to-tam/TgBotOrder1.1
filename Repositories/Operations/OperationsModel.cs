using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos.Abstract;


namespace tgBotOrderV11.Repos.Model;

public class OperationsModel : Abstract.IModel
{
    [Required]
    public long TgID { get; private set; }
    public int? OpT { get; private set; }
    public decimal Amount { get; private set; }
    public string? To { get; private set; } 
    public OperationsModel(long tgID, decimal Amount, int? OpT = null, string? to = null)
    {
        TgID = tgID;
        this.OpT = OpT;
        To = to;
    } 
}