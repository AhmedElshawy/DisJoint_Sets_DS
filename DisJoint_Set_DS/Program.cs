namespace DisJoint_Set_DS
{
    public class DisjointSet
    {
        private Dictionary<int, int> _parents;
        private Dictionary<int, int> _rank;

        public DisjointSet()
        {
            _parents= new Dictionary<int, int>();
            _rank= new Dictionary<int, int>();
        }

        public void MakeSet(int i)
        {
            _parents[i] = i;
            _rank[i] = 0;
        }

        public int GetProductOfTreesHeight()
        {
            List<int> Roots = new List<int>();

            foreach (var item in _parents)
            {
                if (item.Key == item.Value)
                    Roots.Add(item.Key);
            }

            int product = 1;
            foreach (var item in Roots) 
            {
                product *= _rank[item];
            }

            return product;
        }

        public int GetMaxHeightOfTree()
        {
            List<int> Roots = new List<int>();

            foreach (var item in _parents)
            {
                if (item.Key == item.Value)
                    Roots.Add(item.Key);
            }

            var maxHeight = 0;
            foreach (var item in Roots)
            {
                maxHeight = Math.Max(maxHeight, _rank[item]);
            }

            return maxHeight;
        }

        public int FindSetId(int i)
        {
            while (i != _parents[i])
            {
                i = _parents[i];
            }

            return i;
        }

        public int FindSetIdWithPathCompression(int i)
        {
            if (i != _parents[i])
            {
                _parents[i] = FindSetIdWithPathCompression(_parents[i]);
            }

            return _parents[i];
        }

        public void Union(int i , int j)
        {
            int i_id = FindSetId(i);
            int j_id = FindSetId(j);

            if (i_id == j_id)
                return;

            if (_rank[i_id] > _rank[j_id])
            {
                _parents[j_id] = i_id;
            }
            else
            {
                _parents[i_id] = j_id;
                if (_rank[i_id] == _rank[j_id])
                {
                    _rank[j_id] += 1;
                }
            }
        }


    }
    public class Program
    {
        static void Main(string[] args)
        {
            var disjointSet = new DisjointSet();

            for (int i = 1; i <= 12; i++)
            {
                disjointSet.MakeSet(i);
            }
            disjointSet.Union(2, 10);
            disjointSet.Union(7, 5);
            disjointSet.Union(6, 1);
            disjointSet.Union(3, 4);
            disjointSet.Union(5, 11);
            disjointSet.Union(7, 8);
            disjointSet.Union(7, 3);
            disjointSet.Union(12, 2);
            disjointSet.Union(9, 6);


            Console.WriteLine(disjointSet.FindSetIdWithPathCompression(6));
            Console.WriteLine(disjointSet.FindSetIdWithPathCompression(3));
            Console.WriteLine(disjointSet.FindSetIdWithPathCompression(11));
            Console.WriteLine(disjointSet.FindSetIdWithPathCompression(9));

            Console.WriteLine(disjointSet.GetProductOfTreesHeight());

        }
    }
}