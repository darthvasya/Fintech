using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Fintech
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerService : ICustomerService
    {
        DB_9FFBF4_FintechEntities context = new DB_9FFBF4_FintechEntities();

        public string DoWork()
        {
            //string da = context.Users.Where(p => p.uid == 1).FirstOrDefault().login.ToString();
            return "da";
        }

        public int Register(Users newUser)
        {
            List<Users> users = context.Users.Where(p => p.login == newUser.login).ToList();
            if (users.Count > 0)
            {
                return -1;
            }
            else
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return newUser.uid;
            }

        }

        public bool Login(LogInfo logInfo)
        {
            Users tempUser;
            if (((tempUser = context.Users.Where(p => p.uid == logInfo.id).FirstOrDefault()) != null)
                && (tempUser.password == logInfo.password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AddCard(Cards newCard)
        {
            if ((newCard.cardKey.Length == 3) && (newCard.cardNumber.Length == 16) && (newCard.uid != -1))
            {
                context.Cards.Add(newCard);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShopInfo GetShopInfo(string id)
        {
            ShopInfo result = null;
            try
            {
                int int_id = Int32.Parse(id);
                ObjectInfo objectInfo = context.ObjectInfo.Where(p => p.Id == int_id).FirstOrDefault();
                Image objectImage = context.Image.Where(p => p.Id == objectInfo.IdImage).FirstOrDefault();
                if (objectInfo == null)
                    return result;
                result = new ShopInfo();
                result.Name = objectInfo.Name;
                result.Address = objectInfo.Address;
                result.Description = objectInfo.Description;
                if (objectImage != null)
                    result.ImageUrl = objectImage.ImageString;
                else
                    result.ImageUrl = "";
                return result;
            }
            catch (Exception exc)
            {
                return result;
            }
        }

        public List<CardInfo> GetUserCards(string id)
        {
            try
            {
                int int_id = Int32.Parse(id);
                List<Cards> cards = context.Cards.Where(p => p.uid == int_id).ToList();
                List<CardInfo> result = new List<CardInfo>();
                foreach (Cards card in cards)
                {
                    CardInfo cardInfo = new CardInfo();
                    cardInfo.cardNumber = card.cardNumber;
                    cardInfo.id = card.id;
                    result.Add(cardInfo);
                }
                return result;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public List<Category> GetShopCats(string shopId)
        {
            try
            {
                int int_shopId = Int32.Parse(shopId);
                List<CategoryInfo> cats = context.CategoryInfo.Where(p => p.IdObject == int_shopId).ToList();
                List<Category> result = new List<Category>();
                foreach (CategoryInfo cat in cats)
                {
                    Image image = context.Image.Where(p => p.Id == cat.IdImage).FirstOrDefault();
                    Category category = new Category();
                    category.id = cat.Id;
                    category.Description = cat.Description;
                    if (image != null)
                        category.ImageUrl = image.ImageString;
                    else
                        category.ImageUrl = "";
                    category.Name = cat.Name;
                    result.Add(category);
                }
                return result;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public List<Good> GetCatGoods(string shopId, string catId)
        {
            try
            {
                int int_shopId = Int32.Parse(shopId);
                int int_catId = Int32.Parse(catId);

                List<ProductInfo> goods = context.ProductInfo.Where(p => p.IdCategory == int_catId).ToList();
                List<Good> result = new List<Good>();
                foreach (ProductInfo productInfo in goods)
                {
                    Image image = context.Image.Where(p => p.Id == productInfo.IdImage).FirstOrDefault();
                    Good good = new Good();
                    good.ImageUrl = image.ImageString;
                    good.CatId = int_catId;
                    good.Currency = productInfo.Currency;
                    good.Description = productInfo.Description;
                    good.Id = productInfo.Id;
                    good.Price = productInfo.Price;
                    good.Name = productInfo.Name;
                    good.ShopId = productInfo.IdObject;
                    result.Add(good);
                }
                return result;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public Good GetGood(string shopId, string catId, string goodId)
        {
            try
            {
                int int_shopId = Int32.Parse(shopId);
                int int_catId = Int32.Parse(catId);
                int int_goodId = Int32.Parse(goodId);

                ProductInfo good = context.ProductInfo.Where(p => p.IdCategory == int_catId).FirstOrDefault();
                Good result = new Good();
                Image image = context.Image.Where(p => p.Id == good.IdImage).FirstOrDefault();
                result.ImageUrl = image.ImageString;
                result.CatId = int_catId;
                result.Currency = good.Currency;
                result.Description = good.Description;
                result.Id = good.Id;
                result.Price = good.Price;
                result.Name = good.Name;
                result.ShopId = good.IdObject;
                return result;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public string MakeOrder(Order order)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.IdObject = order.ShopId;
            orderInfo.AcceptStatus = 0;
            orderInfo.DeliveryStatus = 0;
            orderInfo.PaymentStatus = 0;
            orderInfo.CardId = order.CardId;
            orderInfo.GoodsId = formStringWithGoodsId(order.GoodsId);
            int tempOrderId = order.GoodsId[0];
            ProductInfo tempGood = context.ProductInfo.Where(p => p.Id == tempOrderId).FirstOrDefault();
            orderInfo.Currency = tempGood.Currency;
            orderInfo.CreationTime = order.OrderTime;
            orderInfo.DeliveryTime = "";
            context.OrderInfo.Add(orderInfo);
            context.SaveChanges();
            sendOrderChangeSignal(orderInfo.Id);
            return "Ваш заказ поставлен в очередь.";
        }

        private string formStringWithGoodsId(List<int> ids)
        {
            string result = "";
            foreach (int id in ids)
                result += id.ToString() + " ";
            return result;
        }

        public void SayAboutOrderAccept(string id)
        {
            int int_id = Int32.Parse(id);
            OrderInfo orderInfo = context.OrderInfo.Where(p => p.Id == int_id).FirstOrDefault();
            orderInfo.AcceptStatus = 1;
            orderInfo.PaymentStatus = 1;
            context.SaveChanges();
            sendOrderChangeSignal(int_id);
        }

        public string GetCatNameById(string id)
        {
            int int_id = Int32.Parse(id);
            return context.CategoryInfo.Where(p => p.Id == int_id).FirstOrDefault().Name;
        }

        public void SayAboutGoodDelivery(string id)
        {
            int int_id = Int32.Parse(id);
            OrderInfo orderInfo = context.OrderInfo.Where(p => p.Id == int_id).FirstOrDefault();
            orderInfo.DeliveryStatus = 1;
            context.SaveChanges();
            sendOrderChangeSignal(orderInfo.Id);
        }

        public string GetOrderStatus(string orderId, string operationId)
        {
            int int_orderId = Int32.Parse(orderId);
            OrderInfo orderInfo = context.OrderInfo.Where((p => p.Id == int_orderId)).FirstOrDefault();
            switch (Int32.Parse(operationId))
            {
                case 0:
                    if (orderInfo.AcceptStatus == 1)
                        return "Заказ одобрен магазином";
                    else
                        return "Заказ не одобрен магазином";

                case 1:
                    if (orderInfo.AcceptStatus == 1)
                        return "Заказ оплачен";
                    else
                        return "Заказ не оплачен";

                case 2:
                    if (orderInfo.AcceptStatus == 1)
                        return "Заказ доставлен";
                    else
                        return "Заказ не доставлен";
            }
            return "Неверный запрос";
        }

        private void sendOrderChangeSignal(int orderId)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            try
            {
                client = new TcpClient("localhost", 8800);
                stream = client.GetStream();
                byte[] data = BitConverter.GetBytes(orderId);
                stream.Write(data, 0, data.Length);
            }
            finally
            {
                stream.Flush();
                stream.Close();
                client.Close();
            }
        }

        public List<Shop> GetShopList()
        {
            List<ObjectInfo> shops = context.ObjectInfo.ToList();
            List<Shop> result = new List<Shop>();
            foreach (ObjectInfo objectInfo in shops)
            {
                Image image = context.Image.Where(p => p.Id == objectInfo.IdImage).FirstOrDefault();
                Shop shop = new Shop();
                shop.address = objectInfo.Address;
                shop.description = objectInfo.Description;
                shop.id = objectInfo.Id;
                shop.imageUrl = image.ImageString;
                shop.name = objectInfo.Name;
                result.Add(shop);
            }
            return result;
        }
    }
}
