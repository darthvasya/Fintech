using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.IO;
using System.Net.Sockets;

namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MarketService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MarketService.svc or MarketService.svc.cs at the Solution Explorer and start debugging.
    public class MarketService : IMarketService
    {
        DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();
        // РЕГИСТРАЦИЯ ОБЪЕКТА
        public string CreateObject(string card, string password, string name)
        {
            string result = "Неизвестная ошибка.";
            if (MarketMethod.CheckPassword(password) == true)
            {
                try
                {
                    ObjectInfo OI = new ObjectInfo();
                    OI.Card = card;
                    OI.Password = password;
                    OI.Name = name;
                    context.ObjectInfo.Add(OI);
                    context.SaveChanges();
                    result = "ok";
                }
                catch (Exception E) { result = E.Message; }
            }
            else { result = "Неверный пароль."; }
            return result;
        }
        // ПРОВЕРКА ЛОГИНА И ПАРОЛЯ
        public string CheckObject(string Card, string Password)
        {
            string answer = "Неизвестная ошибка.";
            if (MarketMethod.CheckPassword(Password) == true)
            {
                try
                {
                    ObjectInfo OI = context.ObjectInfo.Where(p => p.Card == Card).FirstOrDefault();
                    if (OI != null)
                    {
                        if (OI.Password == Password) { answer = "ok"; }
                        else { answer = "Неверный пароль."; }
                    }
                    else { answer = "Номер карты не найден."; }
                }
                catch (Exception E) { answer = E.Message; }
            }
            else { answer = "Неверный пароль."; }
            return answer;
        }
        // ИЗМЕНЕНИЕ ОБЪЕКТА
        public string ChangeObject(string Card, string Password, string PasswordNew, string Name, string Address, string Description, string Image)
        {
            string result = "ok";
            try
            {
                ObjectInfo OI = context.ObjectInfo.Where(p => p.Card == Card).Where(p => p.Password == Password).FirstOrDefault();
                if (OI != null)
                {
                    // изменяем
                    if (PasswordNew != null) if (MarketMethod.CheckPassword(PasswordNew)) { OI.Password = PasswordNew; }
                    OI.Name = Name;
                    OI.Address = Address;
                    OI.Description = Description;
                    OI.IdImage = 0; // ПОТОМ СДЕЛАТЬ ФОТКУ
                    context.SaveChanges();
                }
                else { result = "Категория не найдена."; }
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ДАННЫЕ ОБЪЕКТА
        public ObjectInfo GetObject(string Card, string Password)
        {
            ObjectInfo OI = null;
            if (MarketMethod.CheckPassword(Password) == true)
            {
                try
                {
                    ObjectInfo OItemp = context.ObjectInfo.Where(p => p.Card == Card).FirstOrDefault();
                    if (OItemp != null)
                        if (OItemp.Password == Password)
                            OI = OItemp;
                }
                catch { }
            }
            return OI;
        }
        // ДОБАВЛЕНИЕ КАТЕГОРИИ
        public string AddCategory(int IdObject, string Name, string Image, string Description)
        {
            string result = "ok";
            try
            {
                CategoryInfo CI = new CategoryInfo();
                CI.IdObject = IdObject;
                CI.Name = Name;
                CI.IdImage = 0; // ПОТОМ СДЕЛАТЬ ФОТКУ
                CI.Description = Description;
                context.CategoryInfo.Add(CI);
                context.SaveChanges();
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ИЗМЕНЕНИЕ КАТЕГОРИИ
        public string ChangeCategory(int Id, int IdObject, string Name, string Image, string Description)
        {
            string result = "ok";
            try
            {
                CategoryInfo CI = context.CategoryInfo.Where(p => p.IdObject == IdObject).Where(p => p.Id == Id).FirstOrDefault();
                if (CI != null)
                {
                    // изменяем
                    CI.Name = Name;
                    CI.IdImage = 0; // ПОТОМ СДЕЛАТЬ ФОТКУ
                    CI.Description = Description;
                    context.SaveChanges();
                }
                else { result = "Категория не найдена."; }
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // УДАЛЕНИЕ КАТЕГОРИИ
        public string RemoveCategory(int Id, int IdObject)
        {
            string result = "ok";
            try
            {
                CategoryInfo CI = context.CategoryInfo.Where(p => p.IdObject == IdObject).Where(p => p.Id == Id).FirstOrDefault();
                if (CI != null)
                {
                    // удаляем
                    context.CategoryInfo.Remove(CI);
                    context.SaveChanges();
                }
                else { result = "Категория не найдена."; }
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ДАННЫЕ ВСЕХ КАТЕГОРИЙ
        public List<CategoryInfo> GetAllCategory(int IdObject)
        {
            List<CategoryInfo> CIList = null;
            try { CIList = context.CategoryInfo.Where(p => p.IdObject == IdObject).ToList(); } catch { }
            return CIList;
        }
        // ДОБАВЛЕНИЕ ТОВАРА
        public string AddProduct(int IdObject, int IdCategory, string Name, decimal Price, string Currency, string Image, string Description)
        {
            string result = "ok";
            try
            {
                ProductInfo PI = new ProductInfo();
                PI.IdObject = IdObject;
                PI.IdCategory = IdCategory;
                PI.Name = Name;
                PI.Price = Price;
                PI.Currency = Currency;
                PI.IdImage = 0; // ПОТОМ СДЕЛАТЬ ФОТКУ
                PI.Description = Description;
                context.ProductInfo.Add(PI);
                context.SaveChanges();
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ИЗМЕНЕНИЕ ТОВАРА
        public string ChangeProduct(int Id, int IdObject, int IdCategory, string Name, decimal Price, string Currency, string Image, string Description)
        {
            string result = "ok";
            try
            {
                ProductInfo PI = context.ProductInfo.Where(p => p.IdObject == IdObject).Where(p => p.Id == Id).FirstOrDefault();
                if (PI != null)
                {
                    // изменяем
                    PI.IdCategory = IdCategory;
                    PI.Name = Name;
                    PI.Price = Price;
                    PI.Currency = Currency;
                    PI.IdImage = 0; // ПОТОМ СДЕЛАТЬ ФОТКУ
                    PI.Description = Description;
                    context.SaveChanges();
                }
                else { result = "Продукт не найден."; }
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // УДАЛЕНИЕ ТОВАРА
        public string RemoveProduct(int Id, int IdObject)
        {
            string result = "ok";
            try
            {
                ProductInfo PI = context.ProductInfo.Where(p => p.IdObject == IdObject).Where(p => p.Id == Id).FirstOrDefault();
                if (PI != null)
                {
                    // удаляем
                    context.ProductInfo.Remove(PI);
                    context.SaveChanges();
                }
                else { result = "Продукт не найден."; }
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ДАННЫЕ ВСЕХ ТОВАРОВ
        public List<ProductInfo> GetAllProduct(int IdObject)
        {
            List<ProductInfo> PIList = null;
            try { PIList = context.ProductInfo.Where(p => p.IdObject == IdObject).ToList(); } catch { }
            return PIList;
        }


        // ЗАГРУЗКА ФОТО
        public string LoadImage(int Id, byte[] Buffer, string Extension)
        {
            string result = "Неизвестная ошибка.";
            if (Extension == "jpg" || Extension == "jpeg" || Extension == "png")
            {
                try
                {
                    Image Img = null;
                    if (Id >= 0)
                    {
                        Img = context.Image.Where(p => p.Id == Id).FirstOrDefault();
                    }
                    if (Img == null)
                    { // создаём
                        Img = new Image();
                        int ImageId = 1; try { ImageId = context.Image.LastOrDefault().Id + 1; } catch { }
                        Img.ImageString = "\\ImageStorage\\" + ImageId + "." + Extension;
                        Img.Ext = Extension;
                        context.Image.Add(Img);
                        context.SaveChanges();
                    }
                    // путь
                    if (Directory.Exists("\\ImageStorage") == false) { Directory.CreateDirectory("\\ImageStorage"); }
                    // создаём файл
                    File.WriteAllBytes(Img.ImageString, Buffer);
                    result = "ok";
                }
                catch (Exception E) { result = E.Message; }
            }
            return result;
        }
    }
}
