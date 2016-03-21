﻿using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CycleComparer
    {
        private int[] firstCycle;
        private int[] secondCycle;

        public bool Equals(int[] first, int[] second)
        {
            if (ThereAreNullCollection(first, second))
                return false;

            if (ReferenceEquals(first, second))
                return true;

            firstCycle = first as int[] ?? first.ToArray();
            secondCycle = second as int[] ?? second.ToArray();

            if (firstCycle.Length != secondCycle.Length)
                return false;

            var startIndex = FindFirstCycleStartIndexInSecondCycle();
            if (startIndex == -1)
                return false;

            return IsCyclesEqual(startIndex);
        }

        private bool ThereAreNullCollection(IEnumerable<int> first, IEnumerable<int> second)
        {
            return first == null || second == null;
        }

        private int FindFirstCycleStartIndexInSecondCycle()
        {
            return secondCycle.IndexOf(v => v == firstCycle[0]);
        }

        private bool IsCyclesEqual(int startIndex)
        {
            var cyclesLength = firstCycle.Length;
            for (int i = 0, j = startIndex; i < cyclesLength; i++)
            {
                if (!firstCycle[i].Equals(secondCycle[j]))
                    return false;
                j = j == cyclesLength - 1 ? 0 : j + 1;
            }
            return true;
        }
    }
}