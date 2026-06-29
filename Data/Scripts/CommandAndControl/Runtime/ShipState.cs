using System.Collections.Generic;

namespace AGS
{
    public sealed class ShipState
    {
        public ShipState(long constructId)
        {
            ConstructId = constructId;
            Providers = new List<ProviderSnapshot>();
            Alerts = new List<AlertRecord>();
            Contacts = new List<ContactRecord>();
            Security = new SecurityState();
            Engineering = new EngineeringState();
        }

        public long ConstructId { get; private set; }
        public List<ProviderSnapshot> Providers { get; private set; }
        public List<AlertRecord> Alerts { get; private set; }
        public List<ContactRecord> Contacts { get; private set; }
        public SecurityState Security { get; private set; }
        public EngineeringState Engineering { get; private set; }
        public int Tick { get; set; }
    }
}
