using Microsoft.EntityFrameworkCore;
using TestPoint.DAL.Contexts;
using TestPoint.Domain;

namespace TestPoint.WebAPI.Database;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();

        try
        {
            appContext.Database.Migrate();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
            {
                webApp.PopulateTestData();
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            throw;
        }

        return webApp;
    }

    private static WebApplication PopulateTestData(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();

        try
        {
            CreateAdmins(appContext);
            CreateUsers(appContext);
            CreateTests(appContext);
            CreateUserGroups(appContext);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            throw;
        }

        return webApp;
    }

    private static void CreateAdmins(AppDbContext context)
    {
        var admins = new Administrator[]
        {
            new Administrator // [makima1, makima12345]
            {
                Login = new SystemLogin
                {
                    LoginType = LoginType.Administrator,
                    Username = "makima1",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now,
                }
            },
            new Administrator // [makima2, makima12345]
            {
                Login = new SystemLogin
                {
                    LoginType = LoginType.Administrator,
                    Username = "makima2",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new Administrator // [makima3, makima12345]
            {
                Login = new SystemLogin
                {
                    LoginType = LoginType.Administrator,
                    Username = "makima3",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            }
        };

        var adminsSet = context.Set<Administrator>();
        foreach (var admin in admins)
        {
            AddAdmin(adminsSet, admin);
        }

        context.SaveChanges();


        static void AddAdmin(DbSet<Administrator> adminsSet, Administrator admin)
        {
            if (adminsSet
                .Include(x => x.Login)
                .FirstOrDefault(x => x.Login.Username == admin.Login.Username && x.Login.LoginType == LoginType.Administrator) is null)
            {
                adminsSet.Add(admin);
            }
        }
    }

    private static void CreateUsers(AppDbContext context)
    {
        var users = new User[]
        {
            new User
            {
                FirstName = "Jhon",
                LastName = "Jhones",
                EmailConfirmed = false,
                Email = "jonne@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima1, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima1",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Maria",
                LastName = "DB",
                EmailConfirmed = false,
                Email = "maria@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima2, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima2",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Anna",
                LastName = "Frank",
                EmailConfirmed = false,
                Email = "anana.ff@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima3, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima3",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Maxon",
                LastName = "Frontend",
                EmailConfirmed = false,
                Email = "zefirka@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima4, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima4",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Marina",
                LastName = "JS",
                EmailConfirmed = false,
                Email = "submarina.js@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima5, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima5",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Victor",
                LastName = "Victorovich",
                EmailConfirmed = false,
                Email = "vik2012@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima6, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima6",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Evgeniy",
                LastName = "Pritko",
                EmailConfirmed = false,
                Email = "evgen00@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima7, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima7",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Denis",
                LastName = "SERVER",
                EmailConfirmed = false,
                Email = "denleo@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima8, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima8",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Danilo",
                LastName = "Chipsono",
                EmailConfirmed = false,
                Email = "chips@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima9, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima9",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            },
            new User
            {
                FirstName = "Igor",
                LastName = "Khmelev",
                EmailConfirmed = false,
                Email = "igorshkh@gmail.com",
                Avatar = null,
                Login = new SystemLogin // [makima10, makima12345]
                {
                    LoginType = LoginType.User,
                    Username = "makima10",
                    PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                    PasswordReseted = false,
                    RegistryDate = DateTime.Now
                }
            }
        };

        var usersSet = context.Set<User>();
        foreach (var user in users)
        {
            AddUser(usersSet, user);
        }

        context.SaveChanges();


        static void AddUser(DbSet<User> userSet, User user)
        {
            if (userSet
                .Include(x => x.Login)
                .FirstOrDefault(x => (x.Login.Username == user.Login.Username && x.Login.LoginType == LoginType.User) || x.Email == user.Email) is null)
            {
                userSet.Add(user);
            }
        }
    }

    private static void CreateTests(AppDbContext context)
    {
        var admin = context.Set<Administrator>()
            .Include(x => x.Login)
            .FirstOrDefault(x => x.Login.Username == "makima1" && x.Login.LoginType == LoginType.Administrator);

        var test = new Test
        {
            AuthorId = admin!.Id,
            Author = admin!.Login.Username,
            Name = "Easy generic test",
            Difficulty = TestDifficulty.Easy,
            EstimatedTime = 15,
            Questions = new Question[]
            {
                new Question
                {
                    QuestionText = "What is * oparator?",
                    QuestionType = QuestionType.SingleOption,
                    Answers = new Answer[]
                    {
                        new Answer
                        {
                            AnswerText = "I don't care",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            AnswerText = "multiply",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            AnswerText = "plus",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            AnswerText = "minus",
                            IsCorrect = false
                        },
                    }
                },
                new Question
                {
                    QuestionText = "Why do you need math?",
                    QuestionType = QuestionType.MultipleOptions,
                    Answers = new Answer[]
                    {
                        new Answer
                        {
                            AnswerText = "I don't need math",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            AnswerText = "For breakfast",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            AnswerText = "To be super clever!",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            AnswerText = "To keep your brain active",
                            IsCorrect = true
                        },
                    }
                },
                new Question
                {
                    QuestionText = "Who is the first woman programmer?",
                    QuestionType = QuestionType.TextSubstitution,
                    Answers = new Answer[]
                    {
                        new Answer
                        {
                            AnswerText = "Ada Lovelace",
                            IsCorrect = true
                        }
                    }
                },
                new Question
                {
                    QuestionText = "Best programming languages?",
                    QuestionType = QuestionType.MultipleOptions,
                    Answers = new Answer[]
                    {
                        new Answer
                        {
                            AnswerText = "Marina JS",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            AnswerText = "C#",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            AnswerText = "Ruby",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            AnswerText = "Perl",
                            IsCorrect = false
                        },
                    }
                },
                new Question
                {
                    QuestionText = "What is H20?",
                    QuestionType = QuestionType.SingleOption,
                    Answers = new Answer[]
                    {
                        new Answer
                        {
                            AnswerText = "Water",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            AnswerText = "K-pop group",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            AnswerText = "I don't know",
                            IsCorrect = false
                        }
                    }
                }
            }
        };

        var testSet = context.Set<Test>();
        AddTest(testSet, test);
        context.SaveChanges();


        static void AddTest(DbSet<Test> testSet, Test test)
        {
            if (testSet.FirstOrDefault(x => x.AuthorId == test.AuthorId && x.Name == test.Name) is null)
            {
                testSet.Add(test);
            }
        }
    }

    private static void CreateUserGroups(AppDbContext context)
    {
        var admin = context.Set<Administrator>()
            .Include(x => x.Login)
            .FirstOrDefault(x => x.Login.Username == "makima1" && x.Login.LoginType == LoginType.Administrator);

        var userGroups = new UserGroup[]
        {
            new UserGroup
            {
                AdministratorId = admin!.Id,
                Name = "10702119"
            },
            new UserGroup
            {
                AdministratorId = admin!.Id,
                Name = "10702319"
            }
        };

        var userGroupSet = context.Set<UserGroup>();
        foreach (var group in userGroups)
        {
            AddUserGroup(userGroupSet, group);
        }

        context.SaveChanges();


        static void AddUserGroup(DbSet<UserGroup> groupSet, UserGroup group)
        {
            if (groupSet.FirstOrDefault(x => x.AdministratorId == group.AdministratorId && x.Name == group.Name) is null)
            {
                groupSet.Add(group);
            }
        }
    }

}
