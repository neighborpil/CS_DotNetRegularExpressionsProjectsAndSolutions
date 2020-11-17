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

 7. Whild card character .  <= Dot!
  - Dot or full stop character matches every character except new line \n
  - Dot may have a performance issue. Use carefullly.
 
 8. Escape with \
  - Problem: Find all occurrences of '.'(dot)
  - Pattern: \.
  - Text: This. Is a Test.

 9. Control Characters(tab, newline, carriage return and so forth)
  - Problem: Find all occurrences of tab
  - Pattern: \t
  - Text: One		.Two		
 
 # Anchors
  1. Anchors are special syntex used for specifying.
   - Start of string or line
   - End of string or line
   - Word boundary
   - And so forth...
  2. Search for text
   - Problem: Find all occurrences of word 'log'
   - Pattern: log
   - Text: catalog of log

  3. Word boundary
   - Pattern: \blog\b => is instruction to match only on word boundary
   - Text: catalog of log

  4. Start of string or line ^
   - Problem: Find occurrences of 'apple' at beginning of string or line
   - Pattern: ^apple
   - Text: apple Grows on apple tree
  
  5. ^ - Multi-line Text
   - Pattern: ^apple
   - Text:
    apple 1 grows on apple tree
    apple 2 grows on apple tree
   - Internal String: "apple 1 grows on apple tree\r\napple 2 grows on apple tree\r\n"
    + Windows uses \r\n to represent new line
    => need to turn-on multi-line mode to interprete embedded lines
   
  6. ^ - Turn on multi-line mode (?m)
   - Problem: Find occurrences of 'apple' at beginning of string or line
   - Pattern: (?m)^apple
   - Text: 
    apple 1 grows on apple tree
    apple 2 grows on apple tree

  7. End of string or line $(matches end of string or \n)
   - Problem: Find occurrences of 'apple' at end of string or line
   - Patten: apple$
   - Text: apple apple

  8. End of string or line $ (matches end of string or \n)
   - Problem: Find occurrences of 'apple' at end of string or line
   - Pattern: apple$
   - Text: apple apple
  
  9. $ - Multi-line text
   - Problem: Find occurrences of 'apple' at end of string or line
   - Pattern: apple$
   - Text:
    apple
    apple
   - Internal String: "apple\r\napple"

  10. $ - Turn on multi-line mode (?m) and include \r as optional character
   - Problem: Find occurrences of 'apple' at end of string or line
   - Pattern: (?m)apple\r?$
     ($ 사인은 \n 또는 end of string만 캐치한다. 하지만 윈도우즈는 \r\n을 줄바꿈으로 쓰므로 \r?이 필요)
   - Text: apple appple

 # Character Classes
  - Characer classes are readymade shortcuts that represents a set of characters

  1. Decimal Digit \d
   - Problem: Check if valid decimal digit(0-9)
   - Pattern 1: [0123456789]
   - Pattern 2: [0-9]
   - Pattern 3: \d
   - Not a decimal digit: \D
  
  2. Word Character \w
   - Problem: Check i a character is a valid letter of an alphabet (any language) or digit(숫자도 포함한다)
   - Pattern: \w
   - Text: F16, F18, ㄱ, ㄴ
   - Not a character: \W

  3. Whtie space character \s
   - Matches space, tab, carriage return, new line and so forth
   - Problem: Check for white space character
   - Pattern: \s
   - Text: One	tab space
          Two		tab
   - Not a white space character: \S

  4. Unicode category or Block \p{category}
   - Problem: Find occurrences of punctuation characters(구분문자)
   - Pattern: \p{P} => 구분문자 전체
   - Text: "one,two;three!FOUR?Five*" => ",;!?*"

   - Problem: Find uppercase characters
   - Pattern: \p{Lu} => 대문자 영어
   - Text: "one,two;three!FOUR?Five*"