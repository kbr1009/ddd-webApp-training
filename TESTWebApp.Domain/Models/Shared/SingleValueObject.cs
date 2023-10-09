
namespace TESTWebApp.Domain.Models.Shared
{
    public abstract class SingleValueObject<T>
    {
        public T Value { get; }
        protected SingleValueObject(T value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value), "値を設定してください。");
        }
    }
}
