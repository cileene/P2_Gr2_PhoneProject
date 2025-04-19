using System.Collections.Generic;

namespace GeneralUtils
{
    public static class OpenTrackerUtils
    {
        public static void Init(List<string> names, List<int> counts)
        {
            if (counts.Count != names.Count)
            {
                counts.Clear();
                for (int i = 0; i < names.Count; i++)
                    counts.Add(0);
            }
        }

        public static bool Register(List<string> names, List<int> counts, string itemName)
        {
            int idx = names.IndexOf(itemName);
            if (idx < 0) return false;
            counts[idx]++;
            return true;
        }

        public static int GetTimes(List<string> names, List<int> counts, string itemName)
        {
            int idx = names.IndexOf(itemName);
            return idx < 0 ? 0 : counts[idx];
        }

        public static bool HasBeenOpened(List<string> names, List<int> counts, string itemName)
        {
            return GetTimes(names, counts, itemName) > 0;
        }

        public static int GetOpenedCount(List<int> counts)
        {
            int total = 0;
            foreach (var c in counts)
                if (c > 0) total++;
            return total;
        }

        public static int GetTotalCount(List<string> names) => names.Count;
    }
}