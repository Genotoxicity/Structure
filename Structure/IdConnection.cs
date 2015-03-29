using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    public class IdConnection
    {
        public int Id { get; private set; }

        public int Id1 { get; private set; }

        public int Id2 { get; private set; }
        
        public IdConnection(int id, int id1, int id2)
        {
            Id = id;
            Id1 = Math.Min(id1, id2);
            Id2 = Math.Max(id1, id2);
        }

        public override bool Equals(object obj)
        {
            IdConnection c2 = obj as IdConnection;
            if (c2.Id1 == Id1 && c2.Id2 == Id2)
                return true;
            return false;
        }

        public static bool operator == (IdConnection c1, IdConnection c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(IdConnection c1, IdConnection c2)
        {
            return !c1.Equals(c2);
        }

        public override int GetHashCode()
        {
            return Id1 * Id2;
        }

    }
}
