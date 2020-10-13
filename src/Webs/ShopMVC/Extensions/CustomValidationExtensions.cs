namespace ShopMVC.Extensions
{
    public static class CustomValidationExtensions
    {
        public static bool ContainsUpper(this string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsDigit(this string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
