using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjct4app
{
    public interface Option<T>
    {
        U Visit<U>(Func<U> onNone, Func<T, U> onSome);
        void Visit(Action onNone, Action<T> onSome);
    }
    public class None<T> : Option<T>
    {
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onNone();
        }
        public void Visit(Action onNone, Action<T> onSome)
        {
            onNone();
        }
    }
    public class Some<T> : Option<T>
    {
        public T value;
        public Some(T value)
        {
            this.value = value;
        }
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onSome(value);
        }
        public void Visit(Action onNone, Action<T> onSome)
        {
            onSome(value);
        }
    }
    public interface Iterator<T>
    {
        Option<T> GetNext();
        Option<T> GetCurrent();
        void Reset();
    }

    public class ListIterator<T> : Iterator<T>
    {
        public List<T> source;
        public int current;

        public ListIterator()
        {
            current = 0;
            source = new List<T>();
            Reset();
        }
        public void Add(T item)
        {
            source.Add(item);
        }

        public Option<T> GetNext()
        {
            current++;
            return GetCurrent();
        }

        public void Reset()
        {
            current = 0;
        }

        public Option<T> GetCurrent()
        {
            if (current >= source.Count)
            {
                return new None<T>();
            }
            return new Some<T>(source[current]);
        }
    }
}
