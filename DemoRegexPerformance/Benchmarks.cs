using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DemoRegexPerformance
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public partial class Benchmarks
    {
        private static readonly List<string> Emails = new List<string> { "max.muster@gmail.com", "maxmuster@gmail.com", "maxmuster.com", "maxmuster@gmail" };

        private static readonly string EmailRegex = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";

        private static readonly Regex StaticRegex = new Regex(EmailRegex);

        private static readonly Regex StaticRegexCompiled = new Regex(EmailRegex, RegexOptions.Compiled);

//#if NET7_0_OR_GREATER
//        [GeneratedRegex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])")]
//        private static partial Regex PartialRegex();
//#endif

        [Benchmark]
        public void MatchInternal()
        {
            var regex = new Regex(EmailRegex);

            Emails.ForEach(email => regex.IsMatch(email));
        }

        [Benchmark]
        public void MatchCompiled()
        {
            var regex = new Regex(EmailRegex, RegexOptions.Compiled);

            Emails.ForEach(email => regex.IsMatch(email));
        }

        [Benchmark]
        public void MatchStaticRegex()
        {
            Emails.ForEach(email => StaticRegex.IsMatch(email));
        }

        [Benchmark]
        public void MatchStaticRegexCompiled()
        {
            Emails.ForEach(email => StaticRegexCompiled.IsMatch(email));
        }

//#if NET7_0_OR_GREATER
//        [Benchmark]
//        public void MatchGeneratedRegex()
//        {
//            Emails.ForEach(email => PartialRegex().IsMatch(email));
//        }
//#endif
    }
}
