using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos.Abstract;


namespace tgBotOrderV11.Repos.Model;

public class OperationsModel : Abstract.IModel
{
    [Required]
    public long TgID { get; private set; }
    public float? From { get; private set; } 
    public float? To { get; private set; } 
    public OperationsModel(long tgID, float? from = null, float? to = null)
    {
        TgID = tgID;
        From = from;
        To = to;
    } 
}