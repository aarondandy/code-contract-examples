using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

namespace Sample
{
    class Program
    {
        static void Main(string[] args) {
            var quiet = Transform("Hello.", Char.ToLower);
            Console.WriteLine("{0} ({1})", quiet, quiet.Length);
            var yelling = Transform("Hello.", Char.ToUpper);
            Console.WriteLine("{0} ({1})", yelling, yelling.Length);

            Console.WriteLine("Transformed: {0}", SuccessfullyTransformedChars);
            Console.ReadKey();
        }




        public static int SuccessfullyTransformedChars = 0;

        public static string Transform(string source, Func<char, char> tx, bool canYell = false) {
            if (tx == null) throw new ArgumentNullException("tx"); // legacy require
            Contract.Requires(source != null);
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.EnsuresOnThrow<Exception>(Contract.OldValue(SuccessfullyTransformedChars) == SuccessfullyTransformedChars);

            var result = String.Concat(source.Select(tx));
            Interlocked.Add(ref SuccessfullyTransformedChars, result.Length);

            if (!canYell && result.Any(Char.IsUpper))
                throw new InvalidOperationException("NO YELLING!");

            return result;
        }



    }
}
