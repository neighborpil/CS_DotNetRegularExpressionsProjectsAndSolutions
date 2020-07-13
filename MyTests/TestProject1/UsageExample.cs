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
            // @ : 시작문자
            // /d : 0-9
            // + : 하나 이상
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
    }
}
