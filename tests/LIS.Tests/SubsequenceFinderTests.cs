// ============================================================================
// SubsequenceFinderTests.cs - Unit tests for the SubsequenceFinder class.
// Tests cover all specification test cases (1-11) plus additional edge cases.
// Uses xUnit framework with [Fact] attributes for test discovery.
// ============================================================================

using LIS;
using Xunit;

namespace LIS.Tests;

/// <summary>
/// Unit tests for SubsequenceFinder covering all test cases from the specification.
/// Each test follows the Arrange-Act-Assert (AAA) pattern:
///   - Arrange: Set up input and expected output
///   - Act: Call FindLongestIncreasingSubsequence
///   - Assert: Verify the result matches the expected output
/// </summary>
public class SubsequenceFinderTests
{
    // ========================================================================
    // Specification Test Cases (1-11) - From the code-test.md document
    // These are the required test cases that must pass for the solution.
    // ========================================================================

    /// <summary>Test Case 1: Simple 5-element sequence with one clear LIS.</summary>
    [Fact]
    public void TestCase1_SimpleSequence()
    {
        // Input: "6 1 5 9 2" → The longest increasing run is "1 5 9" (3 elements)
        string input = "6 1 5 9 2";
        string expected = "1 5 9";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 2: Large sequence loaded from TestData (1000+ elements).</summary>
    [Fact]
    public void TestCase2_LargeSequence_ExpectedOutput()
    {
        string input = TestData.TestCase2Input;
        string expected = "1710 2461 9288 10195 10431 12485"; // LIS of length 6

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 3: Large sequence loaded from file (too large for inline).</summary>
    [Fact]
    public void TestCase3_LargeSequence_ExpectedOutput()
    {
        string input = TestData.TestCase3Input;
        string expected = "10298 10897 12291 15037 18446 23435 25333 27266"; // LIS of length 8

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 4: Medium sequence (64 elements).</summary>
    [Fact]
    public void TestCase4_MediumSequence()
    {
        string input = "923 11613 30483 19569 24201 13461 1189 30793 8848 16914 16053 21700 22116 3852 20909 5231 31469 3862 16353 22813 28735 4421 3618 32303 9932 31892 7823 22547 28888 11143 11695 3339 2094 11023 9661 27440 7186 24750 15427 24502 31606 23515 3563 29553 12145 22184 11409 28824 6636 10658 21404 5578 27807 14073 13967 31310 3132 4321 7643 1951 13289 24375 17912 11304";
        string expected = "3862 16353 22813 28735"; // LIS of length 4

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 5: Medium sequence (128 elements).</summary>
    [Fact]
    public void TestCase5_MediumSequence()
    {
        string input = "27892 18536 13491 11084 11970 24975 30922 11945 15113 27101 1974 31902 2623 21822 11720 30730 23635 27193 17527 19799 16794 30488 8953 28856 12300 25162 32016 20910 30896 6661 9255 26577 12629 10032 24221 31949 26243 26495 18785 22443 10673 13024 30655 11602 20408 28694 17785 31309 29576 23715 3866 10702 4378 3052 17543 11763 19622 24984 2519 27977 14869 2873 23140 10639 14521 15662 25122 17340 14140 14024 304 323 29654 20907 11693 13973 3267 8311 10189 31463 29941 24744 13356 18742 8454 17339 20578 12937 112 21395 5591 1399 5888 30234 16089 3816 19080 21547 491 22560 14549 10160 14176 1529 10720 13575 32041 15727 29256 29611 19692 12642 23040 10768 14422 15768 23365 206 16305 13058 19924 20738 30393 14656 21081 12785 27563 26982";
        string expected = "11084 11970 24975 30922"; // LIS of length 4

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 6: Medium sequence loaded from TestData (256 elements).</summary>
    [Fact]
    public void TestCase6_MediumSequence()
    {
        string input = TestData.TestCase6Input;
        string expected = "3808 3908 10386 19306"; // LIS of length 4

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 7: Large sequence loaded from TestData (512 elements).</summary>
    [Fact]
    public void TestCase7_LargeSequence()
    {
        string input = TestData.TestCase7Input;
        string expected = "125 1841 5882 18464 28317 31497"; // LIS of length 6

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 8: Large sequence loaded from TestData (1024 elements).</summary>
    [Fact]
    public void TestCase8_LargeSequence()
    {
        string input = TestData.TestCase8Input;
        string expected = "9139 17687 25106 26202 27592 30937"; // LIS of length 6

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Test Case 9: Large sequence loaded from file (too large for inline).</summary>
    [Fact]
    public void TestCase9_LargeSequence()
    {
        string input = TestData.TestCase9Input;
        string expected = "918 1089 5133 7725 18035 24605 26716 27095"; // LIS of length 8

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test Case 10: When two subsequences tie in length, the earliest one should be returned.
    /// Input "6 2 4 6 1 5 9 2" has two runs of length 3: "2 4 6" and "1 5 9".
    /// The earlier one ("2 4 6") should be returned.
    /// </summary>
    [Fact]
    public void TestCase10_TiedLength_ReturnsEarliest()
    {
        string input = "6 2 4 6 1 5 9 2";
        string expected = "2 4 6"; // First occurrence of a 3-element increasing run

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test Case 11: Another tie-breaking test. "6 2 4 3 1 5 9" has
    /// "2 4" (length 2) and "1 5 9" (length 3). The longest is "1 5 9".
    /// </summary>
    [Fact]
    public void TestCase11_TiedLength_ReturnsEarliest()
    {
        string input = "6 2 4 3 1 5 9";
        string expected = "1 5 9"; // Longest run is 3 elements

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    // ========================================================================
    // Edge Case Tests - Additional tests for boundary conditions
    // These go beyond the specification to ensure robustness.
    // ========================================================================

    /// <summary>A single element should be returned as-is (it's trivially increasing).</summary>
    [Fact]
    public void SingleElement_ReturnsSameElement()
    {
        string input = "42";
        string expected = "42";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>An empty string should return an empty string (no elements to process).</summary>
    [Fact]
    public void EmptyString_ReturnsEmpty()
    {
        string input = "";
        string expected = "";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>A fully descending sequence has no increasing run longer than 1.</summary>
    [Fact]
    public void AllDescending_ReturnsSingleElement()
    {
        // Every element is smaller than the previous, so the longest run is just the first element
        string input = "9 7 5 3 1";
        string expected = "9";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>A fully ascending sequence should return the entire sequence.</summary>
    [Fact]
    public void AllAscending_ReturnsEntireSequence()
    {
        // The entire sequence is strictly increasing, so it is the LIS
        string input = "1 3 5 7 9";
        string expected = "1 3 5 7 9";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>All equal elements have no strictly increasing pair, so return the first element.</summary>
    [Fact]
    public void AllEqual_ReturnsSingleElement()
    {
        // Equal elements are NOT strictly increasing, so each "run" is length 1
        string input = "5 5 5 5 5";
        string expected = "5";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Two increasing elements should return both.</summary>
    [Fact]
    public void TwoElements_Increasing()
    {
        string input = "1 2";
        string expected = "1 2";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }

    /// <summary>Two decreasing elements should return only the first (longest run is 1).</summary>
    [Fact]
    public void TwoElements_Decreasing()
    {
        string input = "2 1";
        string expected = "2";

        string result = SubsequenceFinder.FindLongestIncreasingSubsequence(input);

        Assert.Equal(expected, result);
    }
}
