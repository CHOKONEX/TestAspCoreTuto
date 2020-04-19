using App.Net.Core.Features.Test.Modules.TestEnums.Impl;
using EnumsNET;
using NUnit.Framework;
using System;
using static App.Net.Core.Features.Test.Modules.TestEnums.Impl.EnumModule;

namespace App.Net.Core.Features.Test
{
    [TestFixture]
    public class EnumsTests
    {
        [Test]
        public void should_return_correct_names()
        {
            string enumNames = EnumModule.GetNamesFromEnum();
            Assert.AreEqual("Unity|Milligram|Gram|Kilogram|Liter|", enumNames);
        }

        [Test]
        public void should_return_description_names()
        {
            string enumNames = EnumModule.GetDescriptionNamesFromEnum();
            Assert.AreEqual("UN|MG|G|KG|L|", enumNames);
        }

        [Test]
        public void should_return_description_name()
        {
            string description = ((EUnitOfMeasurement)1).AsString(EnumFormat.Description);
            Assert.AreEqual("UN", description);
        }

        [Test]
        public void should_return_description_name_v2()
        {
            string name = EUnitOfMeasurement.Unity.ToString();
            Assert.AreEqual("Unity", name);

            string description = EUnitOfMeasurement.Unity.AsString(EnumFormat.Description);
            Assert.AreEqual("UN", description);
        }

        [Test]
        public void test_enum_days()
        {
            Days meetingDays = Days.Monday | Days.Wednesday | Days.Friday;
            Console.WriteLine(meetingDays);
            Assert.AreEqual("Monday, Wednesday, Friday", $"{meetingDays}");
            // Output:
            // Monday, Wednesday, Friday
            //*** => ADD

            Days workingFromHomeDays = Days.Thursday | Days.Friday;
            Console.WriteLine($"Join a meeting by phone on {meetingDays & workingFromHomeDays}");
            Assert.AreEqual("Join a meeting by phone on Friday", $"Join a meeting by phone on {meetingDays & workingFromHomeDays}");
            // Output:
            // Join a meeting by phone on Friday
            //*** => Intesection

            bool isMeetingOnTuesday = (meetingDays & Days.Tuesday) == Days.Tuesday;
            Console.WriteLine($"Is there a meeting on Tuesday: {isMeetingOnTuesday}");
            Assert.AreEqual("Is there a meeting on Tuesday: False", $"Is there a meeting on Tuesday: {isMeetingOnTuesday}");

            // Output:
            // Is there a meeting on Tuesday: False

            var a = (Days)37;
            Console.WriteLine(a);
            Assert.AreEqual("Monday, Wednesday, Saturday", $"{a}");
            // Output:
            // Monday, Wednesday, Saturday

            AttackType attackType = AttackType.Melee | AttackType.Fire;
            attackType &= ~AttackType.Fire;
            Console.WriteLine(attackType);
            Assert.AreEqual(AttackType.Melee, attackType);
            //*** => Negate

            attackType = AttackType.Wind ^ AttackType.Fire;
            Console.WriteLine(attackType);
            Assert.AreEqual(AttackType.Melee, attackType);

        }
    }
}