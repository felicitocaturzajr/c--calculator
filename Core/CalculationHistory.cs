using System;
using System.Collections.Generic;

namespace Calculator.Core
{
    public class CalculationHistory
    {
        private readonly List<string> _history = new();
        private const int MaxEntries = 50;

        public void Add(string entry)
        {
            _history.Add($"[{DateTime.Now:HH:mm:ss}] {entry}");
            if (_history.Count > MaxEntries)
                _history.RemoveAt(0);
        }

        public IReadOnlyList<string> GetAll() => _history.AsReadOnly();

        public void Clear() => _history.Clear();

        public bool HasHistory => _history.Count > 0;
    }
}
