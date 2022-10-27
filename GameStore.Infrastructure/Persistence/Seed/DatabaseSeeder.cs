using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedDatabase(GameStoreContext context, IUnitOfWork unitOfWork)
    {
        if (await context.Genres.FirstOrDefaultAsync() is null)
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
        }

        if (await context.Platforms.FirstOrDefaultAsync() is null)
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
        }

        await unitOfWork.SaveChangesAsync();
    }
}