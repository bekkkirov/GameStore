using GameStore.Application.Persistence;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedDatabase(IUnitOfWork unitOfWork)
    {
        if (!(await unitOfWork.GenreRepository.GetAsync()).Any())
        {
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Name = "Strategy",
                    SubGenres = new List<Genre>()
                    {
                        new Genre()
                        {
                            Name = "RTS"
                        },

                        new Genre()
                        {
                            Name = "TBS"
                        }
                    }
                },

                new Genre()
                {
                    Name = "RPG"
                },
                
                new Genre()
                {
                    Name = "Sports"
                }, 
                
                new Genre()
                {
                    Name = "Races",
                    SubGenres = new List<Genre>()
                    {
                        new Genre()
                        {
                            Name = "Rally"
                        },
                        
                        new Genre()
                        {
                            Name = "Arcade"
                        },
                        
                        new Genre()
                        {
                            Name = "Formula"
                        },
                        
                        new Genre()
                        {
                            Name = "Off-road"
                        },
                    }
                },

                new Genre()
                {
                    Name = "Action",
                    SubGenres = new List<Genre>()
                    {
                        new Genre()
                        {
                            Name = "FPS"
                        },

                        new Genre()
                        {
                            Name = "TPS"
                        },
                    }
                },

                new Genre()
                {
                    Name = "Adventure"
                },
                
                new Genre()
                {
                    Name = "Puzzle & Skill"
                },
                
                new Genre()
                {
                    Name = "Misc."
                },
            };

            foreach (var genre in genres)
            {
                unitOfWork.GenreRepository.Add(genre);
            }

            await unitOfWork.SaveChangesAsync();
        }

        if (!(await unitOfWork.PlatformTypeRepository.GetAsync()).Any())
        {
            var platforms = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Type = "Desktop"
                },
                
                new PlatformType()
                {
                    Type = "Mobile"
                },
                
                new PlatformType()
                {
                    Type = "Browser"
                },
                
                new PlatformType()
                {
                    Type = "Console"
                },

            };

            foreach (var platform in platforms)
            {
                unitOfWork.PlatformTypeRepository.Add(platform);
            }

            await unitOfWork.SaveChangesAsync();
        }

        if (!(await unitOfWork.UserRepository.GetAsync()).Any())
        {
            var user = new User()
            {
                UserName = "bekirov",
                FirstName = "Vladyslav",
                LastName = "Bekirov",
            };

            unitOfWork.UserRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}