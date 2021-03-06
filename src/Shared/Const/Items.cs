﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elania.Shared.Const
{
	public enum InventoryCategory
	{
		Weapon = 1,
		Armor = 2,
		SubWeapon = 3,
		Costume = 4,
		Accessory = 5,
		Consumable = 6,
		Gem = 7,
		Material = 8,
		Card = 9,
		Collection = 10,
		Book = 11,
		Quest = 12,
		PetWeapon = 13,
		PetArmor = 14,
		Unused = 15,
		Etc = 16,
		Premium = 17,
	}

	public static class InventoryCategoryExtension
	{
		/// <summary>
		/// Returns inventory index for category and category index.
		/// </summary>
		/// <remarks>
		/// Categories are offset by 5000 times their id. We start each
		/// category on index 1 in that range. For example, the first
		/// weapon has index 5001, the second one 5002, the fifth armor
		/// has 10005, and so on.
		/// </remarks>
		/// <param name="cat">Category the index is in.</param>
		/// <param name="index">The index of the item in the category.</param>
		/// <returns></returns>
		public static int GetIndex(this InventoryCategory cat, int index)
		{
			return (int)cat * 5000 + index + 1;
		}
	}

	/// <remarks>
	/// We're using this enum to save which slot an item is equipped on,
	/// should the values change, the values in the database would be
	/// incorrect. Should this enum change again, we should think about
	/// saving the slots as string or creating an intermediary enum.
	/// </remarks>
	public enum EquipSlot : byte
	{
		HairAccessory,
		SubsidiaryAccessory,
		Hair, // [i11025 (2016-02-26)]
		_Outer1,
		Top,
		Costume,
		Shoes,
		_Helmet,
		Armband,
		LeftHand,
		RightHand,
		Gloves,
		_Ring1,
		_Ring2,
		_Outer2,
		Pants,
		_Ring3,
		_Ring4,
		Bracelet1,
		Bracelet2,
		Necklace,
	}

	public enum InventoryItemRemoveMsg : byte
	{
		Given = 0, // 0,6,10
		Sold = 1,
		PaidWith = 2,
		Destroyed = 3,
		Equipped = 5,
		Used = 7, // 7,8
	}

	public enum InventoryType : byte
	{
		Inventory = 0,
		Warehouse = 1,
	}

	public enum InventoryAddType : byte
	{
		Buy = 1,
		Sell = 2,
		PickUp = 3, // ?
		NotNew = 4,
	}

	public static class Items
	{
		/// <summary>
		/// Ammount of equip slots.
		/// </summary>
		/// <remarks>
		/// [i11025 (2016-02-26)] 20->21, Hair slot?
		/// </remarks>
		public const int EquipSlotCount = 22;

		/// <summary>
		/// Ids of the items equipped by default.
		/// (Literally empty items, NoHat, NoWeapon, etc.)
		/// </summary>
		public static readonly int[] DefaultItems = new int[EquipSlotCount]
		{
			(int)DefaultEquipment.NoHat,
			(int)DefaultEquipment.NoHat,
			(int)DefaultEquipment.NoHat,
			(int)DefaultEquipment.LightArmor,
			(int)DefaultEquipment.NoGloves,
			(int)DefaultEquipment.NoBoots,
			(int)DefaultEquipment.NoHelmet,
			(int)DefaultEquipment.NoArmband,
			(int)DefaultEquipment.NoWeapon,
			(int)DefaultEquipment.NoWeapon,
			(int)DefaultEquipment.NoHat,
			(int)DefaultEquipment.NoRing,
			(int)DefaultEquipment.NoRing,
			(int)DefaultEquipment.NoOuter,
			(int)DefaultEquipment.LightPants,
			(int)DefaultEquipment.NoRing,
			(int)DefaultEquipment.NoRing,
			(int)DefaultEquipment.NoRing,
			(int)DefaultEquipment.NoRing,
			(int)DefaultEquipment.NoNeck,
			(int)DefaultEquipment.NoHat,
			(int)DefaultEquipment.NoOuter
		};
	}

	public enum DefaultEquipment : int
	{
		NoHat = 2,
		NoOuter = 4,
		NoGloves = 6,
		NoBoots = 7,
		NoRing = 9,
		NoNeck = 10,
		NoHelmet = 10000,
		NoArmband = 11000,
		LightPants = 521101,
		LightArmor = 531101,
		NoWeapon = 9999996,
	}

	public enum InventoryOrder : byte
	{
		Price = 2,
		Grade = 0,
		Weight = 1,
		Name = 3,

		Id = 0xFF,
	}

	/// <summary>
	/// Ids of commonly used items.
	/// </summary>
	public static class ItemId
	{
		public const int Silver = 900011;
	}
}
