using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utils;

namespace Sudoku.Domain.Models
{
    public class SquareLeaf : IComponent
    {
        private bool locked;
        public bool Locked
        {
            get => locked;
            private init => locked = value;
        }

        public bool IsValid { private get; set; } = true;
        public bool Selected { get; private set; }
        private string value;
        private string hintValue = "0";
        public Coordinate Coordinate { get; }

        public string Value
        {
            get => value;
            set
            {
                if (Locked) return;
                this.value = value;
                IsValid = true;
                hintValue = "0";
            }
        }

        public string HintValue
        {
            get => hintValue;
            set
            {
                if (Locked || !Value.Equals("0")) return;
                hintValue = value;
                IsValid = true;
            }
        }

        public SquareLeaf(bool locked, string value, Coordinate coordinate)
        {
            Locked = locked;
            this.value = value;
            Coordinate = coordinate;
        }

        public SquareLeaf(bool isLocked, Coordinate coordinate)
        {
            Locked = isLocked;
            value = "";
            Coordinate = coordinate;
        }

        public void Select()
        {
            Selected = true;
        }

        public void UnSelect()
        {
            Selected = false;
        }

        public bool Valid(State state, bool setValid)
        {
            return IsValid;
        }

        public bool Valid()
        {
            return IsValid;
        }

        public bool Composite()
        {
            return false;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder)
        {
            return GetChildren();
        }

        public bool IsEmpty()
        {
            return Locked && value.Length == 0;
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return new ReadOnlyCollection<IComponent>(new List<IComponent>());
        }
    }
}