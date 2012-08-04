

namespace Smoothie.Domain.Dto
{
    public class ActionConfirmation<T>
    {
        public bool WasSuccessful { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
