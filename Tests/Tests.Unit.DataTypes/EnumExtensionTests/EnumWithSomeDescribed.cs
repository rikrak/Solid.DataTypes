namespace Tests.Unit.DataTypes.EnumExtensionTests
{
    public enum EnumWithSomeDescribed
    {
        [System.ComponentModel.Description(Resources.DescriptionValue)]
        ValueWithDescription,
        ValueWithoutDescription,

        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources), Name = "Resource1")]
        ValueWithDisplay
    }
}