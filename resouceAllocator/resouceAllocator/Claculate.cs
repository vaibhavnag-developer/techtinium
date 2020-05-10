using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace resouceAllocator
{
    class Claculate
    {
        public string Calculate(int capacity, int hour, int[] units, int[] Cost, string[] unitType, string region)
        {
            List<UnitCost> uc = new List<UnitCost>();
            output op = new output();
            op.region = region;
            int i = 0;
            while (i < Cost.Length)
            {
                if (Cost[i] != 0)
                {
                    UnitCost temp = new UnitCost();
                    double perUnit = Cost[i] / units[i];
                    temp.units = units[i];
                    temp.cost = Cost[i];
                    temp.perUnitCost = perUnit;
                    temp.unitType = unitType[i];
                    uc.Add(temp);                    
                }
                i++;

            }
            uc = uc.OrderBy(x => x.perUnitCost).ToList();
            int j = 0;
            int totalAmount = 0;
            while (capacity != 0)
            {
                if (capacity >= uc[j].units)
                {
                    int count = capacity / uc[j].units;
                    capacity = capacity - uc[j].units * count;
                    totalAmount = totalAmount + uc[j].cost * count;
                    string addTypeValue = "(" + uc[j].unitType + "," + Convert.ToString(count) + ")";
                    op.machine.Add(addTypeValue);
                }
                j++;
            }
            op.total_Cost ="$"+ Convert.ToString(totalAmount * hour);
            string output = JsonConvert.SerializeObject(op);
            return output;
        }
    }
}
