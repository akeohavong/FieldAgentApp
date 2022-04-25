

namespace FieldAgent.Core.Entities
{
    public class MissionAgent
    {

        public int AgentID { get; set; }
        public int MissionID { get; set; }
        public Agent Agent { get; set; }
        public Mission Mission { get; set; }
    }
}
