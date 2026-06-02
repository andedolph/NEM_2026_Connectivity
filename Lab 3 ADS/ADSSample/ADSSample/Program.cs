using TwinCAT;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;
using TwinCAT.ValueAccess;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new AdsClient();
            client.Connect(AmsNetId.Parse("199.4.42.250.1.1"), 851);

            if (client.IsConnected)
            {
                var symbolLoader = SymbolLoaderFactory.Create(client, new SymbolLoaderSettings(SymbolsLoadMode.VirtualTree));
                foreach (var symbol in symbolLoader.Symbols.Where(s => s.InstancePath.Contains("MAIN")))
                {
                    OutputSymbols(symbol);
                }
            }
        }

        static void OutputSymbols(ISymbol symbol, int level = 0)
        {
            string s = "";
            for (var i = 0; i < level; i++)
            {
                s += '\t';
            }
            Console.WriteLine($"{s}{symbol.InstancePath}");
            string attribute = "";

            if (symbol.Attributes.Count > 0)
            {
                attribute = $"{symbol.Attributes.First().Name} : {symbol.Attributes.First().Value}";
            }
            Console.WriteLine($"{s}{attribute}");
            foreach (var sub in symbol.SubSymbols)
            {
                OutputSymbols(sub, level + 1);
            }
        }
    }
}