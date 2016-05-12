using System;
using System.Linq.Expressions;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.In.ReadToEnd();

            var calc = new Sprache.Calc.XtensibleCalculator();
		    calc.RegisterFunction("sqrt", Math.Sqrt);
		    input = input.Replace("%", "* 0.01");
            // using expressions
            var expr = calc.ParseExpression(input);
            var func = expr.Compile();
            if (input.Contains("."))
                Console.WriteLine(func().ToString().Replace(",", ".").Replace("бесконечность","Infinity"));
            else
            {
                Console.WriteLine(((int)func()).ToString().Replace(",", ".").Replace("бесконечность", "Infinity"));
            }
		}
    }
}
