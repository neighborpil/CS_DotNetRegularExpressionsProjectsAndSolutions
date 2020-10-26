# CS_DotNetRegularExpressionsProjectsAndSolutions
book example


# .Net RegularExpression Quick Reference
https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/regular-expression-language-quick-reference

# Example codes
https://docs.microsoft.com/ko-kr/dotnet/standard/base-types/character-classes-in-regular-expressions

# unicode 8.0
https://www.unicode.org/versions/Unicode8.0.0/

# Case sensitive
 - Regular expression is basically case sensitive.
   I should turn it on when I need case insensitive comparison
   Option1: Specify as part of pattern inline directive(?i)A
   Option2: Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase

# Single Characters
 1. Single Character "OR" condition
  - Problem: Find all occurrences of letter 'a' or 'b'
  - Pattern: a|b
  - Text:  this is a big text
 
 2. String literal
  - Problem: Find all occurrences of string 'ab'
  - Pattern: ab
  - Text: this is absolute test
 
 3. Set based - Square brackeets [set membership]
  - Problem: Find all occurrences of a or b
  - Pattern: [ab]
  - Text: this is a big test