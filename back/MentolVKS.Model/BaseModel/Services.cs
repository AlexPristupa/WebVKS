using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class Services : TableBasedEntityBase
    {
        public int Id { get; set; } // int
        public string Name { get; set; } // varchar(100)
        public string Conffilename { get; set; } // varchar(100)
        public string Pathexe { get; set; } // varchar(100)
        public string Description { get; set; } // varchar(250)
    }
}
