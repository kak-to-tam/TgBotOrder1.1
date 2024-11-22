using tgBotOrder_v11.Resositories.Model;
using tgBotOrder_v11.DbBot;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using tgBotOrder_v11.Resositories.Abstract;

namespace tgBotOrder_v11.Resositories.Repos;

public class UserRepos : IRepos<UserModel>
{
    private TgBotOrderContext tgBotOrderContext;
    public UserRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
        
    }
    public async Task<List<UserModel>?> GetAllModel()
    {
        try
        {
            UserModel userModel;

            List<UserModel>? users = new List<UserModel>();

            await tgBotOrderContext.Users.LoadAsync();

            foreach(User user in tgBotOrderContext.Users)
            {
                userModel = new UserModel(user.TgId, user.Email); 

                var context = new ValidationContext(userModel);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(userModel, context, results, true))
                {
                    Console.WriteLine("Не удалось создать объект User");
                    foreach (var error in results)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                else
                    users.Add(userModel);
            }

            return users;
        }
        catch (Exception ex)
        {
            return null;
        }

        return null;
    }

    public async Task<UserModel?> GetModel(int tgID)
    {
        try
        {
            UserModel userModel;

            await tgBotOrderContext.Users.LoadAsync();
            
            User user = tgBotOrderContext.Users.Where(needful => needful.TgId == tgID).First();

            userModel = new UserModel(user.TgId, user.Email);    

            var context = new ValidationContext(userModel);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(userModel, context, results, true))
            {
                Console.WriteLine("Не удалось создать объект User");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                return userModel;
        }
        catch(Exception ex)
        {
            return null;
        }

        return null;
    }
    public async Task<UserModel?> CreateUserModel(int tgID, string email)
    {
        try
        {
            UserModel userModel = new UserModel(tgID, email);

            var context = new ValidationContext(userModel);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(userModel, context, results, true))
            {
                Console.WriteLine("Не удалось создать объект User");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                return userModel;   
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return null;
    }

    public async Task<UserModel?> Add(UserModel userModel)
    {
        try
        {
            User user = new User();
            user.TgId = userModel.TgID;
            user.Email = userModel.Email;

            tgBotOrderContext.Users.Add(user);
            
            await tgBotOrderContext.SaveChangesAsync();
            return userModel;
        }
        catch (Exception ex)
        {
            return null;
        }
        return null;
    }
    public async Task<UserModel?> Update(UserModel userModel)
    {   
        try 
        {
            UserModel userModelOld = await GetModel(userModel.TgID);

            if (userModel.Email != userModelOld.Email)
            {
                User user = await tgBotOrderContext.Users.Where(needful => needful.TgId == userModel.TgID).FirstAsync();
                user.Email = userModel.Email;
                
                tgBotOrderContext.SaveChanges();
                
                return userModel;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return null; 
    }
}