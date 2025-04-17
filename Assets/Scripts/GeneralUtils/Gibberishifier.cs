namespace GeneralUtils
{
    public static class Gibberishifier
    {
        public static string ToGibberish(string input)
        {
            var result = new char[input.Length];
            bool inPlaceholder = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c == '$')
                {
                    result[i] = c;
                    continue;
                }

                if (c == '{') inPlaceholder = true;
                else if (c == '}') inPlaceholder = false;

                if (!inPlaceholder && char.IsLetter(c))
                    result[i] = ShiftCharTwoPositions(c);
                else
                    result[i] = c;
            }

            return new string(result);
        }

        private static char ShiftCharTwoPositions(char c)
        {
            if (char.IsUpper(c))
                return (char)('A' + (c - 'A' + 2) % 26);
            else
                return (char)('a' + (c - 'a' + 2) % 26);
        }
    }
}