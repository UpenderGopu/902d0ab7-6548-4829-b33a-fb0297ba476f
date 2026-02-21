namespace LIS;

/// <summary>
/// Provides methods to find the longest increasing subsequence
/// from a whitespace-separated string of integers.
/// The subsequence must consist of consecutive (contiguous) elements
/// from the input that are strictly increasing.
/// </summary>
public static class SubsequenceFinder
{
    /// <summary>
    /// Finds the longest contiguous strictly increasing subsequence from the input string.
    /// If multiple subsequences share the longest length, the earliest one is returned.
    /// </summary>
    /// <param name="input">A string of integers separated by single whitespace.</param>
    /// <returns>The longest increasing subsequence as a whitespace-separated string.</returns>
    public static string FindLongestIncreasingSubsequence(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        int[] numbers = ParseInput(input);

        if (numbers.Length == 0)
        {
            return string.Empty;
        }

        if (numbers.Length == 1)
        {
            return numbers[0].ToString();
        }

        var (startIndex, length) = FindLongestContiguousIncreasingRun(numbers);

        int[] result = new int[length];
        Array.Copy(numbers, startIndex, result, 0, length);

        return string.Join(" ", result);
    }

    /// <summary>
    /// Parses the whitespace-separated input string into an array of integers.
    /// </summary>
    private static int[] ParseInput(string input)
    {
        string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int[] result = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            result[i] = int.Parse(parts[i]);
        }

        return result;
    }

    /// <summary>
    /// Finds the longest contiguous increasing run in the array.
    /// Returns the start index and length of the earliest longest run.
    /// A run is a sequence of consecutive elements where each is strictly
    /// greater than the previous.
    /// </summary>
    private static (int StartIndex, int Length) FindLongestContiguousIncreasingRun(int[] nums)
    {
        int bestStart = 0;
        int bestLength = 1;

        int currentStart = 0;
        int currentLength = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[i - 1])
            {
                // Extend the current run.
                currentLength++;
            }
            else
            {
                // Current run is broken. Check if it's the best so far.
                if (currentLength > bestLength)
                {
                    bestStart = currentStart;
                    bestLength = currentLength;
                }

                // Start a new run from the current element.
                currentStart = i;
                currentLength = 1;
            }
        }

        // Check the last run.
        if (currentLength > bestLength)
        {
            bestStart = currentStart;
            bestLength = currentLength;
        }

        return (bestStart, bestLength);
    }
}
