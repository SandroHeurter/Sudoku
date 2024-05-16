using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Utils
{
    public class UnSubscriber<T> : IDisposable
    {
        private readonly IObserver<T> observer;
        private readonly List<IObserver<T>> observers;

        public UnSubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            observers.Remove(observer);
        }
    }
}