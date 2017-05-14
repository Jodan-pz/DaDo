using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DaDo
{
    class SampleYamlProgram
    {
        public class DeserializeObjectGraph
        {
            public static void Test()
            {
                var input = new StringReader(Document);
                var deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(new CamelCaseNamingConvention())
                                        .Build();

                // var order = deserializer.Deserialize<Order>(input);

                var ser = new SerializerBuilder()
                                .WithNamingConvention(new CamelCaseNamingConvention())
                                .Build();

                Order order = new Order();
                order.Receipt = "My Order!";
                order.ShipTo = new Address
                {
                    City = "Bettola",
                    State = "Italy",
                    Street = "Nenni 3/a"
                };
                order.SpecialDelivery = "milan";
                order.Customer = new Customer
                {
                    Given = "Me",
                    Family = "My"
                };
                order.Date = DateTime.Now;
                order.Items = new List<OrderItem>();
                order.Items.Add(new OrderItem
                {
                    Descrip ="A",
                    Partno ="#2",
                    Price = 123.32M,
                    Quantity = 12
                });
                order.Items.Add(new OrderItem
                {
                    Descrip ="AV",
                    Partno ="#23",
                    Price = 1.32M,
                    Quantity = 1
                });
                order.BillTo = order.ShipTo; 
                File.WriteAllText("meh.txt", ser.Serialize(order));
                
                // Console.WriteLine("Order");
                // Console.WriteLine("-----");
                // Console.WriteLine();
                // foreach (var item in order.Items)
                // {
                //     Console.WriteLine("{0}\t{1}\t{2}\t{3}", item.Partno, item.Quantity, item.Price, item.Descrip);
                // }
                // Console.WriteLine();

                // Console.WriteLine("Shipping");
                // Console.WriteLine("--------");
                // Console.WriteLine();
                // Console.WriteLine(order.ShipTo.Street);
                // Console.WriteLine(order.ShipTo.City);
                // Console.WriteLine(order.ShipTo.State);
                // Console.WriteLine();

                // Console.WriteLine("Billing");
                // Console.WriteLine("-------");
                // Console.WriteLine();
                // if (order.BillTo == order.ShipTo)
                // {
                //     Console.WriteLine("*same as shipping address*");
                // }
                // else
                // {
                //     Console.WriteLine(order.ShipTo.Street);
                //     Console.WriteLine(order.ShipTo.City);
                //     Console.WriteLine(order.ShipTo.State);
                // }
                // Console.WriteLine();

                // Console.WriteLine("Delivery instructions");
                // Console.WriteLine("---------------------");
                // Console.WriteLine();
                // Console.WriteLine(order.SpecialDelivery);
            }

            public class Order
            {
                public string Receipt { get; set; }
                public DateTime Date { get; set; }
                public Customer Customer { get; set; }
                public List<OrderItem> Items { get; set; }

                [YamlMember(Alias = "bill-to")]
                public Address BillTo { get; set; }

                [YamlMember(Alias = "ship-to")]
                public Address ShipTo { get; set; }

                public string SpecialDelivery { get; set; }
            }

            public class Customer
            {
                public string Given { get; set; }
                public string Family { get; set; }
            }

            public class OrderItem
            {
                [YamlMember(Alias = "part_no")]
                public string Partno { get; set; }
                public string Descrip { get; set; }
                public decimal Price { get; set; }
                public int Quantity { get; set; }
            }

            public class Address
            {
                public string Street { get; set; }
                public string City { get; set; }
                public string State { get; set; }
            }

            private const string Document = @"---
            receipt:    Oz-Ware Purchase Invoice
            date:        2007-08-06
            customer:
                given:   Dorothy
                family:  Gale

            items:
                - part-no:   A4786
                  descrip:   Water Bucket (Filled)
                  price:     1.47
                  quantity:  4

                - part-no:   E1628
                  descrip:   High Heeled ""Ruby"" Slippers
                  price:     100.27
                  quantity:  1

            bill-to:  &id001
                street: |-
                        123 Tornado Alley
                        Suite 16
                city:   East Westville
                state:  KS

            ship-to:  *id001

            specialDelivery: >
                Follow the Yellow Brick
                Road to the Emerald City.
                Pay no attention to the
                man behind the curtain.
...";
        }
    }
}
