namespace FreeCMS.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToPersianString(this bool boolValue)
        {
            if(boolValue)
             return "بله";
            return "خیر";
        }
    }
}