using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Utils
{
    public class Provider<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> observers = new();

        protected void Notify(T value)
        {
            foreach (var observer in observers) observer.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer)) observers.Add(observer);

            return new UnSubscriber<T>(observers, observer);
        }

    }
}