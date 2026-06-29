using System.Collections.Generic;
using VRageMath;

namespace AGS
{
    public sealed class SurfaceMap
    {
        private readonly Dictionary<string, SurfaceCoords> _coordsByKey = new Dictionary<string, SurfaceCoords>();

        public SurfaceMap()
        {
            Add("LargeLCDPanel", 0, new Vector3(-1.23463f, 1.23463f, -1.02366f), new Vector3(-1.23463f, -1.23463f, -1.02366f), new Vector3(1.23463f, -1.23463f, -1.02366f));
            Add("LargeTextPanel", 0, new Vector3(-1.19463f, 0.694398f, -1.02366f), new Vector3(-1.19463f, -0.694398f, -1.02366f), new Vector3(1.19463f, -0.694398f, -1.02366f));
            Add("LargeLCDPanelWide", 0, new Vector3(-2.48463f, 1.23463f, -1.02366f), new Vector3(-2.48463f, -1.23463f, -1.02366f), new Vector3(2.48463f, -1.23463f, -1.02366f));
            Add("TransparentLCDLarge", 0, new Vector3(-1.0842f, 1.08415f, -1.1926f), new Vector3(-1.0842f, -1.08416f, -1.20177f), new Vector3(1.0842f, -1.08415f, -1.1926f));
            Add("LargeLCDPanel5x5", 0, new Vector3(-6.23f, 6.23f, -1.02037f), new Vector3(-6.23f, -6.23f, -1.02037f), new Vector3(6.23f, -6.23f, -1.02037f));
            Add("LargeLCDPanel5x3", 0, new Vector3(-6.23f, 3.73f, -1.02037f), new Vector3(-6.23f, -3.73f, -1.02037f), new Vector3(6.23f, -3.73f, -1.02037f));
            Add("LargeLCDPanel3x3", 0, new Vector3(-3.73f, 3.73f, -1.02037f), new Vector3(-3.73f, -3.73f, -1.02037f), new Vector3(3.73f, -3.73f, -1.02037f));
            Add("LargeBlockCorner_LCD_1", 0, new Vector3(0.957584f, -0.992113f, 1.20251f), new Vector3(0.957584f, -1.20251f, 0.992113f), new Vector3(-0.957585f, -1.20251f, 0.992113f));
            Add("LargeBlockCorner_LCD_2", 0, new Vector3(-0.957585f, -0.992113f, 1.02049f), new Vector3(-0.957585f, -1.20251f, 1.23089f), new Vector3(0.957585f, -1.20251f, 1.23089f));
            Add("LargeBlockCorner_LCD_Flat_1", 0, new Vector3(-0.957585f, -1.17074f, -1.21998f), new Vector3(-0.957585f, -1.17074f, -0.923483f), new Vector3(0.957585f, -1.17074f, -0.923484f));
            Add("LargeBlockCorner_LCD_Flat_2", 0, new Vector3(-0.957585f, -1.17123f, 0.91751f), new Vector3(-0.957585f, -1.17123f, 1.21506f), new Vector3(0.957586f, -1.17123f, 1.21506f));
            Add("LargeProgrammableBlock", 0, new Vector3(-0.435951f, 0.48753f, 0.993358f), new Vector3(-0.435951f, -0.044765f, 0.997674f), new Vector3(0.436368f, -0.044765f, 0.997674f));
            Add("LargeProgrammableBlock", 1, new Vector3(-0.206985f, -0.116908f, 1.0524f), new Vector3(-0.206985f, -0.23073f, 1.18466f), new Vector3(0.235275f, -0.23073f, 1.18466f));
            Add("LargeBlockCockpitIndustrial", 0, new Vector3(-0.918698f, 0.620028f, -0.619588f), new Vector3(-0.973858f, 0.433626f, -0.73788f), new Vector3(-0.630658f, 0.430848f, -0.900063f));
            Add("LargeBlockCockpitIndustrial", 1, new Vector3(-0.462543f, 0.71061f, -0.657349f), new Vector3(-0.49153f, 0.543964f, -0.770974f), new Vector3(-0.210559f, 0.553486f, -0.866144f));
            Add("LargeBlockCockpitIndustrial", 2, new Vector3(-0.1541f, 0.71061f, -0.760206f), new Vector3(-0.142477f, 0.543964f, -0.876892f), new Vector3(0.1541f, 0.553486f, -0.870225f));
            Add("LargeBlockCockpitIndustrial", 3, new Vector3(0.181572f, 0.720131f, -0.75252f), new Vector3(0.210559f, 0.553486f, -0.866144f), new Vector3(0.49153f, 0.543964f, -0.770974f));
            Add("LargeBlockCockpitIndustrial", 4, new Vector3(-0.123102f, -0.3576f, -0.497145f), new Vector3(-0.123105f, -0.422629f, -0.384497f), new Vector3(0.123105f, -0.422629f, -0.384498f));
            Add("LargeBlockCockpitSeat", 0, new Vector3(-0.20481f, 0.054932f, -0.436759f), new Vector3(-0.204811f, -0.20229f, -0.35828f), new Vector3(0.204732f, -0.20229f, -0.35828f));
            Add("LargeBlockCockpitSeat", 1, new Vector3(-0.56155f, 0.014181f, -0.341422f), new Vector3(-0.56155f, -0.209932f, -0.273044f), new Vector3(-0.280005f, -0.22683f, -0.32843f));
            Add("LargeBlockCockpitSeat", 2, new Vector3(0.278656f, -0.001624f, -0.397258f), new Vector3(0.27865f, -0.225961f, -0.32875f), new Vector3(0.56008f, -0.209189f, -0.273803f));
            Add("LargeBlockCockpitSeat", 3, new Vector3(-0.114655f, -0.236712f, -0.310878f), new Vector3(-0.114655f, -0.284872f, -0.197601f), new Vector3(0.114692f, -0.284872f, -0.197601f));
            Add("LargeBlockCockpitSeat", 4, new Vector3(-0.620698f, -0.226691f, -0.168774f), new Vector3(-0.568499f, -0.288196f, -0.078508f), new Vector3(-0.438854f, -0.309467f, -0.167972f));
            Add("LargeBlockCockpitSeat", 5, new Vector3(0.490973f, -0.24806f, -0.258088f), new Vector3(0.438842f, -0.309487f, -0.167938f), new Vector3(0.568511f, -0.288212f, -0.078457f));
            Add("CockpitOpen", 0, new Vector3(-0.70525f, 0.01592f, -0.315901f), new Vector3(-0.70525f, -0.24127f, -0.167418f), new Vector3(0.70525f, -0.24127f, -0.167418f));
            Add("LargeBlockCockpit", 0, new Vector3(-0.683253f, 0.648219f, -0.430285f), new Vector3(-0.683253f, -0.094249f, -0.231341f), new Vector3(0.683253f, -0.094249f, -0.231341f));
            Add("OpenCockpitLarge", 0, new Vector3(-0.32339f, -0.388036f, -0.868841f), new Vector3(-0.32339f, -0.527916f, -0.544065f), new Vector3(0.317703f, -0.527916f, -0.544065f));
            Add("OpenCockpitLarge", 1, new Vector3(-0.961732f, -0.339453f, -0.626006f), new Vector3(-0.852037f, -0.503519f, -0.516081f), new Vector3(-0.617797f, -0.503381f, -0.750337f));
            Add("OpenCockpitLarge", 2, new Vector3(0.632119f, -0.346583f, -0.770753f), new Vector3(0.632119f, -0.449903f, -0.770753f), new Vector3(0.736728f, -0.449903f, -0.666222f));
            Add("OpenCockpitLarge", 3, new Vector3(-1.01423f, -0.352563f, 0.014583f), new Vector3(-0.941375f, -0.423826f, 0.014583f), new Vector3(-0.941375f, -0.423826f, -0.06059f));
            Add("OpenCockpitLarge", 4, new Vector3(1.04306f, -0.3606f, -0.245201f), new Vector3(0.922525f, -0.481052f, -0.245201f), new Vector3(0.922525f, -0.481052f, 0.010062f));
            Add("SmallLCDPanel", 0, new Vector3(-0.741776f, 0.741776f, -0.110717f), new Vector3(-0.741776f, -0.741776f, -0.110717f), new Vector3(0.741776f, -0.741776f, -0.110717f));
            Add("SmallLCDPanelWide", 0, new Vector3(-1.49217f, 0.742556f, -0.110685f), new Vector3(-1.49217f, -0.742556f, -0.110685f), new Vector3(1.49245f, -0.742556f, -0.110685f));
            Add("TransparentLCDSmall", 0, new Vector3(-0.21684f, 0.215277f, -0.24354f), new Vector3(-0.21684f, -0.218385f, -0.24354f), new Vector3(0.21684f, -0.218385f, -0.24354f));
            Add("SmallTextPanel", 0, new Vector3(-0.215663f, 0.21561f, -0.160736f), new Vector3(-0.215663f, -0.21561f, -0.160736f), new Vector3(0.215663f, -0.21561f, -0.160736f));
            Add("SmallBlockCorner_LCD_1", 0, new Vector3(0.201149f, -0.138066f, 0.229387f), new Vector3(0.201149f, -0.229387f, 0.138066f), new Vector3(-0.201149f, -0.229387f, 0.138066f));
            Add("SmallBlockCorner_LCD_2", 0, new Vector3(-0.201149f, -0.138066f, 0.150701f), new Vector3(-0.201149f, -0.229387f, 0.242023f), new Vector3(0.201149f, -0.229387f, 0.242023f));
            Add("SmallBlockCorner_LCD_Flat_1", 0, new Vector3(-0.197906f, -0.210322f, -0.221506f), new Vector3(-0.197906f, -0.210322f, -0.060492f), new Vector3(0.197905f, -0.210322f, -0.060492f));
            Add("SmallBlockCorner_LCD_Flat_2", 0, new Vector3(0.197707f, -0.210349f, -0.060558f), new Vector3(0.197708f, -0.210349f, -0.221411f), new Vector3(-0.197709f, -0.210349f, -0.221412f));
            Add("SmallProgrammableBlock", 0, new Vector3(-0.192459f, 0.227048f, -0.025001f), new Vector3(-0.192459f, -0.125537f, 0.037169f), new Vector3(0.192471f, -0.125537f, 0.037169f));
            Add("SmallProgrammableBlock", 1, new Vector3(-0.188676f, -0.17532f, 0.077997f), new Vector3(-0.18931f, -0.209441f, 0.20358f), new Vector3(0.189101f, -0.209442f, 0.203582f));
            Add("SmallBlockCockpit", 0, new Vector3(-0.114976f, 0.036534f, -0.275231f), new Vector3(-0.114976f, -0.166655f, -0.213239f), new Vector3(0.114768f, -0.166655f, -0.213239f));
            Add("SmallBlockCockpit", 1, new Vector3(-0.330622f, -0.007521f, -0.215116f), new Vector3(-0.330621f, -0.123458f, -0.179744f), new Vector3(-0.174889f, -0.132765f, -0.210248f));
            Add("SmallBlockCockpit", 2, new Vector3(0.174837f, -0.016915f, -0.245605f), new Vector3(0.174838f, -0.132867f, -0.210228f), new Vector3(0.330761f, -0.123549f, -0.179687f));
            Add("SmallBlockCockpit", 3, new Vector3(-0.11472f, -0.202878f, -0.17911f), new Vector3(-0.114723f, -0.251076f, -0.065746f), new Vector3(0.114751f, -0.251076f, -0.065746f));
            Add("SmallBlockCockpitIndustrial", 0, new Vector3(-0.462543f, 0.74513f, -0.596508f), new Vector3(-0.500172f, 0.568964f, -0.712422f), new Vector3(-0.210559f, 0.568964f, -0.809882f));
            Add("SmallBlockCockpitIndustrial", 1, new Vector3(-0.1541f, 0.74513f, -0.694986f), new Vector3(-0.1541f, 0.568964f, -0.81834f), new Vector3(0.1541f, 0.568964f, -0.818339f));
            Add("SmallBlockCockpitIndustrial", 2, new Vector3(0.17293f, 0.74513f, -0.693967f), new Vector3(0.210559f, 0.568964f, -0.809881f), new Vector3(0.500172f, 0.568964f, -0.712421f));
            Add("SmallBlockCockpitIndustrial", 3, new Vector3(-0.123102f, -0.116188f, -0.282139f), new Vector3(-0.123105f, -0.181217f, -0.169491f), new Vector3(0.123105f, -0.181217f, -0.169491f));
            Add("DBSmallBlockFighterCockpit", 0, new Vector3(-0.286403f, 0.058772f, -0.746375f), new Vector3(-0.286403f, -0.249135f, -0.661003f), new Vector3(0.285588f, -0.249135f, -0.661003f));
            Add("DBSmallBlockFighterCockpit", 1, new Vector3(-0.211372f, -0.267189f, -0.410418f), new Vector3(-0.21138f, -0.362092f, -0.332993f), new Vector3(-0.023276f, -0.362089f, -0.332974f));
            Add("DBSmallBlockFighterCockpit", 2, new Vector3(0.022894f, -0.267189f, -0.410418f), new Vector3(0.022886f, -0.362092f, -0.332993f), new Vector3(0.21099f, -0.362089f, -0.332974f));
            Add("DBSmallBlockFighterCockpit", 3, new Vector3(-0.212735f, -0.416471f, -0.222682f), new Vector3(-0.212735f, -0.462862f, -0.040841f), new Vector3(0.212735f, -0.462862f, -0.040841f));
            Add("DBSmallBlockFighterCockpit", 4, new Vector3(-0.058204f, -0.474206f, -0.010117f), new Vector3(-0.044999f, -0.547803f, 0.142677f), new Vector3(0.044999f, -0.547803f, 0.142677f));
            Add("DBSmallBlockFighterCockpit", 5, new Vector3(0.425303f, -0.361203f, 0.080077f), new Vector3(0.311416f, -0.44669f, 0.081541f), new Vector3(0.318325f, -0.453803f, 0.186467f));
            Add("OpenCockpitSmall", 0, new Vector3(-0.180041f, 0.146844f, -0.053429f), new Vector3(-0.180049f, 0.02569f, 0.108006f), new Vector3(0.181396f, 0.025686f, 0.108012f));
            Add("RoverCockpit", 0, new Vector3(-0.392778f, -0.046541f, -0.293668f), new Vector3(-0.392662f, -0.189838f, -0.248914f), new Vector3(-0.155318f, -0.189838f, -0.309159f));
            Add("RoverCockpit", 1, new Vector3(-0.136667f, -0.046311f, -0.362766f), new Vector3(-0.136667f, -0.159087f, -0.324238f), new Vector3(0.136667f, -0.159087f, -0.324238f));
            Add("RoverCockpit", 2, new Vector3(0.155318f, -0.045813f, -0.364009f), new Vector3(0.155318f, -0.189838f, -0.309159f), new Vector3(0.392662f, -0.189838f, -0.248914f));
            Add("RoverCockpit", 3, new Vector3(-0.052947f, -0.08278f, -0.135536f), new Vector3(-0.052947f, -0.138511f, -0.104184f), new Vector3(0.052947f, -0.138511f, -0.104184f));
            Add("RoverCockpit", 4, new Vector3(0.20219f, -0.228944f, -0.228622f), new Vector3(0.195939f, -0.279159f, -0.211448f), new Vector3(0.267393f, -0.279159f, -0.185441f));
            Add("BuggyCockpit", 0, new Vector3(-0.14081f, 0.18805f, -0.369332f), new Vector3(-0.14434f, 0.034122f, -0.310549f), new Vector3(0.14647f, 0.035292f, -0.310549f));
            Add("BuggyCockpit", 1, new Vector3(-0.396802f, 0.164742f, -0.21128f), new Vector3(-0.369163f, 0.03563f, -0.169657f), new Vector3(-0.183721f, 0.03563f, -0.292798f));
            Add("BuggyCockpit", 2, new Vector3(0.21136f, 0.164742f, -0.334421f), new Vector3(0.183721f, 0.03563f, -0.292798f), new Vector3(0.369163f, 0.03563f, -0.169657f));
            Add("LargeTurretControlBlock", 0, new Vector3(-0.593234f, 0.900054f, 0.114358f), new Vector3(-0.593234f, 0.325494f, -0.072266f), new Vector3(0.385896f, 0.325494f, -0.072266f));
            Add("LargeTurretControlBlock", 1, new Vector3(0.483036f, 0.900054f, 0.114358f), new Vector3(0.483036f, 0.576696f, -0.036392f), new Vector3(0.704897f, 0.576696f, -0.036392f));
            Add("LargeTurretControlBlock", 2, new Vector3(-0.520536f, -0.199998f, 0.073605f), new Vector3(-0.520536f, -0.405104f, 0.278711f), new Vector3(-0.34654f, -0.405104f, 0.278711f));
            Add("LargeTurretControlBlock", 3, new Vector3(0.34654f, -0.199998f, 0.073605f), new Vector3(0.34654f, -0.405104f, 0.278711f), new Vector3(0.520536f, -0.405104f, 0.278711f));
            Add("SmallTurretControlBlock", 0, new Vector3(-0.143935f, 0.235472f, -0.306565f), new Vector3(-0.143935f, 0.235472f, -0.091221f), new Vector3(0.144789f, 0.235472f, -0.091221f));
            Add("LargeBlockStandingCockpit", 0, new Vector3(-0.372228f, -0.08327f, -1.12346f), new Vector3(-0.372228f, -0.156863f, -1.06368f), new Vector3(-0.111456f, -0.156863f, -1.06368f));
            Add("LargeBlockStandingCockpit", 1, new Vector3(-0.434899f, -0.205596f, -0.989841f), new Vector3(-0.434899f, -0.251315f, -0.819213f), new Vector3(-0.014451f, -0.251315f, -0.819213f));
            Add("LargeProgrammableBlockReskin", 0, new Vector3(0.29712f, 0.86475f, -0.55811f), new Vector3(0.29712f, 0.4021f, -0.55859f), new Vector3(0.55859f, 0.4021f, -0.29712f));
            Add("LargeProgrammableBlockReskin", 1, new Vector3(0.25024f, -0.05444f, -0.44922f), new Vector3(0.17676f, -0.17944f, -0.37573f), new Vector3(0.37378f, -0.18054f, -0.17761f));
            Add("LargeFullBlockLCDPanel", 0, new Vector3(1.25f, 1.19434f, 1.25f), new Vector3(1.25f, -1.19434f, 1.25f), new Vector3(1.25f, -1.19434f, -1.25f));
            Add("LargeDiagonalLCDPanel", 0, new Vector3(1.25f, 1.19434f, 1.25f), new Vector3(1.25f, -1.19434f, 1.25f), new Vector3(-1.25f, -1.19434f, -1.25f));
            Add("LargeCurvedLCDPanel", 0, new Vector3(1.22852f, 1.19434f, 0.92383f), new Vector3(1.22852f, -1.19434f, 0.92383f), new Vector3(-0.92383f, -1.19434f, -1.22852f));
            Add("LargeBlockInsetEntertainmentCorner", 0, new Vector3(0.84619f, 0.72314f, -0.99854f), new Vector3(0.84619f, -0.25879f, -0.99854f), new Vector3(-0.84277f, -0.25879f, -0.99854f));
            Add("LargeBlockInsetButtonPanel", 0, new Vector3(1.08106f, 0.56592f, 0.69238f), new Vector3(1.08106f, -0.23901f, 0.69238f), new Vector3(1.08106f, -0.23901f, -0.69238f));
            Add("LargeBlockInsetButtonPanel", 1, new Vector3(0.77637f, 0.33179f, -0.93213f), new Vector3(0.77637f, -0.10138f, -0.93213f), new Vector3(0.03107f, -0.10138f, -0.93213f));
            Add("LargeBlockInsetButtonPanel", 2, new Vector3(-0.03345f, 0.33179f, -0.93213f), new Vector3(-0.03345f, -0.10138f, -0.93213f), new Vector3(-0.77881f, -0.10138f, -0.93213f));
            Add("LargeMedicalRoomReskin", 0, new Vector3(1.25195f, 0.44702f, -1.4082f), new Vector3(1.21484f, 0.23706f, -1.4082f), new Vector3(1.21484f, 0.23706f, -1.00977f));
            Add("SmallBlockCapCockpit", 0, new Vector3(-0.51563f, 0.68359f, -0.427f), new Vector3(-0.51563f, 0.6333f, -0.45605f), new Vector3(-0.30518f, 0.6333f, -0.45605f));
            Add("SmallBlockCapCockpit", 1, new Vector3(-0.46753f, 0.23303f, -0.3999f), new Vector3(-0.46704f, 0.04352f, -0.34863f), new Vector3(-0.20093f, 0.04227f, -0.4375f));
            Add("SmallBlockCapCockpit", 2, new Vector3(-0.15991f, 0.22644f, -0.47192f), new Vector3(-0.16016f, 0.08759f, -0.43677f), new Vector3(0.16016f, 0.08759f, -0.43677f));
            Add("SmallBlockCapCockpit", 3, new Vector3(0.20105f, 0.2323f, -0.48755f), new Vector3(0.20093f, 0.04227f, -0.4375f), new Vector3(0.46704f, 0.04352f, -0.34863f));
            Add("HoloLCDLarge", 0, new Vector3(-1.25f, 1.25f, -0.82324f), new Vector3(-1.25f, -1.25f, -0.82324f), new Vector3(1.25f, -1.25f, -0.82324f));
        }

        public bool TryGet(string subtypeId, int index, out SurfaceCoords coords)
        {
            return _coordsByKey.TryGetValue(subtypeId + ":" + index, out coords);
        }

        private void Add(string subtypeId, int index, Vector3 topLeft, Vector3 bottomLeft, Vector3 bottomRight)
        {
            _coordsByKey[subtypeId + ":" + index] = new SurfaceCoords(subtypeId, index, topLeft, bottomLeft, bottomRight);
        }
    }
}

