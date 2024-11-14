namespace App.Core.Interfaces.Services
{
    public interface IGenericSvc<T> where T : class
    {
        void Create(T m);
        void Delete(T m);
        void Update(T m);
        List<T>? Read(string text);
        T? Read(string code, bool notUsed);
        T? Read(int id);
        List<T>? Read();
    }
}
