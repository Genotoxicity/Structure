using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    public class LayoutElement
    {
        public int Id { get; private set; }

        public List<int> InIds { get; private set; }

        public List<int> OutIds { get; private set; }

        public LayoutElement(int id)
        {
            Id = id;
            InIds = new List<int>();
            OutIds = new List<int>();
        }

    }
}
