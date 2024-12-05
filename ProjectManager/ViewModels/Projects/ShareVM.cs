using ProjectManager.Entities;

namespace ProjectManager.ViewModels.Projects
{
    public class ShareVM
    {

        //These fields are used in the HTTP POST
        public int ProjectId { get; set; }  // the id of the project 
        public List<int> UserIds { get; set; }  // list of the ids that has been sent from the form in the 
                                                // share view . List of the cecked checkboxes. 


        //These fields are used in the HTTP GET
        public Project Project { get; set; }   // info for the given project  
        public List<UserToProject> Shares { get; set; } // List of shares.The users that have accsess.We have already shared
                                                        // the project with them

        public List<User> Users { get; set; } // List of users that this project hasn't been shared with yet.



    }
}
