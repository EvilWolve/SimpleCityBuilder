namespace Save
{
    public interface ISaveService
    {
        void Save<T>(string key, T data) where T : new();
        T Load<T>(string key) where T : new();
    }
}
