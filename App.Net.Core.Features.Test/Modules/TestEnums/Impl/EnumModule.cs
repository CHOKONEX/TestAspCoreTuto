namespace App.Net.Core.Features.Test.Modules.TestEnums.Impl
{
    public class EnumModule
    {
        public enum EUnitOfMeasurement : int
        {
            [Description("UN")]
            Unity = 1,

            [Description("MG")]
            Milligram = 2,

            [Description("G")]
            Gram = 3,

            [Description("KG")]
            Kilogram = 4,

            [Description("L")]
            Liter = 5
        }
    }
}
