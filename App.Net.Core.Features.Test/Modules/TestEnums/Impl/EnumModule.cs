using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace App.Net.Core.Features.Test.Modules.TestEnums.Impl
{
    public static class EnumModule
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

        //[Flags]. This allows them to be treated as bit masks, storing multiple values
        [Flags] 
        public enum Days
        {
            None = 0b_0000_0000,  // 0
            Monday = 0b_0000_0001,  // 1
            Tuesday = 0b_0000_0010,  // 2
            Wednesday = 0b_0000_0100,  // 4
            Thursday = 0b_0000_1000,  // 8
            Friday = 0b_0001_0000,  // 16
            Saturday = 0b_0010_0000,  // 32
            Sunday = 0b_0100_0000,  // 64
            Weekend = Saturday | Sunday
        }

        [Flags]
        public enum AttackType
        {
            // Decimal                  // Binary
            None = 0,           // 000000
            Melee = 1,           // 000001
            Fire = 2,           // 000010
            Wind = 3,           // 000011
            Ice = 4,           // 000100
            Water = 7,           // 000111
            Poison = 8,           // 001000

            MeleeAndFire = Melee | Fire // 000011 = 3

            /*AND & 
            //two integers it keeps only the bits which are set in both of them
                //000011
                //000100
                // MeleeAndFire & Ice:  000000 = 0

                //000011
                //000010
                // MeleeAndFire & Fire: 000010 = 2
            */
            /*NOT ~ : inverting all the bits of an integer
                //0011
                // => 1100
            */
            /*XOR ^= : two binary values is true only if one or the other is true, but not both
                //000011
                //000010
                // => 00001
            */
            /*SHIFTS right (>>) or left (<<)
                // 1 <<  x => Add 1 zero from right of x
                // 1 << 0000 => 0001
                // 1 << 0001 => 0010
                // 1 << 0010 => 0100

                // 1 >>  x => Add 1 zero from left of x
                // 1 >> 0010 => 0001
            */
        }

        public static string GetNamesFromEnum()
        {
            var stringBuilder = new StringBuilder();
            foreach (string colorEnum in Enum.GetNames(typeof(EUnitOfMeasurement)))
            {
                stringBuilder.Append(colorEnum + "|");
            }
            return stringBuilder.ToString();
        }

        public static string GetDescriptionNamesFromEnum()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (EUnitOfMeasurement unitEnum in Enum.GetValues(typeof(EUnitOfMeasurement)))
            {
                stringBuilder.Append(unitEnum.GetDescription() + "|");
            }
            return stringBuilder.ToString();
        }
    }

    public static class EnumExtensionMethods
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

    }
}
