using Lolighter.Data.V2;
using System.Collections.Generic;

namespace Lolighter.Info
{
    internal class Pack
    {
        public List<_Pattern> _pattern { get; set; }
    }

    internal class _Pattern
    {
        public string name { get; set; }
        public List<Notes> _notes { get; set; }
    }
}
