using System;
using System.Collections.Generic;

namespace Regular_Expression_Matching
{
  class Program
  {
    static void Main(string[] args)
    {
      string str = "aab";
      string p = "c*a*b";
      Solution s = new Solution();
      var answer = s.IsMatch(str, p);
      Console.WriteLine(answer);
    }
  }

  // https://www.youtube.com/watch?v=HAA8mgxlov8
  public class Solution
  {
    public bool IsMatch(string s, string p)
    {
      // i is to iterate string s and j is to iterate string p
      // s = aa and p = a, here j would be out of bound and we can return false
      // s = aac p = a*b*c
      // s = a p = a*b* , here i would be out ofbound but b* can be seen as empty as * can be used to have 0 or more b, and we choose empty
      bool Dfs(int i, int j)
      {
        var cache = new Dictionary<(int, int), bool>();
        var key = (i, j);
        if (cache.ContainsKey(key)) return cache[key];
        // if both i and j are out of bound return true - s = aac p = a*b*c
        if (i >= s.Length && j >= p.Length) return true;
        // if j only out of bound return false - s = aa and p = a
        if (j >= p.Length) return false;

        // if control came here which means i might not be out bound, so check first that
        // first we check the both the char of s and p are same OR in p we have period char
        bool match = i < s.Length && (s[i] == p[j] || p[j] == '.');
        if (j + 1 < p.Length && p[j + 1] == '*')
        {
          cache[key] = Dfs(i, j + 2)
              || (match && Dfs(i + 1, j)); // when we want to take *, for this the previous char before star in p and char in s should match
          return cache[key];
        }

        // when char is matched or its period in p, we need to increment both the index
        if (match)
        {
          cache[key] = Dfs(i + 1, j + 1);
          return cache[key];
        }

        cache[key] = false;
        return false;
      }

      return Dfs(0, 0);
    }
  }
}
