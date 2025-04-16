namespace GeneralUtils
{
    public static class Gibberishifier
    {
        private static System.Random CreateDeterministicRng(string input)
        {
            int hash = input.GetHashCode();
            return new System.Random(hash);
        }

        public static string ToGibberish(string input)
        {
            var rng = CreateDeterministicRng(input);
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
                    result[i] = RandomCharSameCase(c, rng);
                else
                    result[i] = c;
            }

            return new string(result);
        }

        private static char RandomCharSameCase(char c, System.Random rng)
        {
            bool isUpper = char.IsUpper(c);
            char baseChar = isUpper ? 'A' : 'a';
            return (char)(baseChar + rng.Next(0, 26));
        }
    }
}