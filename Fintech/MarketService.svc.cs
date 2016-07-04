using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.IO;

namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MarketService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MarketService.svc or MarketService.svc.cs at the Solution Explorer and start debugging.
    public class MarketService : IMarketService
    {
        DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();
        // РЕГИСТРАЦИЯ ОБЪЕКТА
        public string CreateObject(string Card, string Password, string Name)
        {
            string result = "Неизвестная ошибка.";
            if (MarketMethod.CheckPassword(Password) == true)
            {
                try
                {
                    ObjectInfo OI = new ObjectInfo();
                    OI.Card = Card;
                    OI.Password = Password;
                    OI.Name = Name;
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
        public string ChangeObject(string Card, string Password, string PasswordNew, string Name, string Address, string Description, int IdImage)
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
                    OI.IdImage = IdImage;
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
        public string AddCategory(int IdObject, string Name, int IdImage, string Description)
        {
            string result = "ok";
            try
            {
                CategoryInfo CI = new CategoryInfo();
                CI.IdObject = IdObject;
                CI.Name = Name;
                CI.IdImage = IdImage;
                CI.Description = Description;
                context.CategoryInfo.Add(CI);
                context.SaveChanges();
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ИЗМЕНЕНИЕ КАТЕГОРИИ
        public string ChangeCategory(int Id, int IdObject, string Name, int IdImage, string Description)
        {
            string result = "ok";
            try
            {
                CategoryInfo CI = context.CategoryInfo.Where(p => p.IdObject == IdObject).Where(p => p.Id == Id).FirstOrDefault();
                if (CI != null)
                {
                    // изменяем
                    CI.Name = Name;
                    CI.IdImage = IdImage;
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
        public string AddProduct(int IdObject, int IdCategory, string Name, decimal Price, string Currency, int IdImage, string Description)
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
                PI.IdImage = IdImage;
                PI.Description = Description;
                context.ProductInfo.Add(PI);
                context.SaveChanges();
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }
        // ИЗМЕНЕНИЕ ТОВАРА
        public string ChangeProduct(int Id, int IdObject, int IdCategory, string Name, decimal Price, string Currency, int IdImage, string Description)
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
                    PI.IdImage = IdImage;
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
        private string ServerDirectory = "h:/root/home/vasya18-001/www/site1";
        public Image LoadImage(int Id, byte[] Buffer, string Extension)
        {
            Image result = null;
            if (Extension == "jpg" || Extension == "jpeg" || Extension == "png")
            {
                try
                {
                    Image Img = null;
                    if (Id > 0)
                    {
                        Img = context.Image.Where(p => p.Id == Id).FirstOrDefault();
                    }
                    if (Img == null)
                    { // создаём
                        Img = new Image();
                        int ImageId = 1; try { ImageId = context.Image.ToList().LastOrDefault().Id + 1; } catch { }
                        Img.ImageString = "/ImageStorage/" + ImageId + "." + Extension;
                        Img.Ext = Extension;
                        context.Image.Add(Img);
                        context.SaveChanges();
                    }
                    // путь
                    if (Directory.Exists(ServerDirectory + "\\ImageStorage") == false)
                    { Directory.CreateDirectory(ServerDirectory + "\\ImageStorage"); }
                    // создаём файл
                    File.WriteAllBytes(ServerDirectory + "\\" + Img.ImageString, Buffer);
                    result = Img;
                }
                catch { }// (Exception E) { result = E.Message; }
            }
            return result;
        }
        // ПОЛУЧЕНИЕ ФОТО
        public Image GetImage(int Id)
        {
            Image Img = null;
            try { Img = context.Image.Where(p => p.Id == Id).FirstOrDefault(); } catch { }
            return Img;
        }
        // УДАЛЕНИЕ ФОТО
        public string RemoveImage(int Id)
        {
            string result = "ok";
            try { context.Image.Remove(GetImage(Id)); } catch (Exception E) { result = E.Message; }
            return result;
        }

        // ПОЛУЧЕНИЕ ВСЕХ ЗАКАЗОВ
        public List<OrderInfo> GetAllOrder(int IdObject)
        {
            List<OrderInfo> OIList = null;
            try { OIList = context.OrderInfo.Where(p => p.IdObject == IdObject).ToList(); } catch { }
            return OIList;
        }
        // ИЗМЕНЕНИЕ ЗАКАЗА
        public string ChangeOrder(int Id, int IdObject, byte AcceptStatus, byte DeliveryStatus, string DeliveryTime)
        {
            string result = "ok";
            try
            {
                OrderInfo OI = context.OrderInfo.Where(p => p.IdObject == IdObject).Where(p => p.Id == Id).FirstOrDefault();
                if (OI != null)
                {
                    // изменяем
                    OI.AcceptStatus = AcceptStatus;
                    OI.DeliveryStatus = DeliveryStatus;
                    OI.DeliveryTime = DeliveryTime;
                    context.SaveChanges();
                }
                else { result = "Заказ не найден."; }
            }
            catch (Exception E) { result = E.Message; }
            return result;
        }


    }
}
