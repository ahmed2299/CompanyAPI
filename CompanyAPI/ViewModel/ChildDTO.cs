﻿namespace CompanyAPI.ViewModel
{
    public class ChildDTO
    {
        public int ChildId { get; set; }

        public int EmployeeId { get; set; }

        public string ChildName { get; set; } = null!;

        public int ChildAge { get; set; }
    }
}
