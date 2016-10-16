using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcollection;

namespace ThreadedCollectionTest
{
    public class AllocationObject : IThreadedCollectionObject
    {
        Dictionary<string, int> _storeInv = new Dictionary<string, int>
        {
            { "AAA" , 5 },
            { "BBB", 10 },
            { "CCC", 15 },
            { "DDD", 20 }
        };

        public readonly int Id;
        public readonly int qtyNeeded;
        public string pickedStore { get; private set; } = null;

        public AllocationObject(int idIn, int QtyNeeded)
        {
            Id = idIn;
            qtyNeeded = QtyNeeded;
        }

        public AllocationObject(int idIn, int QtyNeeded, Dictionary<string, int> InventoryIn)
        {
            Id = idIn;
            qtyNeeded = QtyNeeded;
            _storeInv = InventoryIn;
        }

        public async Task Process()
        {
            var selectedStore = _storeInv.FirstOrDefault(x => x.Value >= qtyNeeded);
            await Task.Run(()=> { pickedStore = selectedStore.Key ?? "ZZZ"; });
        }
    }
}
