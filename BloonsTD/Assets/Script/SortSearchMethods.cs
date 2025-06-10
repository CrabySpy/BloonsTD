using UnityEditor.Rendering;
using UnityEngine;
using System.Linq;

public class SortSearchMethods
{
    //Bubble Sort
    public static TowerInfo[] BubbleSortCost(TowerInfo[] nums)
    {
        for (int n = 0; n < nums.Length; n++)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i].Cost > nums[i + 1].Cost)
                {
                    TowerInfo temp = nums[i];
                    nums[i] = nums[i + 1];
                    nums[i + 1] = temp;
                }
            }
        }

        return nums;
    }

    public static TowerInfo[] BubbleSortRange(TowerInfo[] nums)
    {
        for (int n = 0; n < nums.Length; n++)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i].Range > nums[i + 1].Range)
                {
                    TowerInfo temp = nums[i];
                    nums[i] = nums[i + 1];
                    nums[i + 1] = temp;
                }
            }
        }
        return nums;
    }
}
