﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using OpenNos.Core;
using OpenNos.Data;
using OpenNos.Data.Enums;
using OpenNos.DAL.EF.Helpers;
using OpenNos.DAL.Interface;

namespace OpenNos.DAL.EF
{
    public class ShopItemDao : MappingBaseDao<ShopItem, ShopItemDTO>, IShopItemDAO
    {
        #region Methods

        public DeleteResult DeleteById(int itemId)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    ShopItem item = context.ShopItem.FirstOrDefault(i => i.ShopItemId.Equals(itemId));

                    if (item != null)
                    {
                        context.ShopItem.Remove(item);
                        context.SaveChanges();
                    }

                    return DeleteResult.Deleted;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return DeleteResult.Error;
            }
        }

        public ShopItemDTO Insert(ShopItemDTO item)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    ShopItem entity = Mapper.Map<ShopItem>(item);
                    context.ShopItem.Add(entity);
                    context.SaveChanges();
                    return Mapper.Map<ShopItemDTO>(entity);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public void Insert(List<ShopItemDTO> items)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    foreach (ShopItemDTO item in items)
                    {
                        ShopItem entity = Mapper.Map<ShopItem>(item);
                        context.ShopItem.Add(entity);
                    }
                    context.Configuration.AutoDetectChangesEnabled = true;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public IEnumerable<ShopItemDTO> LoadAll()
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                foreach (ShopItem entity in context.ShopItem)
                {
                    yield return Mapper.Map<ShopItemDTO>(entity);
                }
            }
        }

        public ShopItemDTO LoadById(int itemId)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    return Mapper.Map<ShopItemDTO>(context.ShopItem.FirstOrDefault(i => i.ShopItemId.Equals(itemId)));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public IEnumerable<ShopItemDTO> LoadByShopId(int shopId)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                foreach (ShopItem shopItem in context.ShopItem.Where(i => i.ShopId.Equals(shopId)))
                {
                    yield return Mapper.Map<ShopItemDTO>(shopItem);
                }
            }
        }

        #endregion
    }
}