using Microsoft.EntityFrameworkCore;

namespace Practice_Concurrency
{
    /*
     Разработайте систему онлайн-магазина, где администраторы могут управлять товарами, 
    и пользователи могут просматривать и покупать товары. Используя токены параллелизма в 
    Entity Framework Core, реализуйте механизм обработки конфликтов при одновременном редактировании 
    товаров администраторами.

Требования:

1) Система должна содержать информацию о товарах, включая название, описание, цену и доступное количество.
2) Администраторы могут добавлять, редактировать и удалять товары.
3) Пользователи могут просматривать список товаров, узнавать их характеристики и делать заказы.
4) При одновременном редактировании одного и того же товара двумя или более администраторами необходимо 
    обрабатывать конфликты и сохранять актуальные данные.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    Product? p = db.Products.FirstOrDefault();
                    if (p != null)
                    {
                        p.Name = "Chinese table";
                        db.SaveChanges();
                        Console.WriteLine(p.Name);
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
