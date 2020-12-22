using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Questify.Builder.Services.TasksService.TaskClasses
{
    [DataContract]
    public class BuilderTaskResult
    {
        public enum TaskTerminationState { Completed = 0, Cancelled, Halted }

        private List<string> _info = null;
        private List<string> _errors = null;
        private List<string> _warnings = null;
        private List<string> _exceptions = null;

        [DataMember]
        public TaskTerminationState TaskTermination { get; set; }

        [DataMember]
        public List<string> Info { get { if (_info == null) _info = new List<string>(); return _info; } set { _info = value; } }

        [DataMember]
        public List<string> Errors { get { if (_errors == null) _errors = new List<string>(); return _errors; } set { _errors = value; } }

        [DataMember]
        public List<string> Warnings { get { if (_warnings == null) _warnings = new List<string>(); return _warnings; } set { _warnings = value; } }

        [DataMember]
        public List<string> Exceptions { get { if (_exceptions == null) _exceptions = new List<string>(); return _exceptions; } set { _exceptions = value; } }
    }
}