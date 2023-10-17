/* 
 
YOU ARE NOT ALLOWED TO MODIFY ANY FUNCTION DEFINATION's PROVIDED.
WRITE YOUR CODE IN THE RESPECTIVE QUESTION FUNCTION BLOCK


*/

using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            //Question2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "69";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1 };
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        /*
        
        Question 1:
        You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range. A number x is considered missing if x is in the range [lower, upper] and x is not in nums. Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is included in any of the ranges, and each missing number is covered by one of the ranges.
        Example 1:
        Input: nums = [0,1,3,50,75], lower = 0, upper = 99
        Output: [[2,2],[4,49],[51,74],[76,99]]  
        Explanation: The ranges are:
        [2,2]
        [4,49]
        [51,74]
        [76,99]
        Example 2:
        Input: nums = [-1], lower = -1, upper = -1
        Output: []
        Explanation: There are no missing ranges since there are no missing numbers.

        Constraints:
        -109 <= lower <= upper <= 109
        0 <= nums.length <= 100
        lower <= nums[i] <= upper
        All the values of nums are unique.

        Time complexity: O(n), space complexity:O(1)
        */

        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            try
            {
                //initialize result list and sublists of result list
                List<IList<int>> result = new List<IList<int>>();
                List<int> sublist = new List<int>();

                // a streak is a run from the beginning of missing numbers to the end, e.g. from 4 to 49
                bool streak = false;
                int streakBegin = -99;
                int streakEnd = 99;
                // loop through all numbers between the upper and lower range
                for (int i = lower; i <= upper; i++)
                {
                    //if a number is not contained in nums
                    if (!nums.Contains(i))
                    {
                        //either it is the beginning of a streak
                        if (!streak)
                        {
                            streakBegin = i;
                            streak= true;
                            streakEnd = i;
                        }

                        //or the continuation of an existing streak
                        if (streak)
                        {
                            streakEnd= i;
                        }
                    }
                    // if a number is contained in nums
                    else
                    {
                        // either do nothing, or if there is an existing streak   then end it
                        if (streak)
                        {
                            streak = false;
                            sublist.Add(streakBegin);
                            sublist.Add(streakEnd);
                            result.Add(new List<int>(sublist));
                            if (i != upper)
                            {
                               sublist.Clear();
                            } 
                        }
                    }
                    //if the loop reached the upper boundary and there still is an ongoing streak, then end it
                    if ((i==upper) && streak)
                    {
                        sublist.Add(streakBegin);
                        sublist.Add(streakEnd);
                        result.Add(new List<int>(sublist));
                        sublist.Clear();
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /*
         
        Question 2

        Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.An input string is valid if:
        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Every close bracket has a corresponding open bracket of the same type.
 
        Example 1:

        Input: s = "()"
        Output: true
        Example 2:

        Input: s = "()[]{}"
        Output: true
        Example 3:

        Input: s = "(]"
        Output: false

        Constraints:

        1 <= s.length <= 104
        s consists of parentheses only '()[]{}'.

        Time complexity:O(n^2), space complexity:O(1)
        */

        public static bool IsValid(string s)
        {
            try
            {
                // List of brackets that still have to be closed
                List<string> toDo = new List<string>();
                // loop through each character of the input string
                for(int i=0; i<s.Length; i++) {
                    // if the character is an opening bracket, add the corresponding closing bracket to the toDo list
                    if (s[i] == '(')
                    {
                        toDo.Add(")");
                    }
                    else if (s[i] == '[')
                    {
                        toDo.Add("]");
                    }
                    else if (s[i] == '{')
                    {
                        toDo.Add("}");
                    }
                    //if the character is a closing bracket
                    else
                    {
                        //if there are brackets to close in the todo list
                        if (toDo.Count > 0)
                        {
                            string inputChar = s[i].ToString();
                            //look at the last closing bracket that has been added
                            if (inputChar != toDo[toDo.Count - 1])
                            {
                                return false;
                            }
                            //remove closed bracket from ToDo List
                            toDo.RemoveAt(toDo.Count - 1);
                        }
                        //when there was no opening bracket, return false
                        else
                        {
                            return false;
                        }
                    }
                }
                // if there are still brackets left at the end that need to be closed
                if(toDo.Count > 0)
                {
                    return false;
                }
                // in all other cases, everything went as planned, return true
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 3:
        You are given an array prices where prices[i] is the price of a given stock on the ith day.You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
        Example 1:
        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.

        Example 2:
        Input: prices = [7,6,4,3,1]
        Output: 0
        Explanation: In this case, no transactions are done and the max profit = 0.
 
        Constraints:
        1 <= prices.length <= 105
        0 <= prices[i] <= 104

        Time complexity: O(n), space complexity:O(1)
        */

        public static int MaxProfit(int[] prices)
        {
            try
            {
                // make a list of all possible profits
                List<int> posProfit = new List<int>();
                //loop through all numbers in prices
                for(int i=0; i<prices.Length-1; i++)
                {
                    //loop through all numbers that come after the above number in prices
                    for(int j=i+1; j<prices.Length; j++)
                    {
                        // if profit would be positive
                        if (prices[j] > prices[i])
                        {
                            // add profit to list of possible profits
                            posProfit.Add(prices[j] - prices[i]);
                        }                       
                    }
                }
                //if list of possible profits is empty
                if (!posProfit.Any())
                {
                    return 0;
                } 
                // return maximum profit
                return posProfit.Max();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        
        Question 4:
        Given a string num which represents an integer, return true if num is a strobogrammatic number.A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).
        Example 1:

        Input: num = "69"
        Output: true
        Example 2:

        Input: num = "88"
        Output: true
        Example 3:

        Input: num = "962"
        Output: false

        Constraints:
        1 <= num.length <= 50
        num consists of only digits.
        num does not contain any leading zeros except for zero itself.

        Time complexity:O(n), space complexity:O(1)
        */

        public static bool IsStrobogrammatic(string s)
        {
            try
            {
                //only if only symmetric numbers are included in the string, it is worth checking it for symmetricity
                if (s.IndexOfAny("123457".ToCharArray()) == -1)
                {
                    //if the length of the number is odd and the middle number is a 6 or 9, it is also not symmetric
                    HashSet<char> noMiddleNums = new HashSet<char> {'6','9'}; 
                    if ((s.Length%2!=0) && (noMiddleNums.Contains(s[(s.Length / 2)]))){
                        return false;
                    }

                    //loop through the string until it's middle and always check if the current number has it's counterpart after the middle
                    for(int i=0; i<(s.Length/2); i++)
                    {
                        //for a 0 on the left part of the string there has to be a 0 at the right part of the string
                        if (s[i] == 0)
                        {
                            if (s[s.Length - 1 - i] != 0)
                            {
                                return false;
                            }
                        //for a 6 on the left part of the string there has to be a 9 at the right part of the string
                        }
                        if (s[i] == 6)
                        {
                            if (s[s.Length - 1 - i] != 9)
                            {
                                return false;
                            }
                        }
                        //for an 8 on the left part of the string there has to be another 8 at the right part of the string
                        if (s[i] == 8)
                        {
                            if (s[s.Length - 1 - i] != 8)
                            {
                                return false;
                            }
                        }
                        //for a 9 on the left part of the string there has to be a 6 at the right part of the string
                        if (s[i] == 9)
                        {
                            if (s[s.Length - 1 - i] != 6)
                            {
                                return false;
                            }
                        }
                    }
                    //if looping through the string without errors was successful, return true
                    return true;
                }
                //if unsymmetric numbers are included, return false
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 5:
        Given an array of integers nums, return the number of good pairs.A pair (i, j) is called good if nums[i] == nums[j] and i < j. 

        Example 1:

        Input: nums = [1,2,3,1,1,3]
        Output: 4
        Explanation: There are 4 good pairs (0,3), (0,4), (3,4), (2,5) 0-indexed.
        Example 2:

        Input: nums = [1,1,1,1]
        Output: 6
        Explanation: Each pair in the array are good.
        Example 3:

        Input: nums = [1,2,3]
        Output: 0

        Constraints:

        1 <= nums.length <= 100
        1 <= nums[i] <= 100

        Time complexity:O(n), space complexity:O(n)

        */

        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                int pairs = 0;
                //loop through all numbers in nums
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    //loop through all numbers that come after the above number in nums
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        // if the numbers are the same, increase amount of pairs
                        if (nums[j] == nums[i])
                        {
                            pairs += 1;
                        }
                    }
                }

                // return number of good pairs
                return pairs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        Question 6

        Given an integer array nums, return the third distinct maximum number in this array. If the third maximum does not exist, return the maximum number.

        Example 1:

        Input: nums = [3,2,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2.
        The third distinct maximum is 1.
        Example 2:

        Input: nums = [1,2]
        Output: 2
        Explanation:
        The first distinct maximum is 2.
        The second distinct maximum is 1.
        The third distinct maximum does not exist, so the maximum (2) is returned instead.
        Example 3:

        Input: nums = [2,2,3,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2 (both 2's are counted together since they have the same value).
        The third distinct maximum is 1.
        Constraints:

        1 <= nums.length <= 104
        -231 <= nums[i] <= 231 - 1

        Time complexity:O(nlogn), space complexity:O(n)
        */

        public static int ThirdMax(int[] nums)
        {
            try
            {
                //Convert the array into a List of unique values only
                List<int> dist_nums = nums.Distinct().ToList();
                //If the list is long enough to take the 3rd maximum, remove the 1st and 2nd maximum
                if(dist_nums.Count >= 3)
                {
                    dist_nums.Remove(dist_nums.Max());
                    dist_nums.Remove(dist_nums.Max());
                   
                }
                //return the (remaining) maximum
                return dist_nums.Max();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        
        Question 7:

        You are playing a Flip Game with your friend. You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].
        Example 1:
        Input: currentState = "++++"
        Output: ["--++","+--+","++--"]
        Example 2:

        Input: currentState = "+"
        Output: []
 
        Constraints:
        1 <= currentState.length <= 500
        currentState[i] is either '+' or '-'.

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                //create an empty List of all possible outcomes
                List<string> poss_outcome = new List<string>() { };
                // loop through 'currentState' except for the last item, because we always look at the current and the next char
                for(int i=0; i<currentState.Length-1; i++)
                {
                    //if we have two consecutive + signs
                    if((currentState[i] == '+') && (currentState[i+1]== '+'))
                    {
                        // Build the possible outcome by replacing the + signs with - signs
                        StringBuilder outcome = new StringBuilder (currentState);
                        outcome[i] = '-';
                        outcome[i+1] = '-';
                        // Add the possible outcome to the list of possible outcomes
                        poss_outcome.Add(outcome.ToString());
                    }
                }
                return poss_outcome;  
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 8:

        Given a string s, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.
        Example 1:

        Input: s = "leetcodeisacommunityforcoders"
        Output: "ltcdscmmntyfrcdrs"

        Example 2:

        Input: s = "aeiou"
        Output: ""

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static string RemoveVowels(string s)
        {
            // define vowels
            string vowel = "aeiou";
            // build a result string that equals the input string s
            StringBuilder result = new StringBuilder (s);
            // loop through each char of the result string
            for(int i = 0; i < result.Length; i++)
            {
                // if the current char is a vowel
                if (vowel.Contains(result[i]))
                {
                    // then replace the vowel with an empty space
                    result.Replace(result[i].ToString(), "");
                }
            }
            // format the output into a string and return it
            string output = result.ToString();
            return output;
        }

        /* Inbuilt Functions - Don't Change the below functions */
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }


        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }
    }
}
