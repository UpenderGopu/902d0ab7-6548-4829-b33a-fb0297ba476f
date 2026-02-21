// ============================================================================
// Program.cs - Entry point for the LIS (Longest Increasing Subsequence) app
// ============================================================================

using LIS;

// Prompt the user to enter a sequence of integers
Console.WriteLine("Enter a sequence of integers separated by spaces:");

// Read the user input from the console (nullable string since user could press Enter without typing)
string? input = Console.ReadLine();

// Validate that the input is not null, empty, or whitespace
if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("Please provide a sequence of integers separated by spaces.");
    return; // Exit the program early if input is invalid
}

// Call the SubsequenceFinder to compute the longest increasing subsequence
string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

// Display the result to the user
Console.WriteLine($"Longest Increasing Subsequence: {result}");
