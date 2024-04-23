namespace LMVC.Models
{
    public class AuthenticationResponse
    {
        public required string Status { get; set; }
        public required int StaffId { get; set; }
        public required string StaffName { get; set; }
        //public required string StaffName { get; set; }
        public required string Email { get; set; }
        //public required string StaffName { get; set; }
    }    
}
