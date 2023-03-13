public interface IItemManager
{
    Weapon GetWearedWeapon();

    void SetWearedWeapon(Weapon weapon);

}

public class ItemManager : IItemManager
{
    private readonly WeaponPack _weaponPack;

    private Weapon _wearedWeapon;

    ItemManager(WeaponPack weaponPack)
    {
        _weaponPack = weaponPack;
        
        
        SetWearedWeapon(weaponPack.weapons[0]);
    }


    public Weapon GetWearedWeapon()
    {
        return _wearedWeapon;
    }

    public void SetWearedWeapon(Weapon weapon)
    {
        _wearedWeapon = weapon;
    }
}
