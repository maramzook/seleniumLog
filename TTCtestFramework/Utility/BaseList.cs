using System.Collections.Generic;

namespace TTCtestFramework.Utility
{
    /// <summary>
    /// This class expands functionality of List<T> class.
    /// </summary>

    public class BaseList<T> : List<T>
    {
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public BaseList() : base() { }
        public BaseList(IEnumerable<T> collection) : base(collection) { }
    }
}
