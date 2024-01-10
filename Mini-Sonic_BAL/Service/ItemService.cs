﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using Mini_Sonic_DAL.Repositories;
using System.Data;
namespace Mini_Sonic.Service
{
    public class ItemService  :IGenericRepository<Item>
    {

        private readonly ItemManager _itemManager;

        public ItemService(IRepository<Item> itemRepository)
        {
            _itemManager = new ItemManager(itemRepository);
        }

        public OperationResult Add(Item entity)
        {
            var Result=   _itemManager.Add(entity);
            if (Result == OperationResult.Success)
            {
                // If operation was successful, return the entity
                return OperationResult.Success;
            }
            else
            {
                // If operation failed, you might want to handle it accordingly
                // For now, let's return null, but you can modify this based on your needs
                return OperationResult.Fail;
            }
        }

        public void Delete(int id)
        {
            _itemManager.Delete(id);
        }

        public List<Item> GetAll()
        {
            return _itemManager.GetAll();
        }

        public Item GetById(int id)
        {
            return _itemManager.GetById(id);
        }

        public List<Item> GetOperationDetailsByOperationId(int operationId)
        {
            throw new NotImplementedException();
        }

        public OperationResult Update(Item entity)
        {
            var Result = _itemManager.Update(entity);
            if (Result == OperationResult.Success)
            {
                // If operation was successful, return the entity
                return OperationResult.Success;
            }
            else
            {
                // If operation failed, you might want to handle it accordingly
                // For now, let's return null, but you can modify this based on your needs
                return OperationResult.Fail;
            }
        }
    }
}