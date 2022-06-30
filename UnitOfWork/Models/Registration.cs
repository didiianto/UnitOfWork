using System;
using System.Collections.Generic;

namespace UnitOfWork.Models
{
    public partial class Registration
    {
        public Guid Id { get; set; }
        public string SerialNo { get; set; }
        public string Name { get; set; }
        public string Idtype { get; set; }
        public string Idnumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Organisation { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateSubmited { get; set; }
        public string Designation { get; set; }
        public string StepsAction { get; set; }
        public bool? IsOpenForm { get; set; }
    }
}
