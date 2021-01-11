using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class PagingHelper
    {
        public static int GetTotalPagesCount(int totalCount, int pageSize)
        {
            return (int)Math.Ceiling((decimal)totalCount / pageSize);
        }

        public static IEnumerable<int> GetPageNumbers(int currentPageNumber, int totalPagesCount, int maxPageNumbersToDisplay)
        {
            if (totalPagesCount == 0)
            {
                return Enumerable.Empty<int>();
            }

            var half = GetHalfMaxPageNumbersToDisplay(maxPageNumbersToDisplay);
            var lastPageNumber = currentPageNumber + half;
            var firstPageNumber = lastPageNumber - maxPageNumbersToDisplay + 1;            
            var pageNumbers = HandleEdgeCases(firstPageNumber, lastPageNumber, totalPagesCount, maxPageNumbersToDisplay);

            return pageNumbers;
        }

        public static bool HasHiddenRightPageNumbers(IEnumerable<int> pageNumbers, int totalPagesCount)
        {
            if (!pageNumbers.Any())
            {
                return false;
            }

            return pageNumbers.Last() < totalPagesCount;
        }

        public static bool HasHiddenLeftPageNumbers(IEnumerable<int> pageNumbers)
        {
            if (!pageNumbers.Any())
            {
                return false;
            }

            return pageNumbers.First() != 1;
        }

        private static IEnumerable<int> HandleEdgeCases(int firstPageNumber, int lastPageNumber, int totalPagesCount, int maxPageNumbersToDisplay)
        {
            if (lastPageNumber > totalPagesCount)
            {
                lastPageNumber = totalPagesCount;
                firstPageNumber = lastPageNumber - maxPageNumbersToDisplay;
            }
            if (firstPageNumber < 1)
            {
                firstPageNumber = 1;
                lastPageNumber = Math.Min(totalPagesCount, maxPageNumbersToDisplay);
            }

            if (firstPageNumber == 2)
            {
                firstPageNumber = 1;
            }
            if (lastPageNumber == totalPagesCount - 1)
            {
                lastPageNumber = totalPagesCount;
            }

            return Enumerable.Range(firstPageNumber, lastPageNumber - firstPageNumber + 1);
        }

        private static int GetHalfMaxPageNumbersToDisplay(int maxPageNumbersToDisplay)
        {
            return (int)Math.Floor((decimal)(maxPageNumbersToDisplay - 1) / 2);
        }
    }
}