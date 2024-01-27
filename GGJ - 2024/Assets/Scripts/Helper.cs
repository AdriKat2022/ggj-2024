public class Helper
{
    public static bool IsLayerInLayerMask(int layer, int layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}
