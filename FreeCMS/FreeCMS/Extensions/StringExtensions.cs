namespace FreeCMS.Extensions
{
    public static class StringExtensions
    {
        // convert -> this is sample text
        //max length 7 
        //to this -> this is 
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        // convert -> this is sample text
        //max length 7 
        //endwith ...
        //to this -> this is ... 
        public static string Truncate(this string value, int maxLength, string endWith)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + " " + endWith;
        }
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("Your boolean value is null.");
            if (value != "True" && value != "False") throw new ArgumentException("Your boolean value is not valid.");
            if (value == "True") return true;
            return false;
        }
    }
}
