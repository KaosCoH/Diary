namespace Diary.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrNullString(this string? stringRef)
        {
            if (String.IsNullOrEmpty(stringRef) || stringRef == "null")
            {
                return true;
            }

            return false;
        }
    }
}
