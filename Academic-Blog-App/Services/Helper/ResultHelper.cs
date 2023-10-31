namespace Academic_Blog_App.Services.Helper
{
    public class ResultHelper<T> : IResultHelper<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }

        private ResultHelper(bool isSuccess, T data, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ResultHelper<T> Success(T data)
        {
            return new ResultHelper<T>(true, data, null);
        }

        public static ResultHelper<T> Success()
        {
            return new ResultHelper<T>(true, default(T), null);
        }

        public static ResultHelper<T> Fail(string errorMessage)
        {
            return new ResultHelper<T>(false, default(T), errorMessage);
        }
    }
}
