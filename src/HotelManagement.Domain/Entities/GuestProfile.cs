using HotelManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities
{
    public class GuestProfile
    {
        public int ProfileId { get; set; }
        public string GuestId { get; set; }
        public IdType IdType { get; set; }
        public string IdNumber { get; set; }
        public string nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        [ForeignKey(nameof(GuestId))]
        virtual public Guest Guest { get; set; }

    }
}
