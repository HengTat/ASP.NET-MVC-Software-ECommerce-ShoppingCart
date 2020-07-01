using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingCart_9b.Models;

namespace ShoppingCart_9b.DB
{
    public class DBSeeder
    {
        public DBSeeder(ShoppingContext dbcontext)
        {
            // Product List
            Product product1 = new Product()
            {
                Id = "001",
                Name = "Microsoft Office",
                Description = "Microsoft Office is a collection of office-related applications.Each application serves a unique purpose and offers a specific service to its users. For example, Microsoft Word is used to create documents. Microsoft PowerPoint is used to create presentations.",
                Price = 100,
                ProductPic = "Microsoftoffice.png"
            };
            dbcontext.Add(product1);

            Product product2 = new Product()
            {
                Id = "002",
                Name = "Google Suites",
                Description = "G Suite comprises Gmail, Hangouts, Calendar, and Currents for communication; Drive for storage; Docs, Sheets, Slides, Keep, Forms, and Sites for productivity and collaboration; and, depending on the plan, an Admin panel and Vault for managing users and the services.",
                Price = 150,
                ProductPic = "Gsuite.png"
            };
            dbcontext.Add(product2);

            Product product3 = new Product()
            {
                Id = "003",
                Name = "Adobe Photoshop",
                Description = "Photoshop is Adobe's photo editing, image creation and graphic design software. The software provides many image editing features for raster (pixel-based) images as well as vector graphics. ... Photoshop is used by photographers, graphic designers, video game artists, advertising and meme designers.",
                Price = 60,
                ProductPic = "Photoshop.png"
            };
            dbcontext.Add(product3);

            Product product4 = new Product()
            {
                Id = "004",
                Name = "Adobe Illustrator",
                Description = "Adobe Illustrator or Illustrator is a vector graphics editing program published by Adobe. It is useful for designing logos, clip art, blueprints, and other precise, resolution-independent illustrations. Illustrator was first released in 1987 for the Apple Macintosh; today it also runs on Microsoft Windows",
                Price = 60,
                ProductPic = "Illustrator.png"
            };
            dbcontext.Add(product4);

            Product product5 = new Product()
            {
                Id = "005",
                Name = "Adobe Lightroom",
                Description = "Adobe Lightroom(officially Adobe Photoshop Lightroom) is a family of image organization and image manipulation software developed by Adobe Systems for Windows,macOS,iOS, Android, and tvOS (Apple TV). It allows importing/saving, viewing, organizing, tagging, editing, and sharing large numbers of digital images.",
                Price = 70,
                ProductPic = "Lightroom.png"
            }; 
            dbcontext.Add(product5);

            Product product6 = new Product()
            {
                Id = "006",
                Name = "Adobe InDesign",
                Description = "Adobe InDesign is a desktop publishing and typesetting software application produced by Adobe Systems. It can be used to create works such as posters, flyers, brochures, magazines, newspapers, presentations, books and ebooks.",
                Price = 70,
                ProductPic = "Indesign.png"
            };
            dbcontext.Add(product6);

            Product product7 = new Product()
            {
                Id = "007",
                Name = "Visual Studio Code",
                Description = "Visual Studio is an Integrated Development Environment(IDE) developed by Microsoft to develop GUI(Graphical User Interface), console, Web applications, web apps, mobile apps, cloud, and web services, etc. With the help of this IDE, you can create managed code as well as native code.",
                Price = 250,
                ProductPic = "Visualstudio.png"
            };
            dbcontext.Add(product7);

            Product product8 = new Product()
            {
                Id = "008",
                Name = "IBM SPSS",
                Description = "SPSS Statistics is a software package used for interactive, or batched, statistical analysis. Long produced by SPSS Inc., it was acquired by IBM in 2009. The current versions are named IBM SPSS Statistics.",
                Price = 260,
                ProductPic = "SPSS.png"
            };
            dbcontext.Add(product8);

            Product product9 = new Product()
            {
                Id = "009",
                Name = "MS SQL",
                Description = "Microsoft SQL Server is a relational database management system developed by Microsoft. As a database server, it is a software product with the primary function of storing and retrieving data as requested by other software applications—which may run either on the same computer or on another computer across a network.",
                Price = 100,
                ProductPic = "Mssql.png"
           };
            dbcontext.Add(product9);

            Product product10 = new Product()
            {
                Id = "010",
                Name = "My SQL",
                Description = "MySQL is the world's most popular open source database. With its proven performance, reliability and ease-of-use, MySQL has become the leading database choice for web-based applications, used by high profile web properties including Facebook, Twitter, YouTube, Yahoo! and many more.Oracle drives MySQL innovation, delivering new capabilities to power next generation web, cloud, mobile and embedded applications.",
                Price = 10,
                ProductPic = "Mysql.png"
           };
            dbcontext.Add(product10);

            List<Product> prodList = new List<Product>();
            prodList.Add(product1);
            prodList.Add(product2);
            prodList.Add(product3);
            prodList.Add(product4);
            prodList.Add(product5);
            prodList.Add(product6);
            prodList.Add(product7);
            prodList.Add(product8);
            prodList.Add(product9);
            prodList.Add(product10);
            foreach(Product prod in prodList)
            {
                for(var i = 0; i < 10; i++)
                {
                    ActivationCode code = new ActivationCode()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Product = prod
                    };
                    dbcontext.Add(code);
                }
            }
            //User
            User user1 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "John",
                Password = "Password"
                
            };
            dbcontext.Add(user1);

            User user2 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "Kaylene",
                Password = "Password"
                
            };
            dbcontext.Add(user2);

            //Product Review
            ProductReview review1 = new ProductReview
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                Review = "This is a wonderful product, it helped me in my assignment",
                Username = user1.Username,
                Rating = 4
            };
            dbcontext.Add(review1);

            ProductReview review2 = new ProductReview
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product1.Id,
                Review = "Wonderful tool to use during Work From Home, glad I bought it. 10/10",
                Username = user2.Username,
                Rating = 5
            };
            dbcontext.Add(review2);

            ProductReview review3 = new ProductReview
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product2.Id,
                Review = "Awesome Software!",
                Username = user2.Username,
                Rating = 5
            };
            dbcontext.Add(review3);

            //order
            /*Order order1 = new Order
            {
                Id = "o1",
                UserId  = user1.Id,
                Time = new DateTime(2019, 5, 28, 22, 30, 00)
            };
            dbcontext.Add(order1);

            OrderDetails orderdetails1  = new OrderDetails
            {     Id = "od1",          
                  ProductId  =  product2.Id,
                  OrderId  =  order1.Id,
                  Qty=3
            };
            dbcontext.Add(orderdetails1);

            OrderDetails orderdetails2 = new OrderDetails
            {   
                Id ="od2",
                ProductId = product1.Id,
                OrderId = order1.Id,
                Qty = 1
            };
            dbcontext.Add(orderdetails2);

            Order order2 = new Order
            {
                Id = "o2",
                UserId = user1.Id,
                Time = new DateTime(2020, 2, 24, 22, 30, 00)
                
            };
            dbcontext.Add(order2);

            OrderDetails orderdetails3 = new OrderDetails
            {
                Id="od3",
                ProductId = product1.Id,
                OrderId = order2.Id,
                Qty = 1
            };
            dbcontext.Add(orderdetails3);

            OrderDetails orderdetails4 = new OrderDetails
            {
                Id = "od4",
                ProductId = product3.Id,
                OrderId = order2.Id,
                 Qty = 1
            };
            dbcontext.Add(orderdetails4);*/

            dbcontext.SaveChanges();

        }
    }
}
