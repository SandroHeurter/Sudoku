using System.Collections.Generic;
using NUnit.Framework;
using Sudoku.Domain.Strategies;

namespace Sudoku.Tests.TestData
{
    public class SudokuFormatData
    {
        public static readonly List<SudokuFormat> Formats = new()
        {
            new SudokuFormat("4x4", "0340400210030210"),
            new SudokuFormat("6x6", "003010560320054203206450012045040100"),
            new SudokuFormat("9x9", "700509001000000000150070063003904100000050000002106400390040076000000000600201004")
        };

        public static object[] FormatsWithSolver =
        {
            new object[] { Formats[0].Format, Formats[0].Data, typeof(BackTrackStrategy) },
            new object[] { Formats[1].Format, Formats[1].Data, typeof(BackTrackStrategy) },
            new object[] { Formats[2].Format, Formats[2].Data, typeof(BackTrackStrategy) },
        };

        public static object[] FormatsWithLengths =
        {
            new object[] { Formats[0].Format, Formats[0].Data, Formats[0].Data.Length },
            new object[] { Formats[1].Format, Formats[1].Data, Formats[1].Data.Length },
            new object[] { Formats[2].Format, Formats[2].Data, Formats[2].Data.Length },
        };

    }

    public readonly struct SudokuFormat
    {
        public SudokuFormat(string format, string data)
        {
            Format = format;
            Data = data;
        }

        public string Format { get; }
        public string Data { get; }
    }
}