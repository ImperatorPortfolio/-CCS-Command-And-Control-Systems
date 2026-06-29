namespace AGS
{
    public enum BootPhase
    {
        MissingController,
        Bios,
        Logo,
        Desktop
    }

    public sealed class BootState
    {
        private const int BiosTicks = 90;
        private const int LogoTicks = 60;

        public BootPhase Phase { get; private set; }
        public int Ticks { get; private set; }

        public BootState()
        {
            Phase = BootPhase.MissingController;
        }

        public void Update(bool hasController)
        {
            if (!hasController)
            {
                Phase = BootPhase.MissingController;
                Ticks = 0;
                return;
            }

            if (Phase == BootPhase.MissingController)
            {
                Phase = BootPhase.Bios;
                Ticks = 0;
                return;
            }

            if (Phase == BootPhase.Desktop)
            {
                return;
            }

            Ticks++;
            if (Phase == BootPhase.Bios && Ticks >= BiosTicks)
            {
                Phase = BootPhase.Logo;
                Ticks = 0;
            }
            else if (Phase == BootPhase.Logo && Ticks >= LogoTicks)
            {
                Phase = BootPhase.Desktop;
                Ticks = 0;
            }
        }
    }
}
