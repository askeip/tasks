﻿using System;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.In.ReadToEnd();

            var calc = new Sprache.Calc.XtensibleCalculator();
            calc.RegisterFunction("sqrt", (a) => Math.Sqrt(a));
            // using expressions
            var expr = calc.ParseExpression(input);
            var func = expr.Compile();
            Console.WriteLine(func().ToString().Replace(",", ".").Replace("бесконечность","Infinity"));
        }
    }
}
