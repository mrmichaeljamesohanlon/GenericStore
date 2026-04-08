using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AppModule
    {
        public AppModule()
        {
            Entities = new HashSet<Entity>();
            ScrnsLocalizations = new HashSet<ScrnsLocalization>();
        }

        public int AppModuleId { get; set; }
        public string AppModuleName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<ScrnsLocalization> ScrnsLocalizations { get; set; }
    }
}
