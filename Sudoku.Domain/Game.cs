#nullable enable

using System.Linq;
using System.Collections.Generic;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;
using Sudoku.Domain.Utils;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain
{
    public class Game : Provider<Game>, IGame
    {
        private readonly IContext context;

        private List<(string format, bool selected)> formatList = new()
        {
            ("4x4", true),
            ("6x6", false),
            ("9x9", false)
        };

        public Board Board { get; private set; }

        public BaseSudoku? sudoku
        {
            get => context.Sudoku();
            set
            {
                context.SetSudoku(value);
                Board = context.ConstructBoard();
                Notify(this);
            }
        }

        public Game(IContext context)
        {
            this.context = context;
            context.SwitchState(new DefinitiveState());
            Board = context.ConstructBoard();
        }

        public void Solve()
        {
            if (sudoku == null) return;

            sudoku.GetSquares()
                .ForEach(square => square.Value = "0");

            context
                .GetStrategy()?
                .Solve(sudoku, context.GetState()!);

            Notify(this);
        }

        public void ValidateSudoku(bool update = true)
        {
            if (sudoku == null) return;

            sudoku.ValidateSudoku(context.GetState()!, true);

            if (update)
            {
                Notify(this);
            }
        }

        public void SwitchState(State state)
        {
            context.SwitchState(state);

            Board = context.ConstructBoard();
        }

        public void SelectSquare(Coordinate coordinate)
        {
            context.GetState()!.Select(coordinate);

            Notify(this);
        }

        public void EnterValue(string value)
        {
            if (int.Parse(value) > sudoku?.MaxValue()) return;

            var orderedSquares = sudoku?.GetSquares();

            var currentSquare = orderedSquares?
                .FirstOrDefault(square => square.Selected);

            if (currentSquare == null) return;

            context.EnterValue(value, currentSquare);

            Notify(this);
        }

        public List<(string format, bool selected)> GetFormats()
        {
            return formatList;
        }

        public void SelectFormat(List<(string format, bool selected)> selection)
        {
            formatList = selection;
        }
    }
}