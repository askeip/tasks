using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.In.ReadToEnd();
		    input = input.Replace("%", "*0.01");
            var matches = Regex.Matches(input, @"([0-9\.]*)[\s]*/[\s]*([0-9\.]*)");
		    //var str = Regex.Match(input, "([0-9.]*)[ ]{0,}/[ ]{0,}([0-9.]*)");
		    foreach (Match match in matches)
		    {
                bool skip = true;
		        bool isInt = true;
                foreach (var group in match.Groups)
		        {
                    if (skip)
                    {
                        skip = false;
                    }
                    else if (group.ToString().Contains("."))
                    {
                        isInt = false;
                    }
		        }
		        if (isInt)
		        {
		            input = Regex.Replace(input, match.Value, $"div({match.Groups[1]},{match.Groups[2]})");
		        }
		    }
            var calc = new Sprache.Calc.XtensibleCalculator();
            calc.RegisterFunction("sqrt", (a) => Math.Sqrt(a));
            calc.RegisterFunction("div", (a,b) => (int)(a /b));
            // using expressions
            var expr = calc.ParseExpression(input);
            var func = expr.Compile();
            Console.WriteLine(func().ToString().Replace(",", ".").Replace("бесконечность","Infinity"));
		}
    }
}
