namespace Academic_Blog_App.Services.Helper
{
    public interface IResultHelper<T>
    {
        bool IsSuccess { get; }
        T Data { get; }
        string ErrorMessage { get; }
    }
}
