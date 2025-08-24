using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.Common
{
    public class Result
    {
        public bool Success { get; init; }
        public string[] Errors { get; init; } = Array.Empty<string>();
        public static Result Ok() => new() { Success = true };
        public static Result Fail(params string[] errors) => new() { Success = false, Errors = errors };
    }
    public class Result<T> : Result
    {
        public T? Value { get; init; }
        public static Result<T> Ok(T value) => new() { Success = true, Value = value };
        public new static Result<T> Fail(params string[] errors) => new() { Success = false, Errors = errors };
    }

}
