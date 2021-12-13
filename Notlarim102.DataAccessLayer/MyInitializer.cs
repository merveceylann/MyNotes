using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<NotlarimContext>
    {
        protected override void Seed(NotlarimContext context)
        {
            //Adding admin user...
            NotlarimUser admin = new NotlarimUser()
            {
                Name = "Merve",
                Surname = "Ceylan",
                Email = "merve@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "merveotesi",
                Password = "1234",
                CreatdOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "merveotesi"
            };

            NotlarimUser standartUser = new NotlarimUser()
            {
                Name = "Busra",
                Surname = "Koc",
                Email = "busra@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "busrakoc",
                Password = "12345",
                CreatdOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUserName = "merveotesi"
            };
            context.NotlarimUsers.Add(admin);
            context.NotlarimUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                NotlarimUser user = new NotlarimUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user-{i}",
                    Password = "123",
                    CreatdOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user-{i}"
                };
                context.NotlarimUsers.Add(user);
            }

            context.SaveChanges();

            //User List for using...

            List<NotlarimUser> userList = context.NotlarimUsers.ToList();

            //Adding fake category...

            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()  //bu sinif
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatdOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "merveotesi"
                };

                context.Categories.Add(cat);   //bu da db contexti

                //Adding fake Notes...

                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    NotlarimUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatdOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.Username
                    };
                    cat.Notes.Add(note);

                    //Adding fake Comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        NotlarimUser comment_owner= userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner=comment_owner,
                            CreatdOn=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_owner.Username
                        };
                        note.Comments.Add(comment);
                    }

                    //Adding fake Likes...
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]
                        };
                        note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
