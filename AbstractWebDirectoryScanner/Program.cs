using System;

namespace AbstractWebDirectoryScanner
{
	class Program
	{
		static void Scan(String url, String pathToFile)
		{

		}

		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Please, type URL and path to dictionary");
			}
			else
			{
				try
				{
					Console.WriteLine($"{args[0]} {args[1]}");
					Scan(args[0], args[1]);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
