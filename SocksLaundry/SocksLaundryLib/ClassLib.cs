using System;
using System.Linq;
using System.Collections.Generic;


namespace SocksLaundryLib
{
    public class ClassLib
    {

        //Do not delete or edit this method, you can only modify the block
        public int GetMaximumPairOfSocks(int noOfWashes, int[] cleanPile, int[] dirtyPile)
        {
            //You can edit from here down
            int maxPairs = 0;

            //check if the washing machine is working
            if(noOfWashes != 0 )
            {
                //firstly combine the both arrays
                int[] allSocks = cleanPile.Concat(dirtyPile).ToArray();

                //check if there are socks to wash
                if(allSocks.Length != 0)
                {
                    //split the array of all socks by the number of washes the machine can take
                    List<int[]> washes = batchOfWashes(noOfWashes, allSocks);

                    int[] pairsOfSocks = getPairsOfSocks(washes);

                    maxPairs = pairsOfSocks.Length;
                }
            }

            return maxPairs;
        }

        /**
         * You can create various helper methods
         * */
        public List<int[]> batchOfWashes(int noOfWahes, int[] allSocks)
        {
            List<int[]> allWashes = new List<int[]>();

            int counter = 0;

            foreach (int s in allSocks)
            {
                int socksinBatch = allSocks.Length / noOfWahes;
                int[] batchOfSocks = new int[socksinBatch];

                batchOfSocks[counter] = s;

                if(counter == socksinBatch - 1)
                {
                    allWashes.Add(batchOfSocks);
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }

            return allWashes;
        }

        public int[] getPairsOfSocks(List<int[]> washes)
        {
            List<int> pairs = new List<int>();
            List<int> pairsInBatch = new List<int>();
            List<int> paired = new List<int>();
            List<int> currentBatch = new List<int>();
            List<int> nextBatch = new List<int>();

            foreach (int[] wash in washes)
            {
                if (washes.IndexOf(wash) == 0)
                {
                    currentBatch = wash.ToList<int>();
                }
                else if(paired.Count == 0)
                {
                    paired = currentBatch.ToArray().Intersect(wash.ToArray()).ToList<int>();
                    currentBatch = wash.ToList<int>();
                    nextBatch = paired;
                }
                else
                {
                    paired = currentBatch.ToArray().Intersect(nextBatch.ToArray()).ToList<int>();
                    currentBatch = wash.ToList<int>();
                    nextBatch = paired;
                }

                var singlePairs = wash.ToList<int>().GroupBy(w => w)
                    .Where(p => p.Count() > 1)
                    .Select(p => p.Key).ToList<int>();

                foreach (int p in singlePairs)
                    pairsInBatch.Add(p);
            }

            pairs = paired.Concat(pairsInBatch).ToList<int>();

            return pairs.ToArray<int>();
        }
    }


}
