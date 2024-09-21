

namespace Firstproject.Models
{
    public class JoinTable
    {
        public User User { get; set; }  
        public Userhall Userhall { get; set; }  
        public Hall Hall { get; set; }  
        public Address Address { get; set; }    
    }
}
