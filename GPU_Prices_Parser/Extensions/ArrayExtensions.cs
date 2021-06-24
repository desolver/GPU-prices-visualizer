namespace GPU_Prices_Parser.Extensions
{
    public static class ArrayExtensions
    {
        public static int IndexOf<T>(this T[] array, T element)
            where T : class
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == element)
                    return i;

            return -1;
        }
    }
}