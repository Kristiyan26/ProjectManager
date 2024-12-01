using ProjectManager.Entities;

namespace ProjectManager.ViewModels.Projects
{
    public class ShareVM
    {

        public int ProjectId { get; set; }  
        public List<int> UserIds { get; set; }  
        public Project Project { get; set; }    
        public List<UserToProject> Shares { get; set; } 

        public List<User> Users { get; set; }


    }
}
