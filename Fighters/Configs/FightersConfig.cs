using Fighters.Models.Armors;
using Fighters.Models.FighterClasses;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utils;

namespace Fighters.Configs;

public struct NameAndClassPair( string name, IFighterClasses fighter )
{
    public string ClassName = name;
    public IFighterClasses FighterClass = fighter;
}

public static class FightersConfig
{
    public enum AvailiableClasses
    {
        AntiMage,
        Fool,
        MegaKnight,
        Sparki,
        WizardFromClashRoyale
    }
    public readonly static Dictionary<AvailiableClasses, string> AvailiableClassesNames = new()
    {
        {AvailiableClasses.MegaKnight, "МЕГАРыдцорь" },
        {AvailiableClasses.Fool, "Просто чудоковатый парень" },
        {AvailiableClasses.AntiMage, "Антимаг" },
        {AvailiableClasses.Sparki, "СПАРКИ" },
        {AvailiableClasses.WizardFromClashRoyale, "Колдун" },
    };
    public readonly static Dictionary<AvailiableClasses, IFighterClasses> AvailaibleClass = new()
    {
        {AvailiableClasses.MegaKnight, new MegaKnight() },
        {AvailiableClasses.Fool, new Fool() },
        {AvailiableClasses.AntiMage, new AntiMage() },
        {AvailiableClasses.Sparki, new Sparki() },
        {AvailiableClasses.WizardFromClashRoyale, new WizardFromClashRoyale() },
    };
    public static List<AvailiableClasses> GetAvailaibleClassesList()
    {
        return EnumsToList.EnumToList<AvailiableClasses>();
    }


    public enum AvailiableRaces
    {
        Human,
        Goblin,
        Undead,
        WildAnimal
    }
    public readonly static Dictionary<AvailiableRaces, string> AvailiableRacesNames = new()
    {
        {AvailiableRaces.Human, "Человек" },
        {AvailiableRaces.Goblin, "Гоблинц" },
        {AvailiableRaces.Undead, "Нежить" },
        {AvailiableRaces.WildAnimal, "Дичайший зверюга" },
    };
    public readonly static Dictionary<AvailiableRaces, IRace> AvailRaceByEnum = new()
    {
        {AvailiableRaces.Human, new Human() },
        {AvailiableRaces.Goblin, new Goblin() },
        {AvailiableRaces.Undead, new Undead() },
        {AvailiableRaces.WildAnimal, new WildAnimal() },
    };
    public static List<AvailiableRaces> GetAvailaibleRacesList()
    {
        return EnumsToList.EnumToList<AvailiableRaces>();
    }


    public enum AvailiableArmors
    {
        NoArmor,
        BeautifulMaleTorso,
        MegaKnightArmor,
        MagicShield,
        GoldArmorFromMinecraft,
    }
    public readonly static Dictionary<AvailiableArmors, string> AvailiableArmorsNames = new()
    {
        {AvailiableArmors.NoArmor, "Без брони" },
        {AvailiableArmors.BeautifulMaleTorso, "Безупречный мужской торс" },
        {AvailiableArmors.MegaKnightArmor, "Броня мегарыдцоря" },
        {AvailiableArmors.MagicShield, "Магический щит" },
        {AvailiableArmors.GoldArmorFromMinecraft, "Золотая броня из Майнкрафта" },
    };
    public readonly static Dictionary<AvailiableArmors, IArmor> AvailArmorByEnum = new()
    {
        {AvailiableArmors.NoArmor, new NoArmor() },
        {AvailiableArmors.BeautifulMaleTorso,new BeautifulMaleTorso()  },
        {AvailiableArmors.MegaKnightArmor, new MegaKnightArmor() },
        {AvailiableArmors.MagicShield, new MagicShield() },
        {AvailiableArmors.GoldArmorFromMinecraft, new GoldArmorFromMinecraft() },
    };
    public static List<AvailiableArmors> GetAvailaibleArmorsList()
    {
        return EnumsToList.EnumToList<AvailiableArmors>();
    }


    public enum AvailiableWeapons
    {
        Firsts,
        BFG,
        RandomGun,
        StaffOfWizardy,
        TheSpearOfMars,
        
    }
    public readonly static Dictionary<AvailiableWeapons, string> AvailiableWeaponsNames = new()
    {
        {AvailiableWeapons.Firsts, "Кулаки" },
        {AvailiableWeapons.BFG, "Большая ***** пушка" },
        {AvailiableWeapons.RandomGun, "Пушка везунчика" },
        {AvailiableWeapons.StaffOfWizardy, "Посох магии(из доты)" },
        {AvailiableWeapons.TheSpearOfMars, "Копье марса(из доты)" },
    };
    public readonly static Dictionary<AvailiableWeapons, IWeapon> AvailWeaponByEnum = new()
    {
        {AvailiableWeapons.Firsts, new Firsts() },
        {AvailiableWeapons.BFG, new BFG() },
        {AvailiableWeapons.RandomGun, new RandomGun()},
        {AvailiableWeapons.StaffOfWizardy,  new StaffOfWizardy() },
        {AvailiableWeapons.TheSpearOfMars,  new TheSpearofMars() },
    };
    public static List<AvailiableWeapons> GetAvailaibleWeaponsList()
    {
        return EnumsToList.EnumToList<AvailiableWeapons>();
    }
}
