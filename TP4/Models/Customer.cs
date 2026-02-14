namespace TP4.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsSubscribed { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
    }

}
