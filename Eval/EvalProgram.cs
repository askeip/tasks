using System;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.In.ReadToEnd();

            var calc = new Sprache.Calc.XtensibleCalculator();

            // using expressions
            var expr = calc.ParseExpression(input, x => 2, y => System.Math.PI);
            var func = expr.Compile();
            Console.WriteLine(func());
        }
    }
}
