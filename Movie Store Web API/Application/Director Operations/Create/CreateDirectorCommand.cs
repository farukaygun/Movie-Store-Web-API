using AutoMapper;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Application.Director_Operations.Create
{
    public class CreateDirectorCommand(IMovieStoreDbContext context,
        IMapper mapper,
        CreateDirectorModel model)
    {
        public CreateDirectorModel Model { get; set; } = model;
        public async Task<CreateDirectorModel> Handle()
        {
            var director = context.Directors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

            if (director is not null)
                throw new InvalidOperationException("Director already exist!");

            director = mapper.Map<Director>(Model);

            await context.Directors.AddAsync(director);
            await context.SaveChangesAsync();

            return mapper.Map<CreateDirectorModel>(director);
        }
    }
    public class CreateDirectorModel
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public ICollection<Movie> Movies { get; set; } = []; 
    }
}