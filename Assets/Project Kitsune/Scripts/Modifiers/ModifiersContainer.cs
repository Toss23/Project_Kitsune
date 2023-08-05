using System.Collections.Generic;

public class ModifiersContainer
{
    private AttributesContainer _attributesContainer;
    private AbilityModifier[] _abilityModifiers;

    public AbilityModifier[] AbilityModifiers => _abilityModifiers;

    public ModifiersContainer(AttributesContainer attributesContainer)
    {
        _attributesContainer = attributesContainer;

        _abilityModifiers = new AbilityModifier[5];
        for (int i = 0; i < _abilityModifiers.Length; i++)
        {
            _abilityModifiers[i] = new AbilityModifier(i);
        }
    }

    public void Add(AbilityModifier abilityModifier)
    {
        _abilityModifiers[abilityModifier.AbilityIndex].Add(abilityModifier);
    }

    public void Remove(AbilityModifier abilityModifier)
    {
        _abilityModifiers[abilityModifier.AbilityIndex].Subtract(abilityModifier);
    }

    public void Add(AttributeModifier attributeModifier)
    {
        _attributesContainer.Life.AddMaximum(attributeModifier.Life);
        _attributesContainer.Life.Regeneration.Add(attributeModifier.LifeRegeneration);
        _attributesContainer.MagicShield.AddMaximum(attributeModifier.MagicShield);
        _attributesContainer.MagicShield.Regeneration.Add(attributeModifier.MagicShieldRegeneration);
        _attributesContainer.Armour.Add(attributeModifier.Armour);
        _attributesContainer.Damage.Add(attributeModifier.Damage);
        _attributesContainer.Damage.CritChance.Add(attributeModifier.CritChance);
        _attributesContainer.Damage.CritMultiplier.Add(attributeModifier.CritMultiplier);
        _attributesContainer.Movespeed.Add(attributeModifier.Movespeed);
    }

    public void Remove(AttributeModifier attributeModifier)
    {
        _attributesContainer.Life.SubtractMaximum(attributeModifier.Life);
        _attributesContainer.Life.Regeneration.Subtract(attributeModifier.LifeRegeneration);
        _attributesContainer.MagicShield.SubtractMaximum(attributeModifier.MagicShield);
        _attributesContainer.MagicShield.Regeneration.Subtract(attributeModifier.MagicShieldRegeneration);
        _attributesContainer.Armour.Subtract(attributeModifier.Armour);
        _attributesContainer.Damage.Subtract(attributeModifier.Damage);
        _attributesContainer.Damage.CritChance.Subtract(attributeModifier.CritChance);
        _attributesContainer.Damage.CritMultiplier.Subtract(attributeModifier.CritMultiplier);
        _attributesContainer.Movespeed.Subtract(attributeModifier.Movespeed);
    }
}