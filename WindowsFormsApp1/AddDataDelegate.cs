using System;

namespace DisplayGraphs
{
    internal class AddDataDelegate
    {
        private Action<string> addDataMethod;

        public AddDataDelegate(Action<string> addDataMethod)
        {
            this.addDataMethod = addDataMethod;
        }
    }
}