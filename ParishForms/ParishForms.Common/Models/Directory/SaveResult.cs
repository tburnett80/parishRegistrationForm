
namespace ParishForms.Common.Models.Directory
{
    public enum ResultType
    {
        Unset = 0,
        ValidationFailed,
        Exception,
        Success
    }

    public sealed class SaveResult
    {
        public ResultType Type { get; set; }

        public int RowsAffected { get; set; }

        public string Message { get; set; }
    }
}
