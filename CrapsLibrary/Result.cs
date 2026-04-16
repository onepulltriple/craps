using System.Diagnostics.CodeAnalysis;

namespace CrapsLibrary
{
    public class Result<T>
    {
        [MemberNotNullWhen(true, nameof(Value))]
        public bool Success { get; }

        public T? Value { get; } // nullable, since a failed result does not have a value

        public IReadOnlyList<string> Messages { get; }

        public Result(bool success, T? value, List<string> messages)
        {
            // did it work?
            this.Success = success;

            // the instance of type T created upon success, OR a value of a type T, e.g. Bet newBet or int 53
            this.Value = value; 

            // what happened?
            this.Messages = messages ?? new List<string>(); 
        }

        /// <summary>
        /// "Here is your T if it worked (plus what happened)."
        /// </summary>
        /// <param name="value"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static Result<T> Pass(T value, params string[] messages)
        {
            return new Result<T>(
                true,
                value,
                messages?.ToList() ?? new List<string>()
                );
        }

        /// <summary>
        /// "Here's what went wrong if it failed."
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
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
