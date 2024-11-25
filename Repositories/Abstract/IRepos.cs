namespace tgBotOrder_v11.Resositories.Abstract;

public interface IRepos<T> where T : Abstract.IModel
{
    public Task<T?> Add(T model);
    public Task<T?> Update(T model);
    public Task<T?> GetModel(long tgID);
    public Task<List<T>?> GetAllModel();
}