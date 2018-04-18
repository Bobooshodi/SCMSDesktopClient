namespace SCMSClient.Services.Interfaces
{
    public interface IAbstractService<Model>
    {
        Model Get(string parameter);

        Model Create(Model model);

        Model Update(Model model);

        Model Delete(string parameter);

        System.Collections.Generic.List<Model> GetAll();
    }
}