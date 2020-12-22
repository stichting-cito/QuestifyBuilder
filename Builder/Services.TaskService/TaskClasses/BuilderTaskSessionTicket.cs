using System;
using System.Runtime.Serialization;

namespace Questify.Builder.Services.TasksService.TaskClasses
{
    [DataContract]
    public class BuilderTaskSessionTicket
    {
        System.Guid _id;

        public BuilderTaskSessionTicket()
        {
            _id = Guid.NewGuid();
        }

        [DataMember]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}