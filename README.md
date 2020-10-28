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

 4. Set based - Negation '^'
  - Problem: Find all occurrences of characters that are NOT (a or b)
  - Pattern [^ab]
  - Text: this is a big test

  - needs to be first character inside the set
   + Pattern: [a^b] => indicates a set with members(a, b, ^) and will match a literal ^
   + Text: this is a ^ big test
 5. Range of characters
  - Problem: Find all occurrences of (a, b, c, d)
  - Pattern: [a-d] (is equal to [abcd])
  - Text: this is a definitive test 

 6. Multiple range of characters
  - Problem: find all occurrences of (a, b, c, d, x, y, z, 0, 1, 2, 3)
  - Pattern: [a-dx-z0-3]
  - Text: x-ray 3 won't work for this test

  - Negate the whole range with ^
  - Problem: Find all occurrences of characters not in (a, b, c, d, x, y, z, 0, 1, 2, 3)
  - Pattern: [^a-dx-z0-3]

 7. Whild card character
  - Dot or full stop character matches every character except new line \n
 