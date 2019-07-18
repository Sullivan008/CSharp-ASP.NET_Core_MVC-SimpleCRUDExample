using System;

namespace Core.Common.Data.DTOs
{
    public class TeamDTO
    {
        public int TeamID { get; set; }

        public string Name { get; set; }

        public int YearOfFoundation { get; set; }

        public int NumberOfWinWorldChamp { get; set; }

        public bool IsPaidEntryFee { get; set; }
    }
}
