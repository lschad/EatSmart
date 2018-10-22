namespace SchadLucas.EatSmart.UI

{
    public interface IRegion
    {
        object RegionContent { get; set; }
        object RegionContext { get; set; }
        string RegionName { get; set; }
        bool Visible { get; set; }
    }
}