using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
