using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sudoku.Domain;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;
using Sudoku.Parser;

namespace Sudoku.Terminal
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>()!.Run(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<App>();
            services.AddSingleton<IGame, Game>();
            services.AddSingleton<IContext, Context>();
            services.AddSingleton<ISudokuParser, SudokuParser>();

        }
    }
}
