using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories.Abstract;

namespace tgBotOrder_v11.Resositories.Model;

public class UserModel : Abstract.IModel
{
    [Required]
    [RegularExpression(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)")]
    public string Email { get; private set; }
    [Required]
    public int TgID { get; private set; }
    public UserModel(int tgID, string email)
    {
        TgID = tgID;
        Email = email;
    }
}