namespace CrapsLibrary
{
    internal class Result<T>
    {
        public bool success { get; }

        public T? value { get; } // nullable, since a failed result does not have a value

        public IReadOnlyList<string> messages { get; }

        public Result(bool success, T? value, List<string> messages)
        {
            this.success = success;
            this.value = value;
            this.messages = messages ?? new List<string>();
        }

        public static Result<T> Pass(T value, params string[] messages)
        {
            return new Result<T>(
                true,
                value,
                messages?.ToList() ?? new List<string>()
                );
        }

        public static Result<T> Fail(params string[] messages)
        {
            return new Result<T>(
                false,
                default, // null is allowed
                messages?.ToList() ?? new List<string>()
                );
        }
    }
}
