using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace TestProject1
{
    [TestFixture]
    public class UsageExample
    {
        [Test]
        public void ValidationExample1()
        {
            // ^ : 시작문자
            // /d : 0-9
            // + : 하나 이상{equals {1, }
            // $ : 종료문자
            string pattern = @"^\d+$";

            var test = "1234";
            var invalidText = "12A3C";

            var result = Regex.IsMatch(test, pattern);

            Assert.That(result, Is.EqualTo(true));

            result = Regex.IsMatch(invalidText, pattern);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void ValidationExample2()
        {
            string pattern = @"^\d+$";
            string[] positiveTest = {"123456", "456", "321082", "0820102"};
            string[] negativeTest = {"ABCD", "A1234", "1234AB", "  123", "321  ", "  111   ", "123 4567", "123\n456"};

            foreach (var test in positiveTest)
            {
                var result = Regex.IsMatch(test, pattern);
                Assert.That(result, Is.EqualTo(true));
            }

            foreach (var test in negativeTest)
            {
                var result = Regex.IsMatch(test, pattern);
                Assert.That(result, Is.EqualTo(false));
            }
        }

        [Test]
        public void MatchExample()
        {
            // \b : On word boundary, 스페이스나 따옴표와 같이 단어가 구분되는 부분을 표시
            // \d : 정수
            // {5} : 5글자
            // \b : 글자 바운더리
            string pattern = @"\b\d{5}\b";
            string text = "NY Postal Codes are 10001, 10002, 10003, 10004";
            var answer = new[] {"10001", "10002", "10003", "10004"};


            Match match = Regex.Match(text, pattern);
            var i = 0;
            while (match.Success)
            {
                Assert.That(match.Value, Is.EqualTo(answer[i++]));

                match = match.NextMatch();
            }
        }

        [Test]
        public void GroupExample()
        {
            // (?<그룹명>패턴) : 조건의 그룹핑, 추후 그룹단위로 뽑아 낼 수 있음
            string pattern = @"(?<year>\d{4})(?<month>\d{2})(?<day>\d{2})(?<hour>\d{2})(?<minute>\d{2})";

            string text = "Timestamp=202009291251";
            Match match = null;
            try
            {

                match = Regex.Match(text, pattern);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Pattern: {pattern}");
            Console.WriteLine($"Text: {text}");

            Assert.That(match?.Success, Is.EqualTo(true));

            Assert.That(match?.Value, Is.EqualTo("202009291251"));
            Assert.That(match?.Index, Is.EqualTo(text.IndexOf('2')));
            Assert.That(match?.Length, Is.EqualTo("202009291251".Length));

            Assert.That(match?.Groups["year"].Value, Is.EqualTo("2020"));
            Assert.That(match?.Groups["year"].Success, Is.EqualTo(true));

            Assert.That(match?.Groups["month"].Value, Is.EqualTo("09"));
            Assert.That(match?.Groups["month"].Success, Is.EqualTo(true));

            Assert.That(match?.Groups["day"].Value, Is.EqualTo("29"));
            Assert.That(match?.Groups["day"].Success, Is.EqualTo(true));

            Assert.That(match?.Groups["hour"].Value, Is.EqualTo("12"));
            Assert.That(match?.Groups["hour"].Success, Is.EqualTo(true));

            Assert.That(match?.Groups["minute"].Value, Is.EqualTo("51"));
            Assert.That(match?.Groups["minute"].Success, Is.EqualTo(true));
        }

        [Test]
        public void ReplaceExample()
        {
            // value 그룹 (?<value> )
            // * : zero or more occurence of preceding character
            // (,\d{3}) : ,XXX의 숫자가 0회 이상 반복
            // \. : . 점, 소수점할때 사용
            // (\.\d{2})? : .소수2자리가 0회 또는 1회 있음
            // \s : space
            // \s+ : 하나 이상의 스페이스
            // ? : zero or one occurence (equal to {0, 1}
            var pattern = @"(?<value>\d+(,\d{3})*(\.\d{2})?)\s+dollar(s)?";
            string replacePattern = @"**USD ${value}**";

            string text = @"Widget Unit cost: 12,000.56 dollars
Taxes: 234.00 dollars
Total: 12,234.56 dollars";

            string newText = Regex.Replace(text, pattern, replacePattern);

            Console.WriteLine($"Pattern: {pattern}");
            Console.WriteLine($"Replace Pattern: {replacePattern}");
            Assert.That(newText, Is.EqualTo(@"Widget Unit cost: **USD 12,000.56**
Taxes: **USD 234.00**
Total: **USD 12,234.56**"));
        }


        public static string CelsiusToFahrenheit(Match m)
        {
            float degCelsius = float.Parse(m.Groups["celsius"].Value);

            float degF = 32.0f + (degCelsius * 9.0f / 5.0f);

            return degF + @"ºF";
        }

        [Test]
        public void ReplaceExample2()
        {
            // (?<celsius> ) : celsius그룹
            // \d+ : 하나 이상의 숫자
            // \u00B0 : unicode의 특정 문자를 나타냄, º
            string pattern = @"(?<celsius>\d+)\u00BAC";
            string text = @"Today's temperature is 32ºC";

            // MatchEvaluator : delegate로, Replace 함수 안에 넣을때 사용
            string newText = Regex.Replace(text, pattern, new MatchEvaluator(CelsiusToFahrenheit));

            Console.WriteLine(pattern);
            Console.WriteLine(text);

            Assert.That(newText, Is.EqualTo(@"Today's temperature is 89.6ºF"));
        }

        [Test]
        public void SplitExample()
        {
            // \d+ : 하나 이상의 숫자
            // \p : 일치하는 유니코드 범주를 검색
            // \p{P} : 모든 문장부호
            // \s : 공백문자
            // \s* : 0개 이상의 공백문자
            string pattern = @"\d+\p{P}\s*";
            string text = @"Here is the shopping list: 1.     Cilantro   2) Carrot     3. Milk      4.Eggs";
            string[] splitText = Regex.Split(text, pattern);

            Console.WriteLine($"Pattern: {pattern}");
            Console.WriteLine($"Text: {text}");

            Assert.That(splitText[0].Trim(), Is.EqualTo("Here is the shopping list:"));
            Assert.That(splitText[1].Trim(), Is.EqualTo("Cilantro"));
            Assert.That(splitText[2].Trim(), Is.EqualTo("Carrot"));
            Assert.That(splitText[3].Trim(), Is.EqualTo("Milk"));
            Assert.That(splitText[4].Trim(), Is.EqualTo("Eggs"));
        }

        [Test]
        public void VerbatimStringExample()
        {
            // \x09 : 아스키 코드 Horizontal tab
            string[] regularString =
            {
                "\\d\t\\d", "\\d\x09\\d", "Hello\"World",
                "\\\\server\\share\\file.txt", "Line1\nLine2"
            };

            // VerbatimString : 텍스트의 앞에 '@'을 붙여줘서 백슬래쉬를 줄이고 문자 그대로 표현하는 것
            // 문자 그대로 읽기 때문에 특정 코드가 아니라, 문자 그 자체를 표시한다.
            // 쌍따옴표는 ""를 사용한다
            // line feed는 문자 그대로 \n로 푯된다.
            string[] verbatimString =
            {
                @"\d\t\d", @"\d\x09\d", @"Hello""World",
                @"\\server\share\file.txt", @"Line1\nLine2"
            };

            for (int i = 0; i < regularString.Length; i++)
            {
                Console.WriteLine(regularString[i]);
                Console.WriteLine(verbatimString[i]);
            }

            /*
            # Result
            \d	\d
            \d\t\d

            \d	\d
            \d\x09\d

            Hello"World
            Hello"World

            \\server\share\file.txt
            \\server\share\file.txt

            Line1
            Line2
            Line1\nLine2
            */


        }
        public bool IsInteger(string text)
        {
            string pattern = @"\d+";

            return Regex.IsMatch(text, pattern);
        }

        [Test]
        public void IntegerTest()
        {
            string text = "1234";

            Assert.That(IsInteger(text), Is.True);

            string text2 = "asdf";

            Assert.That(IsInteger(text2), Is.False);
        }

        [Test]
        public void IntegerUnitTest()
        {
            // 규칙이 /d+인경우 숫자외에 다른 글자가 있어도 매치된다
            var passList = new string[]{"123", "456", "900", "0991"};
            var failList = new string[]{"a1234", "123a", "1 2 3", "1\t2", " 12", "45 "};

            // integer misclassified as non-integer
            var falseNegative = new List<string>();
            // incorrectly classified as integer
            var falsePositive = new List<string>();

            foreach (string s in passList)
            {
                if(!IsInteger(s))
                {
                    falseNegative.Add(s);
                }
            }

            foreach (string s in failList)
            {
                if(IsInteger(s))
                {
                    falsePositive.Add(s);
                }
            }

            var falseNegativeAnswer = new List<string>()
            {

            };
            var falsePositiveAnswer = new List<string>()
            {
                "a1234", "123a", "1 2 3", "1\t2", " 12", "45 "
            };
            
            Assert.That(falseNegative.Count, Is.EqualTo(0));
            for (int i = 0; i < falsePositiveAnswer.Count; i++)
            {
                Assert.That(falsePositiveAnswer[i], Is.EqualTo(falsePositive[i]));
                
            }

        }

        public bool IsInteger2(string text)
        {
            string pattern = @"^\d+$";

            return Regex.IsMatch(text, pattern);
        }

        [Test]
        public void IntegerUnitTest2()
        {
            var passList = new string[] { "123", "456", "900", "0991" };
            var failList = new string[] { "a1234", "123a", "1 2 3", "1\t2", " 12", "45 " };

            // integer misclassified as non-integer
            var negative = new List<string>();
            // incorrectly classified as integer
            var positive = new List<string>();

            foreach (string s in passList)
            {
                if (!IsInteger2(s))
                {
                    negative.Add(s);
                }
            }

            foreach (string s in failList)
            {
                if (IsInteger2(s))
                {
                    positive.Add(s);
                }
            }
            Assert.That(positive.Count, Is.EqualTo(0));
            Assert.That(negative.Count, Is.EqualTo(0));
        }

        [Test]
        public void FirstMatch()
        {
            // Use Regex.Match to retrive matching substring
            string pattern = @"\d+";
            string text = "my lucky number is 42";

            string answer = "42";

            Match match = Regex.Match(text, pattern);


            Assert.That(match.Success, Is.True);
            Assert.That(match.Value, Is.EqualTo(answer));
            Assert.That(match.Length, Is.EqualTo(answer.Length));
        }

        [Test]
        public void IterateMatches()
        {
            // Use Regex.Match to iterate all matches
            string pattern = @"\d+";
            string text = "my postal code are 1001, 1002, 1003, 10005";

            var answer = new string[] {"1001", "1002", "1003", "10005"};

            Match match = Regex.Match(text, pattern);

            int i = 0;
            while (match.Success)
            {
                Assert.That(match.Value, Is.EqualTo(answer[i]));
                Assert.That(match.Length, Is.EqualTo(answer[i].Length));
                i++;
                match = match.NextMatch();
            }

        }

        [Test]
        public void IterateMatchesLazy()
        {
            // Use Regex.Match to iterate all matches
            string pattern = @"\d+";
            string text = "my postal code are 1001, 1002, 1003, 10005";

            var answer = new string[] { "1001", "1002", "1003", "10005" };


            MatchCollection matchCollection = Regex.Matches(text, pattern);

            int i = 0;
            // Lazy Evaluation
            foreach (Match match in matchCollection)
            {
                Assert.That(match.Value, Is.EqualTo(answer[i]));
                Assert.That(match.Length, Is.EqualTo(answer[i].Length));
                i++;
            }
            
            matchCollection = Regex.Matches(text, pattern);

            Console.WriteLine($"Total Matches: {matchCollection.Count}");
            //Match match = Regex.Match(text, pattern);

            //int i = 0;
            //while (match.Success)
            //{
            //    Assert.That(match.Value, Is.EqualTo(answer[i]));
            //    Assert.That(match.Length, Is.EqualTo(answer[i].Length));
            //    i++;
            //    match = match.NextMatch();
            //}

        }
    }
}
