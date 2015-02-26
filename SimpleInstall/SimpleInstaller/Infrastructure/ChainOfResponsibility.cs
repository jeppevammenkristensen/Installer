using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleInstaller.Infrastructure
{
    public class ChainOfResponsibility<T>
    {
        private readonly IEnumerable<Chain<T>> _chains;

        public ChainOfResponsibility(params Chain<T>[] chains)
        {
            _chains = chains;
        }

        public Tuple<bool, T> TryGetMatch()
        {
            return _chains.Where(x => x.IsMatch).Select(x => new Tuple<bool, T>(true, x.GetValue())).FirstOrDefault() ??
                   new Tuple<bool, T>(false, default(T));
        }
    }
}