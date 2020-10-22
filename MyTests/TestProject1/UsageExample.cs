using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string[] positiveTest = { "123456", "456", "321082", "0820102" };
            string[] negativeTest = { "ABCD", "A1234", "1234AB", "  123", "321  ", "  111   ", "123 4567", "123\n456" };

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
            var answer = new[] { "10001", "10002", "10003", "10004" };


            Match match = Regex.Match(text, pattern);
            var i = 0;
            while(match.Success)
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
    }
}
