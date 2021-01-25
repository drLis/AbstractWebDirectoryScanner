using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AbstractWebDirectoryScanner
{
	class Program
	{
		static async Task ScanAsync(String url, String pathToFile)
		{
			using (var stream = new StreamReader(pathToFile))
			{
				String line = stream.ReadLine();
				while (line is not null)
				{
					if (line.Length > 0 && line[0] == '#' || String.IsNullOrEmpty(line))// Ignore comments and LICENSE at inception of dictionary.
					{
						line = stream.ReadLine();
						continue;
					}

					try
					{
						var request = HttpWebRequest.Create(url + "/" + line);
						HttpWebResponse result = (HttpWebResponse)await request.GetResponseAsync();
						if (result.StatusCode == HttpStatusCode.OK)
						{
							Console.WriteLine(url + "/" + line);
							ScanAsync(url + "/" + line, pathToFile);
						}
					}
					catch (Exception e)
					{
						//Console.WriteLine(e.Message + " " + url + "/" + line);
					}

					line = stream.ReadLine();
				}
			}
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
					ScanAsync(args[0], args[1]).Wait();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
